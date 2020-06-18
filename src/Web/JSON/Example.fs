namespace HP.Json

open System.Text.Json

module Example01 =

    type Episode =
        | NewHope = 1
        | Empire = 2
        | Jedi = 3


    type Hero =
        { name: string
          episode: Episode }

    type Person =
        { name: string

          // Change age field to personAge
          [<Serialization.JsonPropertyName "personAge">]
          age: int

          gender: Models.Gender

          // Exlude password from serialization
        //   [<Serialization.JsonIgnore>]
          password: string option
          secretQuestion: string option

          category: Models.CustomerType }


    let p1 =
        { name = "Harshal"
          age = 30
          gender = Models.Male
          password = None
          secretQuestion = None
          category = Models.CustomerType.Free }

    let p2 =
        { name = "X-Men"
          age = 100
          gender = Models.Other "privacy"

          password = Some "abcdef"
          secretQuestion = Some "hello world"
          category = Models.CustomerType.Premium }


    let h1 =
        { name = "First Hero"
          episode = Episode.Empire }


    let run () =

        printfn "First person\n%A" p1
        printfn "Second person\n%A" p2

        printfn "\n\nUSING BUILT-IN SERIALIZER"
        printfn "%s\n" <| Json1.stringify p1

        let byteString = p2 |> Json1.bytify |> System.Text.Encoding.UTF8.GetString
        printfn "Serialized string using bytes:\n%s" byteString

        printfn "\n\nUSING FSharp.SystemTextJson"
        printfn "%s\n\n%s" (Json2.stringify p1) (Json2.stringify p2)
        printfn "%s" <| Json2.stringify h1


        printfn "\n\nUSING Newtonsoft.Json"
        printfn "%s\n\n%s" (Json3.stringify p1) (Json3.stringify p2)

        ()
