namespace HP.Web

open FSharp.Data.GraphQL
open FSharp.Data.GraphQL.Types

open HP.Json

module Graph1 =

    type Payload = { data: obj }

    type Person =
        { firstName: string
          lastName: string
          age: int }

    type Person2 =
        { firstName: string
          lastName: string
          age: int }


    let people: List<Person> =
        [ { firstName = "Harshal"
            lastName = "Patil"
            age = 20 }
          { firstName = "Nitesh"
            lastName = "Phadatare"
            age = 30 } ]


    // Really interesting type-safe way to define resolver
    let fieldDef: FieldDef<Person> =
        Define.Field("firstName", String, "Optional description", (fun ctx (p: Person) -> p.firstName))


    let fieldDef2: FieldDef<Person2> =
        Define.Field("firstName", String, (fun ctx (p: Person2) -> p.firstName))


    let Person =
        Define.Object(
            "Person",
            [ Define.Field("firstName", String, (fun ctx (p: Person) -> p.firstName))
              Define.Field("lastName", String, (fun ctx (p: Person) -> p.lastName))
              Define.Field("age", Int, (fun ctx p -> p.age)) ])


    let QueryRoot =
        Define.Object("Query", [
            Define.Field("people", ListOf Person, (fun ctx () -> people))
        ])


    let schema = Schema(QueryRoot)

    let run () =

        let query = """
            query Example {
                people {
                    firstName
                }
            }
        """

        let result =
            async {
                let! response = Executor(schema).AsyncExecute(query)

                match response.Content with
                | Execution.Direct (dictionary, error) ->

                    let (_, documentId) = dictionary.TryGetValue "documentId"
                    let (_, data) = dictionary.TryGetValue "data"

                    printfn "%A" <| documentId.GetType()
                    printfn "%A" <| data.GetType()

                    printfn "%s" <| Json3.stringify data
                    // printfn "%s" <| Json2.stringify payload
                | _ ->
                    printfn "weird"
            }

        result |> Async.RunSynchronously
