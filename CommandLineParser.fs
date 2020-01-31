namespace CLArgs

module CommandLineParser =
    
    open Argu
    
    type CLIArguments =
        | Working_Directory of path:string
        | Listener of host:string * port:int
        | Data of base64:byte[]
        | Port of tcp_port:int
        | Log_Level of level:int
        | Detach
    with
        interface IArgParserTemplate with
            member s.Usage =
                match s with
                | Working_Directory _ -> "Specify a working directory"
                | Listener _ -> "Specify a listener (hostname : port)"
                | Data _ -> "Specify data in base64 encoding."
                | Port _ -> "Specify a TCP port number"
                | Log_Level _ -> "Set the log level."
                | Detach _ -> "Detach daemon from console"
    
    let parseCommandLineOptions argv =
        try
            let parser = ArgumentParser.Create<CLIArguments>(programName = "CommandLineArgs.exe")
            let parsedOrNot = Some((parser.Parse argv).GetAllResults()) 
            match parsedOrNot.IsNone with
            | true -> Error "Command line arguments are null."
            | _ -> Ok parsedOrNot.Value
        with
            | :? Argu.ArguParseException as ex -> printfn "%s" ex.Message ; Error ex.Message
