import java.net.InetSocketAddress

import akka.actor.{Actor, ActorRef, ActorSystem, Props}

import scala.collection.mutable

object Main {

  def main(args: Array[String]) {

    if (args.length != 2 && args.length != 4) {
      println("usage: " +
        "./run [clientName] [clientPort]\n" +
        "or: " +
        "./run [clientName] [clientPort] [firstNeighbourHostname] [firstNeighbourPort]")
      return
    }

    if (!args(1).matches("^\\d*$") || (args.length == 4 && !args(3).matches("^\\d*$"))) {
      println("Bad port number.")
      return
    }

    val system = ActorSystem("MT")
    val nodeName = args(0)
    val localPort = args(1).toInt
    val callbackHandler = system.actorOf(Props(new ConsoleCallbackHandler))

    val tweetGenerator = system.actorOf(Props(new TweetGenerator(nodeName)))

    val me = new RemoteNode(nodeName, new InetSocketAddress("localhost", localPort))
    val senderActors = new mutable.HashMap[String, ActorRef]()

    val tweetGatherer = system.actorOf(Props(new Gathering(callbackHandler, senderActors, nodeName)))

    val mainNode = system.actorOf(Props(args.length match {
      case 2 => new LocalNode(
        me,
        senderActors,
        callbackHandler,
        tweetGenerator,
        tweetGatherer)

      case 4 => {
        val remoteHost = args(2)
        val remotePort = args(3).toInt
        new LocalNode(
          me,
          senderActors,
          callbackHandler,
          tweetGenerator,
          tweetGatherer,
          remoteHost,
          remotePort)
      }
    }))

    val tweetScheduler = system.actorOf(Props(new TweetScheduler(tweetGenerator, tweetGatherer)))
    system.actorOf(Props(new Listener(localPort, mainNode)))

    while (true) {
      try {
        print(nodeName + ":" + localPort + "% ")

        val command: Array[String] = scala.io.StdIn.readLine().trim().split(" +")

        command(0) match {
          case "e" | "exit" | "disconnect" => {
            println("Sending Disconnect message to peers.")
            mainNode ! new DisconnectCommand
            mainNode ! new ExitCommand
            Thread.sleep(1000)
            println("Exiting.")
            return
          }
          case "g" | "gather" => {
            if (command.length == 3 && command(2).matches("^\\d*$")) {
              tweetGatherer ! new GatherCommand(command(1), command(2).toInt)
            } else
              println("Wrong arguments. ")
          }
          case "s" | "show" => {
            mainNode ! new ShowRoutingCommand
          }
          case "t" | "tweet" => {
            command.length match {
              case 1 => println("No text provided")
              case _ => tweetGenerator !
                new ManualTweetCommand(tweetGatherer, command.slice(1, command.length).mkString(" "))
            }
          }
          case "a" | "auto" => {
            command match {
              case Array("auto", "true") => tweetScheduler ! "enable"
              case Array("auto", "false") => tweetScheduler ! "disable"
              case _ => println("Wrong arguments.")
            }
          }
          case "" => println
          case "connect" =>
            if (command.length == 3 && command(2).matches("^\\d*$")) {
              mainNode ! new ConnectToMeCommand(command(1), command(2).toInt)
            } else
              println("Wrong arguments. ")
          case _ => println("Available commands:\n" +
            "\tshow\n" +
            "\texit\n" +
            "\ttweet [text]\n" +
            "\tauto [true/false]\n" +
            "\tgather [target] [number of tweets]\n" +
            "\trouting [routing/flooding]\n" +
            "\tconnect [host] [port]\n")
        }
        Thread.sleep(100)
      } catch {
        case e: Exception => {
          e.printStackTrace()
          println("An error occurred. Exiting.")
          return
        }
      }
    }
  }

  class ConsoleCallbackHandler extends Actor {
    override def receive: Receive = {
      case s: ShowRoutingReplyCommand => {
        if (s.routingTable.isEmpty) {
          println("Nobody is connected.")
        }
        s.routingTable.foreach(n => {
          print("\t" + n._1 + " -> ")
          n._2.foreach(n => print(n + " "))
          println
        })
      }
      case s: ShowGatheredTweetsCommand => {
        println
        println("### " + s.messages.size + " messages received.")
        println
        s.messages.foreach(t => {
          println("* " + t.nodeName + " (" + t.date.getTime + ")")
          println("* " + t.text)
          println
        })
        println
      }
      case _ => throw new Exception("invalid command")
    }
  }

}
