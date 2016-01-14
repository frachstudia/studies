import java.util.Calendar

import scala.collection.mutable

// id to zlepek <nodeName>_<id>
// np. node1_1, node1_2, node1_3, itd.
sealed trait Message extends Serializable {
  val sender: RemoteNode
  val id: String
  val forwarder: RemoteNode = sender
  val numberOfTweets: Int = 0
  val target: String = null
  val tweets: List[Tweet] = null
  val routingTable: mutable.Set[String] = null
  val nodes: mutable.HashMap[String, RemoteNode] = null
}

sealed trait GatherMessage extends Serializable {
  val sender: String
  val id: String
  val init: String
  val numberOfTweets: Int = 0
  val target: String = null
  val tweets: List[Tweet] = null
}

case class NewConnectionMessage(sender: RemoteNode,
                                val id: String,
                                override val routingTable: mutable.Set[String]) extends Message

case class ConnectMessage(sender: RemoteNode,
                          id: String,
                          override val forwarder: RemoteNode) extends Message

case class DisconnectMessage(sender: RemoteNode,
                             id: String,
                             override val forwarder: RemoteNode) extends Message

case class WrongNameMessage(sender: RemoteNode,
                            id: String) extends Message

case class ShouldConnectToMessage(sender: RemoteNode,
                                  id: String,
                                  override val forwarder: RemoteNode) extends Message

case class MakeChainMessage(sender: RemoteNode,
                            id: String,
                            override val forwarder: RemoteNode,
                            override val nodes: mutable.HashMap[String, RemoteNode]) extends Message

class Tweet(d: Calendar, nn: String, t: String) extends Serializable {
  val date = d
  val nodeName = nn
  val text = t
}

case class GatherRequestMessage(init: String,
                                sender: String,
                                id: String,
                                override val target: String,
                                override val numberOfTweets: Int) extends GatherMessage

case class GatherResponseMessage(init: String,
                                 sender: String,
                                 id: String,
                                 override val tweets: List[Tweet]) extends GatherMessage

case class GatherVisitedMessage(init: String,
                                sender: String,
                                id: String) extends GatherMessage
