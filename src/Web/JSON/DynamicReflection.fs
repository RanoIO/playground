namespace HP.Json

open System

module DynamicReflection =

    // First get to get Type Information via reflection
    // Here we retrieve the type from Value.
    // GetType is defined on System.Object type and thus every value has it.
    let basicString: Type = "hello".GetType()


    // typeof is a keyword not a function.
    // typeof<> gives you a runtime representation of a static type
    // typedefof<> gives a type definition of a static type.
    let genderType: Type = typeof<Models.Gender>

    // While .NET's built-in reflection API is useful,
    // the F# compiler performs a lot of magic which makes built-in types
    // like unions, tuples, functions, and other built-in types appear strange
    // using vanilla reflection. The Microsoft.FSharp.Reflection namespace provides
    // a wrapper for exploring F# types.

    let stringOpts = Some "hello"

    // Not working currently
    // let optionType = Type.GetType ("HP.Json.Models.Gender")

    let walkUnion () =
        let cases = Reflection.FSharpType.GetUnionCases(genderType)

        Array.map (fun (c: Reflection.UnionCaseInfo) ->
            printfn "%A" c.Name) cases


    let run () =
        printfn "UnionType: %A" <| Reflection.FSharpType.IsUnion(genderType)
        walkUnion ()
        |> ignore
