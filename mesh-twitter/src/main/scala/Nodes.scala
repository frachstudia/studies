import java.net.InetSocketAddress
import java.text.SimpleDateFormat
import java.util.Calendar

import akka.actor._
import akka.event.Logging

import scala.collection.mutable

class LocalNode(me: RemoteNode,
                senderActors: mutable.Map[String, ActorRef],
                consoleHandler: ActorRef,
                nodesChangeListener: ActorRef,
                gatherActor: ActorRef) extends Actor {

  import context.system

  val log = Logging(context.system, this)
  val otherNodesRouting = new mutable.HashMap[String, RemoteNode]()
  val receivedMessageIds = new mutable.HashSet[String]()
  val senderNodes = new mutable.HashMap[String, RemoteNode]()

  override def receive: Receive = {
    case m: GatherMessage => gatherActor ! m
    case msg: Message => handleMessage(msg)
    case msg: ConsoleCommand => handleConsoleCommand(msg)
    case _ => throw new Exception("invalid message")
  }

  private def handleMessage(msg: Message) = {
    msg match {
      case _: NewConnectionMessage => NewConnectionMessageReceive(msg.sender, msg.id, msg.routingTable)
      case m: ConnectMessage => ConnectMessageReceive(m)
      case _: DisconnectMessage => DisconnectMessageReceive(msg.sender, msg.id, msg.forwarder)
      case _: WrongNameMessage => WrongNameMessageReceive(msg.sender, msg.id)
      case _: ShouldConnectToMessage => ShouldConnectToMessageReceive(msg.sender, msg.id, msg.forwarder)
      case _: MakeChainMessage => MakeChainMessageReceive(msg.sender, msg.id, msg.forwarder, msg.nodes)
      case _ => throw new Exception("invalid message")
    }
  }

  private def NewConnectionMessageReceive(sender: RemoteNode, id: String, routingTable: mutable.Set[String]) = {
    if (!senderActors.contains(sender.name)) senderActors.put(sender.name, system.actorOf(Props(new Sender(sender.socket))))
    if (!senderNodes.contains(sender.name)) senderNodes.put(sender.name, sender)
    if (otherNodesRouting.contains(sender.name)) otherNodesRouting.remove(sender.name)

    routingTable.foreach(item => {
      if (item != me.name && !senderActors.contains(item)) {
        if (!otherNodesRouting.contains(item))
          otherNodesRouting.put(item, new RemoteNode(sender.name, sender.socket))
      }
    })

    log.info("Got NewConnectionMessage: {}, {}", sender.name, id)
  }

  private def ConnectMessageReceive(msg: ConnectMessage): Unit = {
    if (!receivedMessageIds.contains(msg.id)) {
      receivedMessageIds.add(msg.id)

      log.info("Got ConnectMessage: {}, {}, {}", msg.sender.name, msg.id, msg.forwarder.name)

      if (msg.sender.name == me.name || senderActors.contains(msg.sender.name) || otherNodesRouting.contains(msg.sender.name)) {
        val temp = system.actorOf(Props(new Sender(msg.sender.socket)))
        Thread.sleep(200)
        temp ! WrongNameMessage(me, msg.id)
        return
      }

      if (msg.forwarder.name == msg.sender.name) {
        if (!senderNodes.contains(msg.sender.name)) senderNodes.put(msg.sender.name, msg.sender)
        senderActors.put(msg.sender.name, system.actorOf(Props(new Sender(msg.sender.socket))))
        Thread.sleep(200)
        senderActors.get(msg.sender.name).get ! new NewConnectionMessage(me, getNextId(), getNodesRouting(msg.sender))
      } else {
        otherNodesRouting.put(msg.sender.name, new RemoteNode(msg.forwarder.name, msg.forwarder.socket))
      }

      forwardMessage(msg)
    }
  }

  private def forwardMessage(msg: Message) = {
    senderActors.foreach(item => {
      if (item._1 != msg.forwarder.name && item._1 != msg.sender.name) {
        msg match {
          case _: ConnectMessage => item._2 ! ConnectMessage(msg.sender, msg.id, me)
          case _: DisconnectMessage => item._2 ! DisconnectMessage(msg.sender, msg.id, me)
          // todo other actions if needed
          case _ => throw new Exception("invalid message")
        }
      }
    })
  }

  private def getNodesRouting(recipient: RemoteNode): mutable.Set[String] = {
    val nodeSet = scala.collection.mutable.Set[String]()
    otherNodesRouting.foreach(item => {
      nodeSet += item._1
    })
    senderActors.foreach(item => {
      if (item._1 != recipient.name) nodeSet += item._1
    })
    nodeSet
  }

  private def DisconnectMessageReceive(sender: RemoteNode, id: String, forwarder: RemoteNode) = {
    if (!receivedMessageIds.contains(id)) {
      //println(": otrzymalem disconnectMessage od " + sender.name + " o id = " + id.toString())
      receivedMessageIds.add(id)

      log.info("Got DisconnectMessage: {}, {}, {}", sender.name, id, forwarder.name)

      if (sender.name == forwarder.name) {
        if (senderActors.contains(sender.name)) senderActors.remove(sender.name)
      } else {
        if (otherNodesRouting.contains(sender.name)) otherNodesRouting.remove(sender.name)
      }

      forwardMessage(new DisconnectMessage(sender, id, forwarder))
    }
  }

  private def ShouldConnectToMessageReceive(sender: RemoteNode, id: String, forwarder: RemoteNode): Unit = {
    if (!receivedMessageIds.contains(id)) {
      receivedMessageIds.add(id)
      log.info("Got ShouldConnectToMessage: {}, {}, {}", sender.name, id, forwarder.name)

      if (senderActors.contains(forwarder.name)) return

      senderActors.put(forwarder.name, system.actorOf(Props(new Sender(sender.socket))))
      if (!senderNodes.contains(sender.name)) senderNodes.put(sender.name, sender)
      if (otherNodesRouting.contains(forwarder.name)) otherNodesRouting.remove(forwarder.name)

      Thread.sleep(500)
      senderActors.get(forwarder.name).get ! new NewConnectionMessage(me, getNextId(), getNodesRouting(forwarder))
    }
  }

  private def WrongNameMessageReceive(sender: RemoteNode, id: String) = {
    println("Host name in this network already exists. Take another one and try again.")
    system.shutdown()
  }

  private def MakeChainMessageReceive(sender: RemoteNode,
                                      id: String,
                                      forwarder: RemoteNode,
                                      nodes: mutable.HashMap[String, RemoteNode]) = {
    if (!receivedMessageIds.contains(id)) {
      receivedMessageIds.add(id)

      log.info("Got MakeChainMessage: {}, {}, {}", sender.name, id, forwarder.name)

      if (sender.name != forwarder.name) {
        if (!senderNodes.contains(forwarder.name)) senderNodes.put(forwarder.name, forwarder)
        if (!senderActors.contains(forwarder.name))
          senderActors.put(forwarder.name, system.actorOf(Props(new Sender(forwarder.socket))))
        if (otherNodesRouting.contains(forwarder.name)) otherNodesRouting.remove(forwarder.name)

        Thread.sleep(200)
        senderActors.get(forwarder.name).get ! NewConnectionMessage(me, getNextId(), getNodesRouting(forwarder))
      }

      if (nodes.size > 0) {
        val tempNode = nodes.head
        val temp = system.actorOf(Props(new Sender(new InetSocketAddress(tempNode._2.socket.getHostName, tempNode._2.socket.getPort))))

        nodes.remove(tempNode._1)

        Thread.sleep(500)
        temp ! MakeChainMessage(sender, id, me, nodes)
      }
    }
  }

  private def handleConsoleCommand(msg: ConsoleCommand) = {
    msg match {
      case _: ExitCommand => system.shutdown()
      case _: DisconnectCommand => sendDisconnectAllMessage()
      case _: ShowRoutingCommand => prepareRoutingTable()
      case _: ConnectToMeCommand => sendConnectToMeMessage(new InetSocketAddress(msg.host, msg.port))
      case _ => throw new Exception("invalid message")
    }
  }

  private def sendConnectToMeMessage(socket: InetSocketAddress) = {
    val temp = system.actorOf(Props(new Sender(socket)))
    Thread.sleep(500)
    temp ! ShouldConnectToMessage(me, getNextId(), me)
  }

  private def sendDisconnectAllMessage(): Unit = {
    val id = getNextId()
    receivedMessageIds.add(id)
    Thread.sleep(100)
    val secId = getNextId()
    receivedMessageIds.add(secId)

    val neighbours = senderActors.toArray

    neighbours.size match {
      case 0 => return
      case 1 => neighbours(0)._2 ! DisconnectMessage(me, id, me)
      case _ => {
        neighbours.foreach(item => {
          item._2 ! DisconnectMessage(me, id, me)
        })
        senderNodes.remove(neighbours.head._1)
        Thread.sleep(200)
        neighbours.head._2 ! MakeChainMessage(me, secId, me, senderNodes)
      }
    }
  }

  private def prepareRoutingTable() = {
    val neighbours = new mutable.HashMap[String, List[String]]()
    senderActors.keySet.foreach(s => {
      //neighbours.put(s.name, otherNodesRouting.get(s.name).map(f => f.name).filter(f => f != s.name).toList)
      val temp = new mutable.HashSet[String]()
      otherNodesRouting.foreach(item => {
        if (item._2.name == s && item._1 != s) {
          temp.add(item._1)
        }
      })
      neighbours.put(s, temp.toList)
    })
    consoleHandler ! new ShowRoutingReplyCommand(neighbours.toMap)
  }

  def this(me2: RemoteNode,
           senders: mutable.Map[String, ActorRef],
           consoleHandler: ActorRef,
           nodesChangeListener: ActorRef,
           gatherActor: ActorRef,
           firstNeighbourHostname: String,
           firstNeighbourPort: Int) = {
    this(me2, senders, consoleHandler, nodesChangeListener, gatherActor)

    val firstNeighbour = new RemoteNode("", new InetSocketAddress(firstNeighbourHostname, firstNeighbourPort))

    log.info("Sending connection request to {}:{}.", firstNeighbourHostname, firstNeighbourPort)

    val sen = system.actorOf(Props(new Sender(firstNeighbour.socket)))
    Thread.sleep(500)
    val id = getNextId()
    sen ! ConnectMessage(me, id, me)
    receivedMessageIds.add(id)
  }

  private def getNextId(): String = {
    val format = new SimpleDateFormat("dd-hh-mm-ss-SSS")
    me.name + "_" + format.format(Calendar.getInstance().getTime)
  }
}

class RemoteNode(nodeName: String, sock: InetSocketAddress) extends Serializable {
  val name = nodeName
  val socket = sock

  def this(sock: InetSocketAddress) = this("", sock)

  override def equals(o: Any) = o match {
    case rn: RemoteNode => rn.name == this.name
    case _ => false
  }

  override def hashCode = name.hashCode
}
