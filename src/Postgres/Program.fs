// Learn more about F# at http://fsharp.org

open System

open Npgsql
open FSharp.Data.Sql

[<Literal>]
let connString = "Host=localhost;Port=5432;Database=sulphur;Username=postgres;Password=postgres"


let [<Literal>] dbVendor = Common.DatabaseProviderTypes.POSTGRESQL
let [<Literal>] indivAmount = 1000
let [<Literal>] schema = "public"

let [<Literal>] useOptTypes = true
let [<Literal>] resPath = @"C:\work\code\visual-studio\playground\src\Postgres\libraries"


// type PgSource =
//     SqlDataProvider<
//         DatabaseVendor = dbVendor,
//         ConnectionString = connString,
//         CaseSensitivityChange = Common.CaseSensitivityChange.ORIGINAL,
//         ResolutionPath = resPath,
//         ConnectionStringName = "",
//         IndividualsAmount = indivAmount,
//         UseOptionTypes = useOptTypes,
//         Owner = schema>



[<EntryPoint>]
let main argv =


    printfn "Hello World from F#! %s" resPath

    0 // return an integer exit code
