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



module EvenFibonacci =

    let generator2 (maxValue: int64) =
        seq {
            let mutable x1 = 0L
            let mutable x2 = 1L

            printf "Running Generator"

            for i = 0 to System.Int32.MaxValue do
                let newVal = x1 + x2

                printf "Running Generator - For loop"

                x1 <- x2
                x2 <- newVal

                yield newVal
        }
        |> Seq.takeWhile (fun next -> next < maxValue)


    let generator (maxValue: int64) =
        Seq.unfold
            (fun (n1, n2) ->
                let add = n1 + n2;
                Some (add, (n2, add)))
            (0L, 1L)
        |> Seq.takeWhile (fun next -> next < maxValue)


    let sumOfEven seq =
        seq
        |> Seq.filter (fun x -> x % 2L = 0L)
        |> Seq.fold (+) 0L


    let run () =
        let sum1 = 4000000L |> generator |> sumOfEven
        let sum2 = 4000000L |> generator2 |> sumOfEven

        printfn "Sum of even fibonacci numbers below 4 million is: %i %i" sum1 sum2
