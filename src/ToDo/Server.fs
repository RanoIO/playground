namespace HP.ToDo

open Suave

module Server =

    let config token =
        { defaultConfig with cancellationToken = token }

    let make () = Api.router ()

    let run config webpart =
        startWebServerAsync config webpart
