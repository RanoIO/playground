// Learn more about F# at http://fsharp.org

open FSharp.Data.GraphQL

type XClient = GraphQLProvider<"http://localhost:8888/">


[<EntryPoint>]
let main argv =

    let value = "email"

    let z = XClient.Types.InsertProject("hello")

    let query =
        XClient.Operation<"""query {
            User {
                email
                firstName
            }
        }""">()


    // XClient.Schema

    let result = query.Run()

    let data = result.Data

    let data1 =
        match data with
        | Some x ->
            let x1 = x.GetProperties()
            let x2 = List.head x1
            x2.Value
            ()
        | None -> ()



    printfn "Hello World from F#!"
    0 // return an integer exit code
