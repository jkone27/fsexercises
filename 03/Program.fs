// Learn more about F# at http://fsharp.org
open System

let parseLine (_: unit) =  
    (System.Console.ReadLine()).Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.map int

/// Inclusive 'between' operator:
let (>=<) x (min, max) =
    (x >= min) && (x <= max)

let createTimerAndObservable timerInterval =
    // setup a timer
    let timer = new System.Timers.Timer(float timerInterval)
    timer.AutoReset <- true

    // events are automatically IObservable
    let observable = timer.Elapsed  

    // return an async task
    let task = async {
        timer.Start()
        do! Async.Sleep 5000
        timer.Stop()
        }

    // return a async task and the observable
    (task,observable)

let eagerSequence max start step = seq { for i in 0 .. max-1 do yield start + i*step }

[<EntryPoint>]
let main _ =
    let x1::v1::x2::v2::[] = parseLine () |> Seq.toList
    if not (x1 >=< (0,x2) && x2 >=< (x1,10000)) then
        failwith "exception x"
    if not (v1 >= 1 && v2 <= 10000) then
        failwith "exception v"
    let c1 = eagerSequence 10000 x1 v1
    let c2 = eagerSequence 10000 x2 v2
    let hasMatch = (c1,c2) ||> Seq.exists2 (=)
    match hasMatch with
    |true -> printfn "YES"
    |_ -> printfn "NO"
    0
