//every studente receives a grade between 1 and 100. every grade less than 40 is a fail.
//less than 38 no rounding, otherwise
//If the difference between the grade and the next multiple of 5 is less 3 than , 
//round up to the next multiple of 5 .
let nextMultipleOf c n = 
    let reminder = c%n
    c + (5 - reminder)

let roundGrade current =
    if current >= 38 then
        let nextMultipleOfFive = nextMultipleOf current 5 
        if nextMultipleOfFive - current < 3 then
            nextMultipleOfFive
        else current
    else
        current

let inline (?^) n s = s |> Seq.contains n

let printVal v = printfn "%d" v

[<EntryPoint>]
let main argv =
    printfn "how many students?"
    let parseLine (_: unit) = int (System.Console.ReadLine())
    let n = parseLine ()
    if (not (n ?^ [1..60])) then
        failwith "out of range"
    printfn "grade students"
    let grades = 
        [1 .. n] 
        |> Seq.map (
            (fun _ -> parseLine ()) >> 
            (fun z -> 
                (printfn "registered grad for student = %i" z) ;
                if not (z ?^ [0..100]) then
                    failwith "out of range"
                ; z)) 
        |> Seq.toArray
    let rounded = 
        grades 
        |> Seq.map roundGrade
    printfn "grades"
    for g in grades do
        printVal g
    printfn "passing grades"
    for p in rounded do
        printVal p
    System.Console.ReadKey() |> ignore
    0