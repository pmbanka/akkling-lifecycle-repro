# akkling-lifecycle-repro
```
Microsoft (R) F# Interactive version 14.0.23413.0
Copyright (c) Microsoft Corporation. All Rights Reserved.

For help type #help;;

> # silentCd @"c:\repos\akkling-lifecycle-repro";;
- # 1 @"c:\repos\akkling-lifecycle-repro\src.fsx"
- ;;
> /// > .paket\paket.exe generate-load-scripts --group Main --framework net461 --type fsx
-
- #load @".paket/load/net461/main.group.fsx"
-
- open System
- open FSharp.Data
- open System.Text
- open Akka.Actor
- open Akka.Pattern
- open Akkling
- open Akkling.Extensions
- open System.Threading.Tasks
-
- let config =
-        """
-         akka {
-             stdout-loglevel = DEBUG
-             loglevel = DEBUG
-             log-config-on-start = off
-             actor {
-                 debug {
-                       receive = on
-                       autoreceive = on
-                       lifecycle = on
-                       event-stream = on
-                       unhandled = on
-                 }
-             }
-         }
-       """
-       |> Configuration.parse
-
- let system = System.create "system" <| config.WithFallback(Configuration.defaultConfig ())
-
- let simpleActor (mailbox : Actor<_>) =
-     let rec loop () =
-         actor {
-             let! (message : obj) = mailbox.Receive()
-             match message with
-             | :? string as s ->
-                 printfn "Hello %s" s
-                 return! loop ()
-             | _ -> return Unhandled
-         }
-     loop ()
-
- let aref = spawnAnonymous system (props simpleActor)
-
- ;;
[Loading c:\repos\akkling-lifecycle-repro\.paket\load\net461\main.group.fsx]

namespace FSI_0002.Main

Binding session to 'c:\repos\akkling-lifecycle-repro\.paket\load\net461\../../../packages/Akka/lib/net45/Akka.dll'...
Binding session to 'C:\Program Files (x86)\Reference Assemblies\Microsoft\FSharp\.NETFramework\v4.0\4.3.1.0\FSharp.Core.dll'...
Binding session to 'c:\repos\akkling-lifecycle-repro\.paket\load\net461\../../../packages/Akka/lib/net45/Akka.dll'...
Binding session to 'c:\repos\akkling-lifecycle-repro\.paket\load\net461\../../../packages/System.Collections.Immutable/lib/netstandard1.0/System.Collections.Immutable.dll'...
[DEBUG][22/05/2018 13:44:09][Thread 0001][EventStream] subscribing [akka://all-systems/] to channel Akka.Event.Debug
[DEBUG][22/05/2018 13:44:09][Thread 0001][EventStream] subscribing [akka://all-systems/] to channel Akka.Event.Info
[DEBUG][22/05/2018 13:44:09][Thread 0001][EventStream] subscribing [akka://all-systems/] to channel Akka.Event.Warning
[DEBUG][22/05/2018 13:44:09][Thread 0001][EventStream] subscribing [akka://all-systems/] to channel Akka.Event.Error
[DEBUG][22/05/2018 13:44:09][Thread 0001][EventStream] StandardOutLogger started
Binding session to 'c:\repos\akkling-lifecycle-repro\.paket\load\net461\../../../packages/Akka.Serialization.Hyperion/lib/net45/Akka.Serialization.Hyperion.dll'...
Binding session to 'c:\repos\akkling-lifecycle-repro\.paket\load\net461\../../../packages/Akka.Serialization.Hyperion/lib/net45/Akka.Serialization.Hyperion.dll'...
Binding session to 'c:\repos\akkling-lifecycle-repro\.paket\load\net461\../../../packages/Hyperion/lib/net45/Hyperion.dll'...
Binding session to 'c:\repos\akkling-lifecycle-repro\.paket\load\net461\../../../packages/Newtonsoft.Json/lib/net45/Newtonsoft.Json.dll'...
[DEBUG][22/05/2018 13:44:10][Thread 0010][akka://system/user] Started (Akka.Actor.GuardianActor)
[DEBUG][22/05/2018 13:44:10][Thread 0006][akka://system/] Started (Akka.Actor.GuardianActor)
[DEBUG][22/05/2018 13:44:10][Thread 0012][akka://system/system] Started (Akka.Actor.SystemGuardianActor)
[DEBUG][22/05/2018 13:44:10][Thread 0006][akka://system/] now supervising akka://system/user
[DEBUG][22/05/2018 13:44:10][Thread 0006][akka://system/] now supervising akka://system/system
[DEBUG][22/05/2018 13:44:10][Thread 0009][akka://system/user] now watched by [akka://system/system]
[DEBUG][22/05/2018 13:44:10][Thread 0012][akka://system/system] now watched by [akka://system/]
[DEBUG][22/05/2018 13:44:10][Thread 0004][akka://system/system/log1-DefaultLogger] Started (Akka.Event.DefaultLogger)
[DEBUG][22/05/2018 13:44:10][Thread 0013][akka://system/system] now supervising akka://system/system/log1-DefaultLogger
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream] subscribing [akka://system/system/log1-DefaultLogger#643877759] to channel Akka.Event.Debug
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream] subscribing [akka://system/system/log1-DefaultLogger#643877759] to channel Akka.Event.Info
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream] subscribing [akka://system/system/log1-DefaultLogger#643877759] to channel Akka.Event.Debug
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream] subscribing [akka://system/system/log1-DefaultLogger#643877759] to channel Akka.Event.Info
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream] subscribing [akka://system/system/log1-DefaultLogger#643877759] to channel Akka.Event.Warning
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream] subscribing [akka://system/system/log1-DefaultLogger#643877759] to channel Akka.Event.Error
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream(system)] Logger log1-DefaultLogger [DefaultLogger] started
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream] subscribing [akka://system/system/UnhandledMessageForwarder#1580397114] to channel Akka.Event.UnhandledMessage
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream(system)] StandardOutLogger being removed
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream] unsubscribing [akka://all-systems/] from all channels
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream] subscribing [akka://system/system/log1-DefaultLogger#643877759] to channel Akka.Event.Warning
[DEBUG][22/05/2018 13:44:10][Thread 0009][akka://system/system] now supervising akka://system/system/UnhandledMessageForwarder
[DEBUG][22/05/2018 13:44:10][Thread 0012][akka://system/system/UnhandledMessageForwarder] Started (Akka.Event.LoggingBus+UnhandledMessageForwarder)
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream] subscribing [akka://system/system/log1-DefaultLogger#643877759] to channel Akka.Event.Error
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream(system)] Logger log1-DefaultLogger [DefaultLogger] started
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream] subscribing [akka://system/system/UnhandledMessageForwarder#1580397114] to channel Akka.Event.UnhandledMessage
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream(system)] StandardOutLogger being removed
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream] unsubscribing [akka://all-systems/] from all channels
[DEBUG][22/05/2018 13:44:10][Thread 0009][akka://system/system] now supervising akka://system/system/UnhandledMessageForwarder
[DEBUG][22/05/2018 13:44:10][Thread 0012][akka://system/system/UnhandledMessageForwarder] Started (Akka.Event.LoggingBus+UnhandledMessageForwarder)
[DEBUG][22/05/2018 13:44:10][Thread 0001][EventStream(system)] Default Loggers started
[DEBUG][22/05/2018 13:44:10][Thread 0010][akka://system/system] now supervising akka://system/system/deadLetterListener
[DEBUG][22/05/2018 13:44:10][Thread 0004][EventStream] subscribing [akka://system/system/deadLetterListener#790634790] to channel Akka.Event.DeadLetter
[DEBUG][22/05/2018 13:44:10][Thread 0004][akka://system/system/deadLetterListener] Started (Akka.Event.DeadLetterListener)
[DEBUG][22/05/2018 13:44:10][Thread 0004][akka://system/system] now supervising akka://system/system/EventStreamUnsubscriber-1
[DEBUG][22/05/2018 13:44:10][Thread 0004][EventStreamUnsubscriber] registering unsubscriber with Akka.Event.EventStream
[DEBUG][22/05/2018 13:44:10][Thread 0004][akka://system/system/EventStreamUnsubscriber-1] Started (Akka.Event.EventStreamUnsubscriber)
[DEBUG][22/05/2018 13:44:10][Thread 0005][akka://system/user] now supervising akka://system/user/$a

val config : Akka.Configuration.Config =
    akka : {
    stdout-loglevel : DEBUG
    loglevel : DEBUG
    log-config-on-start : off
    actor : {
      debug : {
        receive : on
        autoreceive : on
        lifecycle : on
        event-stream : on
        unhandled : on
      }
    }
  }

val system : Akka.Actor.ActorSystem
val simpleActor :
  mailbox:Akkling.Actors.Actor<obj> -> Akkling.Actors.Effect<obj>
val aref : Akkling.ActorRefs.IActorRef<obj> = [akka://system/user/$a#26516896]

> [DEBUG][22/05/2018 13:44:10][Thread 0005][akka://system/user/$a] Started (Akkling.Actors+FunActor`1[System.Object])
[ERROR][22/05/2018 13:44:10][Thread 0003][akka://system/system/UnhandledMessageForwarder] Object reference not set to an instance of an object.
Cause: System.NullReferenceException: Object reference not set to an instance of an object.
   at Akka.Event.LoggingBus.UnhandledMessageForwarder.ToDebug(UnhandledMessage message)
   at Akka.Event.LoggingBus.UnhandledMessageForwarder.Receive(Object message)
   at Akka.Actor.ActorBase.AroundReceive(Receive receive, Object message)
   at Akka.Actor.ActorCell.ReceiveMessage(Object message)
   at Akka.Actor.ActorCell.Invoke(Envelope envelope)
[DEBUG][22/05/2018 13:44:10][Thread 0003][akka://system/system/UnhandledMessageForwarder] Restarting
[DEBUG][22/05/2018 13:44:10][Thread 0003][akka://system/system/UnhandledMessageForwarder] Restarted (Akka.Event.LoggingBus+UnhandledMessageForwarder)

```
