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
        let sum = 4000000L |> generator |> sumOfEven

        printf "Sum of even fibonacci numbers below 4 million is: %i" sum
