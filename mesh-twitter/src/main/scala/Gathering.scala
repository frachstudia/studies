import java.text.SimpleDateFormat
import java.util.Calendar

import akka.actor.{Actor, ActorRef}
import akka.event.Logging

import scala.collection.mutable

class GatherState(i: Boolean, ssender: String, noMessages: Int, t: String) {
  val init = i
  val sender = ssender
  val target = t
  val noMsgs = noMessages
  val responded = new mutable.HashSet[String]()
  val tweets = new mutable.HashMap[Calendar, Tweet]()
}

class Gathering(consoleHandler: ActorRef,
                senders: mutable.Map[String, ActorRef],
                myName: String) extends Actor {

  val log = Logging(context.system, this)

  val tweets = new mutable.HashSet[Tweet]()
  val gatherStates = new mutable.HashMap[String, GatherState]()

  override def receive: Receive = {
    case tweet: Tweet => tweets.add(tweet)
    case m: GatherRequestMessage => receiveGatherRequest(m)
    case m: GatherResponseMessage => receiveGatherResponse(m)
    case m: GatherVisitedMessage => receiveGatherVisited(m)
    case c: GatherCommand => initGather(c.name, c.howMany)
    case _ => throw new Exception("invalid message")
  }

  private def initGather(nodeName: String, noMsgs: Int) = {
    val id = getNextId()
    val state = new GatherState(true, myName, noMsgs, nodeName)
    gatherStates.put(id, state)

    log.info("Initializing gather {} messages about {} and sending to {} nodes.", noMsgs, nodeName, senders.size)

    senders.foreach(item => {
      item._2 ! new GatherRequestMessage(myName, myName, id, nodeName, noMsgs)
      log.debug("Send GatherRequest to {}.", item._1)
    })

    if (senders.size == 0) {
      composeReply(id, myName, state)
    }
  }

  private def getNextId(): String = {
    val format = new SimpleDateFormat("dd-hh-mm-ss-SSS")
    myName + "_gather_" + format.format(Calendar.getInstance().getTime)
  }

  private def receiveGatherRequest(msg: GatherRequestMessage) = {
    if (gatherStates.contains(msg.id)) {
      log.info("Got GatherRequest from {} again, replying with GatherVisited.", msg.sender)
      senders.get(msg.sender).get ! new GatherVisitedMessage(msg.init, myName, msg.id)
    } else {
      val state = new GatherState(false, msg.sender, msg.numberOfTweets, msg.target)
      gatherStates.put(msg.id, state)
      log.info("Got GatherRequest from {}", msg.sender)

      senders.filter(s => s._1 != msg.sender).foreach(i => {
        i._2 ! new GatherRequestMessage(msg.init, myName, msg.id, msg.target, msg.numberOfTweets)
        log.debug("Send GatherRequest to {}.", i._1)
      })

      if (senders.size < 2) {
        log.info("Not having other nodes, replying back.")
        composeReply(msg.id, msg.init, state)
      }
    }
  }

  private def receiveGatherResponse(msg: GatherResponseMessage) = {
    if (!gatherStates.contains(msg.id)) {
      throw new Exception("received Response before Request")
    } else {
      val state = gatherStates.get(msg.id).get
      state.responded.add(msg.sender)

      state.tweets ++= msg.tweets.map(t => (t.date, t)).toMap

      log.info("Got Response from {}.", msg.sender)

      val senderSaturated = state.init match {
        case true => senders.size
        case false => senders.size - 1
      }

      if (state.responded.size == senderSaturated) {
        log.info("Got replies from all nodes, replying back. (R)")
        composeReply(msg.id, msg.init, state)
      } else if (state.responded.size > senderSaturated) {
        throw new Exception("more replies than senders")
      }
    }
  }

  private def receiveGatherVisited(msg: GatherVisitedMessage) = {
    if (!gatherStates.contains(msg.id)) {
      throw new Exception("received Visited before Request")
    } else {
      val state = gatherStates.get(msg.id).get
      state.responded.add(msg.sender)

      log.info("Got Visited from {}.", msg.sender)

      val senderSaturated = state.init match {
        case true => senders.size
        case false => senders.size - 1
      }

      if (state.responded.size == senderSaturated) {
        log.info("Got replies from all nodes, replying back. (V)")
        composeReply(msg.id, msg.init, state)
      } else if (state.responded.size > senderSaturated) {
        throw new Exception("more replies than senders")
      }
    }
  }

  private def composeReply(id: String, init: String, state: GatherState) = {

    state.tweets ++= tweets.filter(t => t.text.contains(state.target)).map(t => (t.date, t)).toMap

    val newestTweets = state.tweets.toSeq.sortBy(_._1).map(_._2).takeRight(state.noMsgs).toList

    if (state.sender == myName) {
      consoleHandler ! new ShowGatheredTweetsCommand(newestTweets)
      log.info("Sent {} collected tweets to console.", newestTweets.size)
    } else {
      senders.get(state.sender).get ! new GatherResponseMessage(init, myName, id, newestTweets)
      log.info("Sent {} collected tweets back to {}.", newestTweets.size, state.sender)
    }
  }
}
