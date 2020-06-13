namespace HP.ToDo

open System.Text

open Suave
open Suave.Filters
open Suave.Operators


module Api =

    let jsonOptions =
        let opts = Json.JsonSerializerOptions()
        opts.Converters.Add(Json.Serialization.JsonFSharpConverter())
        opts


    let stringify obj =
        Json.JsonSerializer.Serialize(obj, jsonOptions)


    // My custom webpart
    let json (value: obj): WebPart =
        value
        |> stringify
        |> Successful.OK
        >=> Writers.setMimeType "application/json; charset=utf-8"


    // Custom webpart
    // A webpart is basically a function that takes a HttpContext and produces
    // Async<HttpContext option>
    let getAllToDoWebPart (ctx: HttpContext) =
        async {

            printfn "Getting list of all the todo items"

            let! data = Db.getAllToDos ()
            let response = json data

            return! response ctx
        }


    let getAllToDos: WebPart =
        path "/todos" >=> GET >=> getAllToDoWebPart


    let router () = choose [ getAllToDos ]
