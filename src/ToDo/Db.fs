namespace HP.ToDo

module Db =

    // State
    type State =
        { todos: Map<System.Guid, ToDo> }

    // Actions
    type private Actions =
        | List of AsyncReplyChannel<list<ToDo>>




    let private initialState =

        let firstId = System.Guid.NewGuid()
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
