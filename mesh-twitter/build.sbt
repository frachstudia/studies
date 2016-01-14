name := "meshtwitter"

version := "1.0-SNAPSHOT"

scalaVersion := "2.11.4"

libraryDependencies ++= Seq(
  "junit" % "junit" % "4.11" % "test",
  "org.scalatest" %% "scalatest" % "2.2.1" % "test",
  "com.typesafe.akka" % "akka-actor_2.11" % "2.3.4",
  "com.typesafe.akka" % "akka-remote_2.11" % "2.3.4"
)

//resolvers += Resolver.mavenLocal

resolvers += "Typesafe Repository" at "http://repo.typesafe.com/typesafe/releases/"

//publishTo := Some(Resolver.file("file", new File(Path.userHome.absolutePath + "/.m2/repository")))
