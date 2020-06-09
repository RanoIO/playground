namespace HP


module QuickSort =

    // This can be written as point-free function. But avoid it always.
    let partition pivot list = List.partition (fun x -> x <= pivot) list

    let rec sort list =
        match list with
        | [] -> []
        | [ x ] -> [ x ]
        | [ x; y ] -> if x < y then [ x; y ] else [ y; x ]
        | pivot :: tail ->
            let (left, right) = partition pivot tail
            let sortedLeft = sort left
            let sortedRight = sort right

            sortedLeft @ pivot :: sortedRight


    let example list =
        printfn "Original list: %A" list
        printfn "Sorted list: %A\n" <| sort list


    let run () =
        printfn "Quick sort examples"
        example [ 100; 90; 80; 70; 60; 50; 40; 30; 20; 10 ]
        example [ 10; 20; 30; 40; 50; 60; 70; 80; 90 ]
        example [ 50; 30; 80 ]
        example [ 100L; 400L; 250L; 200L; 30L ]



module MergeSort =

    let merge left right =
        // This final + inner recursion is not exactly CPS but similar to that.
        let rec recur final left right =

            // printfn "Recursion %A %A %A" final left right

            match (left, right) with
            | [], [] -> final
            | [], right -> List.rev final @ right
            | left, [] -> List.rev final @ left
            | x :: xs, y :: _ when x < y ->
                recur (x :: final) xs right
            | _, y :: ys ->
                recur (y :: final) left ys

        recur [] left right


    // List with 2 items feels more like a short-circuit/optimization step
    let rec sort list =
        match list with
        | [] -> []
        | [ x ] -> [ x ]
        | [ x; y ] -> if x < y then [ x; y ] else [ y; x ]
        | _ ->
            let mid = List.length list / 2
            let (first, last) = List.splitAt mid list

            // printfn "Split %A %A" first last

            let sortedFirst = sort first
            let sortedLast = sort last

            merge sortedFirst sortedLast


    let example list =
        printfn "Original list: %A" list
        printfn "Sorted list: %A\n" <| sort list


    let run () =
        printfn "Merge sort examples"
        example [ 100; 90; 80; 70; 60; 50; 40; 30; 20; 10 ]
        example [ 10; 20; 30; 40; 50; 40; 60; 70; 80; 90 ]
        example [ 50; 30; 80 ]
        example [ 100L; 400L; 250L; 200L; 30L; 800L ]
