namespace HP

open System

module Helper =

    let tryInteger (str: string): int option =
        let mutable value = 0
        let succ = Int32.TryParse(str, &value)

        match succ with
        | true -> Some value
        | _ -> None

    // When, C# function takes two values - first as any value and second as out parameter,
    // F# will automatically convert the function into a tuple returning function where
    // the first element is the function return value and the second is the “out” parameter.
    let tryDouble (str: string) =
        let (succ, value) = Double.TryParse(str)
        if succ then Some value else None


    let always (value: 'a): 'b -> 'a = (fun _ -> value)


    let mapError (callback: unit -> 'b) (value: 'a option): 'a option =
        match value with
        | Some x -> Some x
        | None -> (callback ()) |> always None
