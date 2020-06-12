namespace HP.Web

open System

module Main =

    [<EntryPoint>]
    let main argv =

        printfn "Hello Web Programming!"

        // JSON Serialization
        // HP.Json.BuiltIn.run ()

        // GraphQL Hello world
        // Graph1.run ()

        // Reflection playground
        HP.Json.DynamicReflection.run ()

        0 // return an integer exit code
