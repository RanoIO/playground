namespace HP.Json

open System.Text.Json

module BuiltIn =

    type Person =
        { name: string

          // Change age field to personAge
          [<Serialization.JsonPropertyName "personAge">]
          age: int

          gender: Models.Gender

          // Exlude password from serialization
          [<Serialization.JsonIgnore>]
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


    let options =
        let opts = JsonSerializerOptions(WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase)
        opts.Converters.Add(Serialization.JsonStringEnumConverter())
        opts


    // Typical string based serialization
    // It converts object to bytes and then to string
    let p1s =
        System.Text.Json.JsonSerializer.Serialize(p1)

    let p2s1 =
        System.Text.Json.JsonSerializer.Serialize(p2, options)

    // Efficient
    let p2s2 = JsonSerializer.SerializeToUtf8Bytes(p2)


    let run () =
        printfn "Serialized string:\n%s\n" p1s

        System.Text.Encoding.UTF8.GetString p2s2
        |> printfn "Serialized string:\n%s\n%s" p2s1

        ()
