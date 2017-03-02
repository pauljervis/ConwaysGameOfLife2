//The board size
let WIDTH = 60
let HEIGHT = 24

[<Literal>]
let ALIVE = true

[<Literal>]
let DEAD = false

//How many neighbours does this cell have?
let countNeighbours (board:bool[,]) x y =
    (if (y > 0 && board.[x, y - 1]) then 1 else 0) +
    (if (y > 0 && x > 0 && board.[x - 1, y - 1]) then 1 else 0) +
    (if (y > 0 && x < WIDTH - 1 && board.[x + 1, y - 1]) then 1 else 0) +
    (if (y < HEIGHT - 1 && board.[x, y + 1]) then 1 else 0) +
    (if (y < HEIGHT - 1 && x > 0 && board.[x - 1, y + 1]) then 1 else 0) +
    (if (y < HEIGHT - 1 && x < WIDTH - 1 && board.[x + 1, y + 1]) then 1 else 0) +
    (if (x > 0 && board.[x - 1, y]) then 1 else 0) +
    (if (x < WIDTH - 1 && board.[x + 1, y]) then 1 else 0)

let applyRule board x y =
    let numberOfNeighbours = countNeighbours board x y
    match board.[x,y] with
    | ALIVE when numberOfNeighbours < 2 -> DEAD  //Any live cell with fewer than two live neighbours dies, as if caused by underpopulation
    | ALIVE when numberOfNeighbours > 3 -> DEAD  //Any live cell with more than three live neighbours dies, as if by overcrowding.
    | ALIVE -> ALIVE                             //Any live cell with two or three live neighbours lives on to the next generation.
    | dead when numberOfNeighbours = 3 -> ALIVE  //Any dead cell with exactly three live neighbours becomes a live cell.
    | _ -> DEAD

let nextGeneration board : bool[,] = 
    Array2D.init WIDTH HEIGHT (applyRule board)

let display (board : bool[,]) =
    let displayCell x y = if board.[x,y] then "X" else "."
    for r in 0..(HEIGHT - 1) do
        for c in 0..(WIDTH - 1) do
            printf "%s" (displayCell c r)
        printf "%s" System.Environment.NewLine
    board

let waitForABit board = 
    System.Threading.Thread.Sleep(100)        
    board

let rec playGame board = 
    board |> nextGeneration |> display |> waitForABit |> playGame 

let setup =
    let board = Array2D.create WIDTH HEIGHT false
    board.[1,1] <- ALIVE
    board.[1,2] <- ALIVE
    board.[1,3] <- ALIVE
    board

[<EntryPoint>]
let main argv = 
    setup |> playGame
    0
