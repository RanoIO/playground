namespace HP.ToDo

// Learn more about F# at http://fsharp.org

open System

module Main =

    [<EntryPoint>]
    let main argv =

        // It is recommended that objects supporting the IDisposable interface
        // are created using the syntax 'new Type(args)', rather than 'Type(args)'
        // or 'Type' as a function value representing the constructor, to indicate
        // that resources may be owned by the generated value
        let cts = new Threading.CancellationTokenSource()
        let config = Server.config cts.Token
        let webpart = Server.make ()
        let listening, server = Server.run config webpart

        Async.Start(server, cts.Token)

        printfn "Server is now running"

        // Exit on enter
        Console.ReadLine() |> ignore

        cts.Cancel()
        0
