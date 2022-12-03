open System.IO
open System.Collections.Generic
open System.Text.RegularExpressions

// Recursivle reads filepath and returns it as string
let rec readFile path = 
    let text = File.ReadAllText(path)
    text

[<EntryPoint>]
let main argv =

    let text = readFile argv[0]

    // Text to lowercase
    let textToLower = text.ToLower()

    // Filter words
    // TODO: The "" content is also counted
    let filteredText = Regex.Replace(textToLower, "[^A-Za-z0-9\\s]", "")

    // Splits string into String Array
    let words = 
        filteredText.Split(' ', '\n') 
        |> Array.map (fun s -> s.Trim())
    
    // Iterates through Array and sum duplicates
    let dict = Dictionary<_,_>()
    words
    |> Array.iter (fun w -> 
        match dict.TryGetValue w with
        | true, v -> dict.[w] <- v + 1
        | false, _ -> dict.[w] <- 1)

    // Creates a sequence of tuples, with (word,count) in order
    let wordMap =
        dict
        |> Seq.sortBy (fun kvp -> -kvp.Value, kvp.Key)
        |> Seq.map (fun kvp -> kvp.Value, kvp.Key)
           
    // Maps sequence in <int, string> format
    let lines = wordMap |> Seq.map (fun (c, w) -> sprintf "%i %s" c w)

    // Writes word map into output file
    File.WriteAllLines(argv[0] + "\output.txt", lines)
    
    printfn "TEST: A file has been written to %s" argv[1]

    0

