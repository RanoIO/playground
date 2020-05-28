namespace HP

open System

module Program =

    [<EntryPoint>]
    let main argv =

        // 1. First program - Calculator
        // printfn "Welcome to the great calculator"
        // Calculator.run ()

        // 2. Second program - Leap year
        // printfn "Welcome to the Leap year calculator"
        // LeapYear.run ()


        // 3. Third program - Queen attack
        printfn "Welcome to the Queen attach problem"
        TwoQueens.run ()

        // return an integer exit code
        0
