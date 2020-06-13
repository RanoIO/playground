namespace HP.ToDo

[<AutoOpen>]
module Models =

    // This is required to make indentation of 4 space.
    // Otherwise Visual Studio Code doesn't work well.
    type Gender =
        | Male
        | Female
        | Other of string


    type ToDo =
        { id: System.Guid
          title: string
          isComplete: bool
          deadline: System.DateTime option }

    type ToDoInput =
        { title: string
          isComplete: bool
          deadline: System.DateTime option }
