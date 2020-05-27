namespace HP

open System

module LeapYear =

    // REPL - READ EVAL PRINT LOOP

    let read (): string =
        printf "\nEnter the leap year (e for exit): "
        Console.ReadLine()


    let parse raw = Helper.tryInteger raw


    // Business logic
    let eval (year: int): bool =
        if year % 400 = 0 then true
        else year % 4 = 0 && year % 100 <> 0


    // Output
    let print (result: bool): unit =
        if result then printfn "Given year is leap year"
        else printfn "Not a leap year"


    let evalBusinessLogic raw =
        raw
        |> parse
        |> Option.map eval
        |> Helper.mapError (fun _ -> printfn "Invalid Input \n")
        |> Option.map print
        |> Helper.always ()


    // Program loop
    let rec loop () =
        read ()
        |> (fun input ->
            match input with
            | "e" -> printfn "Bye bye\n"
            | input -> evalBusinessLogic input |> loop)


    let run () = loop ()
