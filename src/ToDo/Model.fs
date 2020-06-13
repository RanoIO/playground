namespace HP.ToDo

[<AutoOpen>]
module Models =

    type Gender =
        | Male
        | Female
        | Other of string


    type ToDo =
        { id: System.Guid
          title: string
          isComplete: bool
          deadline: System.DateTime option }
