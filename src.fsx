/// > .paket\paket.exe generate-load-scripts --group Main --framework net461 --type fsx

#load @".paket/load/net461/main.group.fsx"

open System
open FSharp.Data
open System.Text
open Akka.Actor
open Akka.Pattern
open Akkling
open Akkling.Extensions
open System.Threading.Tasks

let config =
       """
        akka {                
            stdout-loglevel = DEBUG
            loglevel = DEBUG
            log-config-on-start = off 
            actor {                
                debug {  
                      receive = on 
                      autoreceive = on
                      lifecycle = on
                      event-stream = on
                      unhandled = on
                }
            }  
        }
      """
      |> Configuration.parse 

let system = System.create "system" <| config.WithFallback(Configuration.defaultConfig ())

let simpleActor (mailbox : Actor<_>) =
    let rec loop () =
        actor {
            let! (message : obj) = mailbox.Receive()
            match message with
            | :? string as s -> 
                printfn "Hello %s" s
                return! loop ()
            | _ -> return Unhandled
        }
    loop ()

let aref = spawnAnonymous system (props simpleActor)
