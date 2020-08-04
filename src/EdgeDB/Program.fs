// Learn more about F# at http://fsharp.org

open FSharp.Data.GraphQL

// type XClient = GraphQLProvider<"http://localhost:8888">


[<EntryPoint>]
let main argv =

    // let x1 = XClient.Types.InsertProject("hello")


    // let m1 =
    //     XClient.Operation<"""mutation {
    //         insert_TestCase(data: [{ name: "Hello world" }]) {
    //             id
    //             name
    //         }
    //     }""">()

    // let m1result = m1.Run()

    // printfn "Mutation tried (err): %A" m1result.Errors
    // printfn "Mutation tried (suc): %A" m1result.Data

    // let q1 =
    //     XClient.Operation<"""query {
    //         User {
    //             email
    //             firstName
    //         }
    //         TestCase {
    //             id
    //             name
    //         }
    //     }""">()


    // let q1result = q1.Run()

    // printfn "Query result: %A" q1result.Data

    printfn "Hello World from F#!"
    0 // return an integer exit code
