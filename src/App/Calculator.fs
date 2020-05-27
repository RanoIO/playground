namespace HP

open System


module Calculator =

    // REPL - Read Eval Print Loop (Calculator)

    type Operation =
        | Add
        | Sub
        | Mul
        | Div

    type Command =
        | Math of Operation
        | Exit
        | Invalid


    let readOperand () = Console.ReadLine() |> Helper.tryDouble


    let parse (value: string): Command =
        match value with
        | "1" -> Math Add
        | "2" -> Math Sub
        | "3" -> Math Mul
        | "4" -> Math Div
        | "5" -> Exit
        | ___ -> Invalid


    // Input
    let read () =
        printfn "Choose your operation"
        printfn "1. Addition"
        printfn "2. Substraction"
        printfn "3. Multiplication"
        printfn "4. Division"
        printfn "5. Exit"
        printf "Make selection: "
        Console.ReadLine()


    // Business logic
    let eval (operation: Operation) (operand1: float) (operand2: float) =
        match operation with
        | Add -> operand1 + operand2
        | Sub -> operand1 - operand2
        | Mul -> operand1 * operand2
        | Div -> operand1 / operand2


    // Output
    let print (result: float) =
        printfn "Result of operation is: %f\n" result
        ()


    let evalMathCommand (o: Operation) =
        printf "First operand: "
        let o1 = readOperand ()

        printf "Second operand: "
        let o2 = readOperand ()

        Option.map2 (eval o) o1 o2


    let doExit () =
        printfn "Bye bye!\n"


    // The loop
    let rec loop () =
        read ()
        |> parse
        |> (fun c ->
            match c with
            | Math opr ->
                evalMathCommand opr
                |> Option.map print
                |> Helper.mapError (fun _ -> printf "Invalid operands\n\n")
                |> (fun _ -> loop ())
            | Exit -> doExit ()
            | Invalid ->
                printf "Invalid operation\n\n"
                loop ())

    // The program
    let run () = loop ()
