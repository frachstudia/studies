import java.net.InetSocketAddress

import akka.actor._
import akka.event.Logging
import akka.io._
import akka.serialization.SerializationExtension
import akka.util.ByteString

class Listener(port: Int, messageHandler: ActorRef) extends Actor {

  import context.system

  val socket = new InetSocketAddress("localhost", port)
  private val log = Logging(context.system, this)
  private val serializer = SerializationExtension(context.system)

  IO(Udp) ! Udp.Bind(self, socket)

  log.info("Bound local listener to port {}", port)

  def receive = {
    case Udp.Bound(local) => context.become(ready(sender()))
  }

  def ready(socket: ActorRef): Receive = {
    case Udp.Received(data, remote) => {
      val msg = serializer.findSerializerFor(data).fromBinary(data.toArray).asInstanceOf[Serializable]
      log.debug("Received {}.", msg.getClass);
      messageHandler ! msg
    }
    case Udp.Unbind => socket ! Udp.Unbind
    case Udp.Unbound => context.stop(self)
  }
}

class Sender(remote: InetSocketAddress) extends Actor {

  import context.system

  private val serializer = SerializationExtension(context.system)
  private val log = Logging(context.system, this)

  IO(Udp) ! Udp.SimpleSender

  def receive = {
    case Udp.SimpleSenderReady =>
      context.become(ready(sender()))
  }

  def ready(send: ActorRef): Receive = {
    case msg: Message => {
      send ! Udp.Send(ByteString.apply(serializer.findSerializerFor(msg).toBinary(msg)), remote)
      log.debug("Sent Message {} to {}, id: {}.", msg.getClass, msg.sender, msg.id)
    }
    case msg: GatherMessage => {
      send ! Udp.Send(ByteString.apply(serializer.findSerializerFor(msg).toBinary(msg)), remote)
      log.debug("Sent Message {} to {}, id: {}.", msg.getClass, msg.sender, msg.id)
    }
    case _ => throw new Exception("invalid message")
  }
}