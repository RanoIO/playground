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
let [<Literal>] resPath = @"pglibs"

type PgSource =
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

    let context = PgSource.GetDataContext()


    let projects = context.Public.Project

    let conn = context.CreateConnection()

    let trans = conn.BeginTransaction()

    let cmd = conn.CreateCommand ()

    cmd.CommandText <- "select * from project"

    let someQuery =
        query {
            for x in projects do
            select (x.Name, x.Id)
        }



    printfn "Hello World from F#! %s" resPath

    0 // return an integer exit code
