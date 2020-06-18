namespace HP.Json


// Built in-serialization
module Json1 =

    open System.Text.Json

    let options =
        // This is an example of named arguments. For methods (not let-bound function), arguments can be passed by name.
        let opts = JsonSerializerOptions(WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase)
        opts.Converters.Add(Serialization.JsonStringEnumConverter())
        opts

    // Typical string based serialization (Not very useful)
    // It converts object to bytes and then to string
    let stringify obj =
        System.Text.Json.JsonSerializer.Serialize(obj)

    let stringifyOpts obj =
        System.Text.Json.JsonSerializer.Serialize(obj, options)

    // Efficient (byte based serialization)
    let bytify obj =
        JsonSerializer.SerializeToUtf8Bytes obj


// Uses FSharp.SystemTextJson
module Json2 =
    open System.Text

    let jsonOptions =
        let opts = Json.JsonSerializerOptions()
        opts.Converters.Add(Json.Serialization.JsonFSharpConverter())
        opts


    let stringify obj =
        Json.JsonSerializer.Serialize(obj, jsonOptions)


// Uses Newtonsoft.Json
module Json3 =
    open Newtonsoft.Json
    let newtonSetting =
        let x = JsonSerializerSettings()
        x.ContractResolver <- Serialization.CamelCasePropertyNamesContractResolver()
        x

    let stringify o = JsonConvert.SerializeObject(o, newtonSetting)
