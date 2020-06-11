namespace HP.Web

open FSharp.Data.GraphQL
open FSharp.Data.GraphQL.Types

open Newtonsoft.Json


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


    let settings =
        let x = JsonSerializerSettings()
        x.ContractResolver <- Serialization.CamelCasePropertyNamesContractResolver()
        x

    let json o = JsonConvert.SerializeObject(o, settings)


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
                let result = json response

                printf "%A\n%s" response result
            }

        result |> Async.RunSynchronously
