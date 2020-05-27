namespace HP

open System

module Helper =

    let tryInteger (str: string): int option =
        let mutable value = 0
        let succ = Int32.TryParse(str, &value)

        match succ with
        | true -> Some value
        | _ -> None


    let tryDouble (str: string) =
        let mutable value = 0.0
        let succ = Double.TryParse(str, &value)

        if succ then Some value else None


    let always (value: 'a): 'b -> 'a = (fun _ -> value)


    let mapError (callback: unit -> 'b) (value: 'a option): 'a option =
        match value with
        | Some x -> Some x
        | None -> (callback ()) |> always None
