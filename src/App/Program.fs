namespace HP

open System

module Program =

    [<EntryPoint>]
    let main argv =

        // 1. Calculator
        // printfn "Welcome to the great calculator"
        // Calculator.run ()


        // 2. Leap year
        // printfn "Welcome to the Leap year calculator"
        // LeapYear.run ()


        // 3. Queen attack
        // printfn "Welcome to the Queen attack problem"
        // TwoQueens.run ()

        // 4. Binary Search Tree
        // printfn "Welcome to the BST program"
        // DataStructure.BST.run ()


        // 5. Euler 01 - Multiples of 3 and 5
        // Euler.Multiple3And5.run ()

        // 6. Euler 02 - Even Fibonacci numbers
        Euler.EvenFibonacci.run ()

        // return an integer exit code
        0
