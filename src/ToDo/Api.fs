namespace HP.ToDo

open System.Text

open Suave
open Suave.Filters
open Suave.Operators


module Api =

    type Error =
        { error: string }


    let byteToString (bytes: byte[]) = Encoding.UTF8.GetString(bytes)

    let jsonOptions =
        let opts = Json.JsonSerializerOptions()
        opts.Converters.Add(Json.Serialization.JsonFSharpConverter())
        opts


    let stringify obj =
        Json.JsonSerializer.Serialize(obj, jsonOptions)


    let parse<'T> (obj: byte[]) =
        try
            obj |> byteToString |> fun s -> Json.JsonSerializer.Deserialize<'T>(s, jsonOptions) |> Ok
        with
        | :? Json.JsonException as ex ->
            printfn  "JSON Parsing exception"
            Error ex


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


    let addNewTodoWebPart ctx =
        async {
            printfn "Adding new todo item"

            let parsed = parse ctx.request.rawForm

            match parsed with
            | Ok value ->
                let! data = Db.addNewItem value
                let response = json data
                let! y = response ctx

                return y
            | Error ex ->
                return! json ex.Message ctx
        }


    let getToDoItemPart guid ctx =
        async {
            printfn "Finding a todo item"

            let! data = Db.getToDo guid

            match data with
            | Some item ->
                let response = json item
                return! response ctx
            | None ->
                return! RequestErrors.NOT_FOUND "Not Found" ctx
        }


    let router () = choose [
        path "/todos" >=> GET >=> getAllToDoWebPart
        path "/todos" >=> POST >=> addNewTodoWebPart
        pathScan "/todos/%s"  (fun rawGuid ->
            let todoId = System.Guid.Parse rawGuid
            getToDoItemPart todoId)
    ]
