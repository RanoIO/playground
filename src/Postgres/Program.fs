// Learn more about F# at http://fsharp.org

open System

open Npgsql
open System.Linq
open FSharp.Data.Sql

[<Literal>]
let connString = "Host=localhost;Port=5432;Database=sulphur;Username=postgres;Password=postgres"


let [<Literal>] dbVendor = Common.DatabaseProviderTypes.POSTGRESQL
let [<Literal>] indivAmount = 1000
let [<Literal>] schema = "public"

let [<Literal>] useOptTypes = true
let [<Literal>] resPath = @"C:\work\code\visual-studio\playground\src\Postgres\pglibs"

type PGSource =
    SqlDataProvider<
        DatabaseVendor = dbVendor,
        ConnectionString = connString,
        CaseSensitivityChange = Common.CaseSensitivityChange.ORIGINAL,
        ResolutionPath = resPath,
        ConnectionStringName = "",
        IndividualsAmount = indivAmount,
        UseOptionTypes = useOptTypes,
        Owner = schema>



[<EntryPoint>]
let main argv =

    Common.QueryEvents.SqlQueryEvent
    |> Event.add (printfn "Executing SQL: %O")


    let context = PGSource.GetDataContext()

    // let newUser =
    //     let user =
    //         context
    //             .Public.User
    //             .Create("h@hp.com", "Harshal", "Patil", "1234", "1234", "1234")
    //     user.Id <- "1"
    //     context.SubmitUpdates ()

    // let newGoogleAccount =
    //     let ga =
    //         context
    //             .Public.GoogleAccount
    //             .Create("ga@hp.com", "12345", "1")
    //     ga.Id <- "101"
    //     context.SubmitUpdates ()

    // context.Public.User.Individuals.``1``

    let q1 =
        query {
            for x in context.Public.User do
            join y in context.Public.GoogleAccount on (x.Id = y.UserId)
            select (x, y)
        }

    // let result =
    //     q1
    //     |> Seq.toList

    // let r2 = List.head result

    // let r3 =
    //     (fst r2).``public.GoogleAccount by id``
    //     |> Seq.toList

    // printfn "Hello World from F#! %O" (r3.Head.Email)

    let n1 =
        query {
            for x in context.Public.User do
            select (x.Id)
        }

    let n2 =
        query {
            for y in context.Public.GoogleAccount do
                where (n1.Contains(y.UserId))
                select y
        }

    let result = n2 |> Seq.toList

    printfn "Hello world from F# %A" result


    0 // return an integer exit code
