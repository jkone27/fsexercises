open System

let parseLine (_: unit) =  
    (System.Console.ReadLine()).Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.map int

let distanceFromTree treeCenter relativeDistance =
    treeCenter + relativeDistance 

let inGarden a b x =
    if not (a > 0 && b < 100000) then
        failwith "invalid range"
    x >= a && x <= b

let validateDistance distance =
    if (distance < -100000) || (distance > 100000) then 
        failwith "invalid distance"
    distance

let fruitsInGarden fruits isInGarden = 
    fruits 
    |> Seq.filter isInGarden
    |> Seq.fold (fun a _ -> a + 1) 0

[<EntryPoint>]
let main _ = 
    //printfn "input: sam start\" \"sam stop"
    let samStart::samStop::[] = parseLine () |> Seq.toList
    let inSamsGarden = inGarden samStart samStop
    //printfn "input: orange tree\" \"apple tree"
    let appleTree::orangeTree::[] = parseLine () |> Seq.toList
    //printfn "input: oranges\" \"apples"
    let numberOfApples::numberOfOranges::[] = parseLine () |> Seq.toList
    //printfn "read oranges distance"
    let apples = parseLine () |> Seq.map validateDistance |> Seq.map (distanceFromTree appleTree) |> Seq.toList
    if apples |> Seq.length <> numberOfApples then
        failwith "number of apples is wrong"
    //printfn "read apples distance"
    let oranges = parseLine () |> Seq.map validateDistance |> Seq.map (distanceFromTree orangeTree) |> Seq.toList
    if oranges |> Seq.length <> numberOfOranges then
        failwith "number of oranges is wrong"
    let orangesInSamGarden = fruitsInGarden oranges inSamsGarden
    let applesInSamGarden = fruitsInGarden apples inSamsGarden
    //printfn "results: apples distance"
    printfn "%d\r\n%d" applesInSamGarden orangesInSamGarden 
    Console.ReadKey() |> ignore
    0

