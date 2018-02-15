open System

let parseLine (_: unit) =  
    (System.Console.ReadLine()).Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.map int

let isBetween a b x =
    (a,b) ||> Seq.forall2 (fun ai bi -> x % ai = 0 && bi % x = 0)

[<EntryPoint>]
let main _ =
    let n::m::[] = parseLine () |> Seq.toList
    if n<1 || m >10 then
        failwith "n or m parse exception"
    let a = parseLine() |> Seq.map(fun e -> if e < 1 || e > 100 then failwith "ex" else e) |> Seq.toList
    if a.Length > n then
        failwith "a too long exception"
    let b = parseLine() |> Seq.map(fun e -> if e < 1 || e > 100 then failwith "ex" else e) |> Seq.toList
    if b.Length > m then
        failwith "b too long exception"
    let max = a @ b |> Seq.distinct |> Seq.max
    let input = [1 .. max] |> Seq.filter(fun x -> max % x = 0)
    let result = input |> Seq.where ((a,b) ||> isBetween) |> Seq.fold (fun a _ -> a + 1) 0
    printfn "%d" result
    0 // return an integer exit code
