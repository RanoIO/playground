namespace HP.Web

open FSharp.Data.GraphQL
open FSharp.Data.GraphQL.Types


module Graph1 =

    type Person =
        { firstName: string
          lastName: string
          age: int }

    let people =
        [ { firstName = "Harshal"
            lastName = "Patil"
            age = 20 }
          { firstName = "Nitesh"
            lastName = "Phadatare"
            age = 30 } ]

    let Person =
        Define.Object
            ("Person",
             [ Define.Field("firstName", String, (fun ctx p -> p.firstName))
               Define.Field("lastName", String, (fun ctx p -> p.lastName))
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
                printf "%A" response
            }

        result |> Async.RunSynchronously
