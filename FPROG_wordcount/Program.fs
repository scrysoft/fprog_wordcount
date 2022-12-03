open System.IO
open System.Collections.Generic
open System.Text.RegularExpressions
open System

// Recursivle reads filepath and returns it as string
let rec readFile path = 
    let text = File.ReadAllText(path)
    text

[<EntryPoint>]
let main argv =
    // Reads all files in a given directory with a given fileextension
    let files = Directory.GetFiles(argv[0], "*." + argv[1])
    let splitFiles = Seq.map(fun f -> readFile f) files  |> Seq.concat |> Seq.map string |> Seq.toArray
    let text = String.Join("", splitFiles)

    // Text to lowercase
    let textToLower = text.ToLower()

    // Filter words
    let filteredText = Regex.Replace(textToLower, "[^A-Za-z0-9\\s]", "")

    // Splits string into String Array
    let words = 
        filteredText.Split(' ', '\n') 
        |> Array.filter (String.IsNullOrWhiteSpace >> not)
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
    File.WriteAllLines(argv[0] + "/output.txt", lines)
    
    // printfn "TEST: A file has been written to %s" argv[1]
    0

