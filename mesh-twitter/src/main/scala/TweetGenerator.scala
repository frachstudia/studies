import java.util.Calendar

import akka.actor.{Actor, ActorRef}
import akka.event.Logging

import scala.collection.mutable
import scala.concurrent.duration._
import scala.util.Random

class TweetGenerator(myName: String) extends Actor {
  private val messageTemplates = Array(
    "I am posting about # and #.",
    "I hate #.",
    "Today I went with # and # to a party at #.",
    "I am so alone.",
    "# and #, let's go party!"
  )

  private val log = Logging(context.system, this)

  private val nodeNames = new mutable.HashSet[String]

  override def receive: Receive = {
    case NodeNameCommand(true, name) => {
      nodeNames.add(name)
      log.debug("Added node {}.", name)
    }
    case NodeNameCommand(false, name) => {
      nodeNames.remove(name)
      log.debug("Removed node {}.", name)
    }
    case ManualTweetCommand(sender, text) => sender ! generateMessage(text)
    case RandomTweetCommand(sender) => {
      if (nodeNames.nonEmpty) {
        sender ! generateRandomMessage()
      }
    }
  }

  private def generateMessage(text: String): Tweet = {
    log.info("Generated message: {}", text)
    new Tweet(Calendar.getInstance(), myName, text)
  }

  private def generateRandomMessage(): Tweet = {
    val text = Random.shuffle(messageTemplates.toList).head.split(" ").map(w => w match {
      case "#" => Random.shuffle(nodeNames).head
      case _ => w
    }).mkString(" ")

    log.info("Generated random message: {}", text)

    return new Tweet(Calendar.getInstance(), myName, text)
  }
}

class TweetScheduler(generator: ActorRef, listener: ActorRef) extends Actor {

  import context.dispatcher

  private val tick = context.system.scheduler.schedule(5.seconds, 7.seconds, self, "tick")

  private var enabled = false

  override def postStop() = tick.cancel()

  def receive = {
    case "tick" => if (enabled) generator ! new RandomTweetCommand(sender())
    case "enable" => enabled = true
    case "disable" => enabled = false
    case t: Tweet => listener ! t
  }
}
