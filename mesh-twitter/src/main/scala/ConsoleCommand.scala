import akka.actor.ActorRef

sealed trait ConsoleCommand extends Serializable {
  val host: String = null
  val port: Int = 0
}

case class ExitCommand() extends ConsoleCommand

case class GatherCommand(name: String, howMany: Int) extends ConsoleCommand

case class ShowRoutingCommand() extends ConsoleCommand

case class ShowRoutingReplyCommand(routingTable: Map[String, List[String]]) extends ConsoleCommand

case class ShowGatheredTweetsCommand(messages: List[Tweet]) extends ConsoleCommand

case class ConnectToMeCommand(override val host: String,
                              override val port: Int) extends ConsoleCommand

case class DisconnectCommand() extends ConsoleCommand

case class ManualTweetCommand(sender: ActorRef, text: String) extends ConsoleCommand

case class RandomTweetCommand(sender: ActorRef) extends ConsoleCommand

case class NodeNameCommand(add: Boolean, name: String) extends ConsoleCommand

