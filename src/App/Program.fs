namespace HP

open System

module Program =

    [<EntryPoint>]
    let main argv =

        // 1. First program - Calculator
        printfn "Welcome to the great calculator"

        Calculator.run ()

        0 // return an integer exit code
