open CLArgs

[<EntryPoint>]
let main argv =
    
    printfn "Parse command line args in F#"
    
    let result = CommandLineParser.parseCommandLineOptions argv
    match result with
    | Ok parsed -> printf "%A" parsed
    | Error e -> printf "%s" e
        
    0 // return an integer exit code
