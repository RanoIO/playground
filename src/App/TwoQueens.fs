namespace HP

open System

module TwoQueens =

    // REPL - READ EVAL PRINT LOOP

    // A position on board
    type Position = int * int

    type Game =
        { queen1: Position
          queen2: Position }


    let read () =
        printf "Enter first queen (eg: 1 2): "
        let position1 = Console.ReadLine()

        printf "Enter second queen (e.g: 4 5): "
        let position2 = Console.ReadLine()

        (position1, position2)


    let parsePosition (raw1: string) : Result<Position, string> =
        let arr = raw1.Split(' ')
        match arr with
        | [| r; c |] ->
            let row = Helper.tryInteger r
            let column = Helper.tryInteger c
            match row, column with
            | Some x, Some y ->
                if x > 0 && x < 9 && y > 0 && y < 9 then Ok (x, y)
                else Error "Invalid range"
            | _ -> Error "Invalid values"
        | _ -> Error "Invalid format"


    let parse (raw1, raw2) : Result<Game, string> =
        let queen1 = parsePosition raw1
        let queen2 = parsePosition raw2

        match queen1, queen2 with
        | Ok q1, Ok q2 -> Ok { queen1 = q1; queen2 = q2 }
        | Error err, Ok -> Error <| "First queen's position invalid - " + err
        | Ok, Error err -> Error <| "Second queen's position invalid - " + err
        | Error, Error -> Error "Both queens positions are invalid"


    let eval (game: Game) =
        let r1, c1 = game.queen1
        let r2, c2 = game.queen2

        match (r1, c1, r2, c2) with
        | _ when r1 = r2 && c1 = c2 ->
                false
        | _ when r1 = r2 ->
                true // In same row
        | _ when c1 = c2 ->
                true // In same column
        | _ ->
                (r2 - r1) / (c2 - c1)
                |> Math.Abs
                |> (fun x -> x = 1)


    let print (result: Result<bool, string>) =
        match result with
        | Ok true -> printfn "Queens can attack each other"
        | Ok false -> printfn "Queens cannot attack each other"
        | Error err -> printfn "Invalid Value: %s" err


    let continueLoop () =
        printf "Press y to continue: "
        let key = Console.ReadKey()
        printfn ""
        key


    let rec loop () =
        read()
        |> parse
        |> Result.map eval
        |> print
        |> continueLoop
        |> (fun x ->
            match x.KeyChar with
            | 'y' -> loop ()
            | _ -> ())


    let run () = loop ()
