open System
//6
//0 9 3 10 2 20
//
//3 0

let parseLine (_: unit) =  
    (System.Console.ReadLine()).Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.map int

let record scores recordSelector =
    scores |> List.fold (fun a n -> 
        if (a |> List.isEmpty) || (recordSelector a n) then
            n :: a
        else
            a
    ) []

[<EntryPoint>]
let main _ =
    let n = (int) (System.Console.ReadLine())
    if n < 1 || n > 1000 then
        failwith "n parse exception"
    let scores = 
        parseLine () 
        |> Seq.map (fun e -> 
            if e < 0 || (e > (int) 10e8) then  
                failwith "score parse exception" 
            else e) 
        |> Seq.toList
    let lowRecords = record scores (fun a n -> n < List.min a)
    let highRecords = record scores (fun a n -> n > List.max a)
    let lows = (lowRecords |> Seq.length) - 1
    let higs = (highRecords |> Seq.length) - 1
    printfn "%d %d" higs lows
    0
