namespace HP.Web

open System

module Main =

    [<EntryPoint>]
    let main argv =

        printfn "Hello GraphQL!"

        HP.Json.BuiltIn.run ()

        // Graph1.run ()

        0 // return an integer exit code
