namespace HP.Euler


module Multiple3And5 =

    let aggregation maxNum =
        let list = [ 1..maxNum ]

        List.fold (fun acc next ->
            if next % 3 = 0 || next % 5 = 0
                then
                    printfn "identified %i" next
                    acc + next
                else acc) 0 list


    let run () =
        printf "Sum of natural numbers below 1000 that are divisible by 3 or 5 are: %i" <| aggregation 999
