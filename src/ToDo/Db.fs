namespace HP.ToDo

open System

module Db =

    // State
    type State =
        { todos: Map<Guid, ToDo> }

    // Actions
    type private Actions =
        | List of AsyncReplyChannel<list<ToDo>>
        | NewItem of ToDoInput * AsyncReplyChannel<ToDo>
        | Get of Guid * AsyncReplyChannel<ToDo option>


    let findInMap key map =
        try
            Some <| Map.find key map
        with
        | :? Collections.Generic.KeyNotFoundException as ex ->
            None


    let guid () = Guid.NewGuid()

    let private initialState =

        let firstId = guid()
        let firstItem = {
            id = firstId
            title = "First Item"
            isComplete = false
            deadline = None }

        { todos = Map.empty.Add(firstId, firstItem) }


    // Reducer
    let private update (state: State) action =
        match action with
        | List replyChannel ->
            state.todos
            |> Map.toList
            |> List.map(fun (id, item) -> item)
            |> replyChannel.Reply
            state
        | NewItem (newItem, channel) ->
            let toAdd =
                { id = guid()
                  title = newItem.title
                  isComplete = newItem.isComplete
                  deadline = newItem.deadline }

            let newMap = Map.add toAdd.id toAdd state.todos

            channel.Reply toAdd
            { state with todos = newMap }
        | Get (id, channel) ->
            findInMap id state.todos
            |> channel.Reply
            state


    // Store instance
    let private agent: MailboxProcessor<Actions> =
        MailboxProcessor.Start(fun inbox ->

            printfn "DB Mailbox is starting"

            let rec loop oldState =
                inbox.Receive() // Read the message
                |> Async.map (update oldState) // Update the state
                |> Async.bind loop // Loop through again

            // Start the reducer
            loop initialState)


    let getAllToDos () =
        agent.PostAndAsyncReply List


    let addNewItem item =
        agent.PostAndAsyncReply (fun c -> NewItem (item, c))


    let getToDo todo =
        agent.PostAndAsyncReply (fun c -> Get (todo, c))
