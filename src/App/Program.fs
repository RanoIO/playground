namespace HP

open System

module Program =

    [<EntryPoint>]
    let main argv =

        // 1. First program - Calculator
        // printfn "Welcome to the great calculator"
        // Calculator.run ()

        // 2. Seconf progrm - Leap year
        printfn "Welcome to the Leap year calculator"
        LeapYear.run ()

        // return an integer exit code
        0
