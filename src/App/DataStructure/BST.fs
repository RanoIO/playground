namespace HP.DataStructure

open System

module BST =

    // Standard REPL Model


    // Alternately, Tuples/anonymous record can be used to describe tree node.
    type Tree<'T> =
        | Empty
        | Tree of Node<'T>

    and Node<'T> =
        { value: 'T
          left: Tree<'T>
          right: Tree<'T> }


    type BSTree = Tree<int>


    type TreeOperation =
        | Insert of int
        | PreOrder
        | InOrder
        | PostOrder


    type Command =
        | Exit
        | Valid of TreeOperation
        | Invalid of string


    type State = { tree: BSTree }


    let rec insert (tree: BSTree) (newVal): BSTree =
        match tree with
        | Empty ->
            Tree
                { value = newVal
                  left = Empty
                  right = Empty }
        | Tree node ->
            if node.value > newVal
                then Tree { node with left = insert node.left newVal }
                else Tree { node with right = insert node.right newVal }


    // Note the use of reverse in the end.
    // That is probably the idiomatic way to handle the list
    let preorder (tree: BSTree) =
        let rec preorderLoop (tree: BSTree) (list: list<int>) : list<int> =
            match tree with
            | Empty -> list
            | Tree node ->
                node.value::list
                |> preorderLoop node.left
                |> preorderLoop node.right
        preorderLoop tree []
        |> List.rev


    let inorder (tree: BSTree) =
        let rec inorderLoop tree list =
            match tree with
            | Empty -> list
            | Tree node ->
                inorderLoop node.left list
                |> (fun l -> node.value::l)
                |> inorderLoop node.right
        inorderLoop tree [] |> List.rev


    let postorder (tree: BSTree) =
        let rec postorderLoop tree list =
            match tree with
            | Empty -> list
            | Tree node ->
                postorderLoop node.left list
                |> postorderLoop node.right
                |> (fun l -> node.value::l)
        postorderLoop tree [] |> List.rev


    let parseBSTNode (raw: string) = HP.Helper.tryInteger raw


    let read (): string =
        printfn "Choose your command"
        printfn "1. Insert"
        printfn "2. Pre-order"
        printfn "3. In-order"
        printfn "4. Post-order"
        printfn "e. Exit"
        printf "Enter your choice: "
        Console.ReadLine()


    let parse (raw: string): Command =
        match raw with
        | "e" -> Exit
        | "E" -> Exit
        | "1" ->
            printf "Enter the node to insert: "
            match parseBSTNode <| Console.ReadLine() with
            | Some value -> Valid <| Insert value
            | None -> Invalid "Invalid value"
        | "2" -> Valid PreOrder
        | "3" -> Valid InOrder
        | "4" -> Valid PostOrder
        | _ -> Invalid "Invalid option"


    let eval (tree: BSTree) (operation: TreeOperation) =
        match operation with
        | Insert newVal -> insert tree newVal
        | PreOrder ->
            printfn "Preorder: %A" (preorder tree)
            tree
        | InOrder ->
            printfn "Preorder: %A" (inorder tree)
            tree
        | PostOrder ->
            printfn "Postorder: %A" (postorder tree)
            tree


    let print = printfn "%A"


    let rec loop (state: State) =
        read ()
        |> parse
        |> (fun c ->
            match c with
            | Valid o ->
                let newState = { state with tree = eval state.tree o }
                print newState
                loop newState
            | Exit -> printfn "Bye bye!"
            | Invalid e -> printfn "Error: %s" e)


    let run () =
        let appState = { tree = Empty }

        loop (appState)
