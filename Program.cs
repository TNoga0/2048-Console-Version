using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Console
{
    class Game
    {
        private int _boardSize;  //size of the tiles' array
        private int[,] _gameBoard;  //tiles' array (game board)

        public Game(int size)  //simple and basic constructor
        {
            _boardSize = size;
            _gameBoard = new int[_boardSize, _boardSize];
        }

        static void Main(string[] args)
        {

            Game Gra = new Game(4);
            bool invalidFlag = false;  //flag used for move validity checking and continuous error message output
            int[,] backupBoard = new int[Gra._boardSize, Gra._boardSize]; //backup used for move validity checking, filled with tiles' values from a previous move
            Gra.InitializeBoard(ref Gra._gameBoard);

            //Gra.testSpawn(ref Gra._gameBoard);

            char move = 'o';  //dummy move for initial play
            Gra.CloneBoard(ref Gra._gameBoard, ref backupBoard);

            do
            {
                Gra.PrintBoard(ref Gra._gameBoard);
                Gra.CloneBoard(ref Gra._gameBoard, ref backupBoard); // cloning gameBoard to a backup for move validity checking
                if (invalidFlag) { Console.Write("\nINVALID MOVE!\n"); }
                if (Gra.CheckGameOver(ref Gra._gameBoard, ref backupBoard))
                {
                   // Gra.PrintBoard(ref Gra._gameBoard);
                    Console.Write("\n GAME OVER! \n\n");
                    Environment.Exit(0);
                }
                else
                {
                    move = (char)Console.Read();
                    Console.Clear();
                    if (move == 'l')
                    {
                        Gra.MoveLeft(ref Gra._gameBoard);
                        if (!Gra.ValidCheck(ref Gra, ref backupBoard)) { invalidFlag = true; }
                        else { invalidFlag = false; Gra.SpawnTile(2, ref Gra._gameBoard); }
                    }
                    else if (move == 'r')
                    {
                        Gra.MoveRight(ref Gra._gameBoard);
                        if (!Gra.ValidCheck(ref Gra, ref backupBoard)) { invalidFlag = true; }
                        else { invalidFlag = false; Gra.SpawnTile(2, ref Gra._gameBoard); }
                    }
                    else if (move == 'd')
                    {
                        Gra.MoveDown(ref Gra._gameBoard);
                        if (!Gra.ValidCheck(ref Gra, ref backupBoard)) { invalidFlag = true; }
                        else { invalidFlag = false; Gra.SpawnTile(2, ref Gra._gameBoard); }
                    }
                    else if (move == 'u')
                    {
                        Gra.MoveUp(ref Gra._gameBoard);
                        if (!Gra.ValidCheck(ref Gra, ref backupBoard)) { invalidFlag = true; }
                        else { invalidFlag = false; Gra.SpawnTile(2, ref Gra._gameBoard); }
                    }
                    else if (move == 'q')
                    {
                        Console.WriteLine("Bye bye!\n\n");
                    }
                }


            } while (move != 'q');



        }

        void InitializeBoard(ref int[,] _board)
        {
            for (int w = 0;w < _board.GetLength(0); w++)
            {
                for (int k = 0; k < _board.GetLength(0); k++)
                {
                    _board[w, k] = 0;
                }
            }
            InitSpawnTile(2, ref _board);

        }

        void PrintBoard(ref int[,] _board)
        {
            Console.SetCursorPosition((Console.WindowWidth - "2048 Game".Length) / 2, Console.CursorTop);
            Console.Write("2048 Game");
            Console.Write("\n\n");
            Console.WriteLine("To move enter: l for left, r for right, u for up, d for down, q to quit.\n\n");

            for (int w = 0; w < _board.GetLength(0); w++)
            {
                for (int k = 0; k < _board.GetLength(0); k++)
                {
                    Console.Write(_board[w, k]);
                    Console.Write(' ');
                }
                Console.Write('\n');
            }
            Console.Write("\n\n");
            Console.WriteLine("Input the desired move:   ");
        }

        void InitSpawnTile(int numberToBeSpawned, ref int[,] _board) //initial tile spawn, insterting two 2's into the empty board
        {
            Random rnd = new Random();
            int x = rnd.Next(0, _boardSize);
            int y = rnd.Next(0, _boardSize);
            _board[x, y] = numberToBeSpawned;
            int x1 = rnd.Next(0, _boardSize);
            int y1 = rnd.Next(0, _boardSize);
            while (_board[x1, y1] != 0)
            {
                x1 = rnd.Next(0, _boardSize);
                y1 = rnd.Next(0, _boardSize);
            }
            _board[x1, y1] = numberToBeSpawned;
        }

        void SpawnTile(int numberToBeSpawned, ref int[,] _board)
        {
            Random rnd = new Random();
            int x = rnd.Next(0, _boardSize);
            int y = rnd.Next(0, _boardSize);
            while (_board[x, y] != 0)
            {
                x = rnd.Next(0, _boardSize);
                y = rnd.Next(0, _boardSize);
            }
            _board[x, y] = numberToBeSpawned;
        }

        void MoveLeft(ref int[,] _board)
        {
            for (int w = 0; w < _boardSize; w++)
            {
                int[] newRow = new int[_boardSize]; //declaring new backup row and filling it with zeroes
                foreach (int elem in newRow) { newRow[elem] = 0; }
                int j = 0;
                int previous = 1;
                for (int k = 0; k < _boardSize; k++)
                {
                    if (_board[w, k] != 0)
                    {
                        if (previous == 1)
                        {
                            previous = _board[w, k];
                        }
                        else
                        {
                            if (previous == _board[w, k])
                            {
                                newRow[j] = 2 * _board[w, k];
                                j += 1;
                                previous = 1;
                            }
                            else
                            {
                                newRow[j] = previous;
                                j += 1;
                                previous = _board[w, k];                               
                            }
                        }
                    }
                }
                if (previous != 1)
                {
                    newRow[j] = previous;
                }
                for(int iter = 0;iter < _boardSize; iter++)
                {
                    _board[w, iter] = newRow[iter];
                }

            }
        }

        void MoveRight(ref int[,] _board)
        {
            for (int w = 0; w < _boardSize; w++)
            {
                int[] newRow = new int[_boardSize]; //declaring new backup row and filling it with zeroes
                foreach (int elem in newRow) { newRow[elem] = 0; }
                int j = _boardSize - 1;
                int previous = 1;
                for (int k = _boardSize - 1; k >= 0; k--)
                {
                    if (_board[w, k] != 0)
                    {
                        if (previous == 1)
                        {
                            previous = _board[w, k];
                        }
                        else
                        {
                            if (previous == _board[w, k])
                            {
                                newRow[j] = 2 * _board[w, k];
                                j -= 1;
                                previous = 1;
                            }
                            else
                            {
                                newRow[j] = previous;
                                j -= 1;
                                previous = _board[w, k];
                            }
                        }
                    }
                }
                if (previous != 1)
                {
                    newRow[j] = previous;
                }
                for (int iter = 0; iter < _boardSize; iter++)
                {
                    _board[w, iter] = newRow[iter];
                }

            }
        }

        void MoveDown(ref int[,] _board)
        {
            for (int k = 0; k < _boardSize; k++)
            {
                int[] newRow = new int[_boardSize]; //declaring new backup row and filling it with zeroes
                foreach (int elem in newRow) { newRow[elem] = 0; }
                int j = _boardSize - 1;
                int previous = 1;
                for (int w = _boardSize - 1; w >= 0; w--)
                {
                    if (_board[w, k] != 0)
                    {
                        if (previous == 1)
                        {
                            previous = _board[w, k];
                        }
                        else
                        {
                            if (previous == _board[w, k])
                            {
                                newRow[j] = 2 * _board[w, k];
                                j -= 1;
                                previous = 1;
                            }
                            else
                            {
                                newRow[j] = previous;
                                j -= 1;
                                previous = _board[w, k];
                            }
                        }
                    }
                }
                if (previous != 1)
                {
                    newRow[j] = previous;
                }
                for (int iter = 0; iter < _boardSize; iter++)
                {
                    _board[iter, k] = newRow[iter];
                }

            }
        }

        void MoveUp(ref int[,] _board)
        {
            for (int k = 0; k < _boardSize; k++)
            {
                int[] newRow = new int[_boardSize]; //declaring new backup row and filling it with zeroes
                foreach (int elem in newRow) { newRow[elem] = 0; }
                int j = 0;
                int previous = 1;
                for (int w = 0; w < _boardSize; w++)
                {
                    if (_board[w, k] != 0)
                    {
                        if (previous == 1)
                        {
                            previous = _board[w, k];
                        }
                        else
                        {
                            if (previous == _board[w, k])
                            {
                                newRow[j] = 2 * _board[w, k];
                                j += 1;
                                previous = 1;
                            }
                            else
                            {
                                newRow[j] = previous;
                                j += 1;
                                previous = _board[w, k];
                            }
                        }
                    }
                }
                if (previous != 1)
                {
                    newRow[j] = previous;
                }
                for (int iter = 0; iter < _boardSize; iter++)
                {
                    _board[iter, k] = newRow[iter];
                }

            }
        }

        bool CheckGameOver(ref int[,] _board, ref int[,] copyBoard)
        {
            bool full = true;  //full flag is used for checking whether the board is full or not
            for (int w = 0; w < _boardSize; w++)
            {
                for (int k = 0; k < _boardSize; k++)
                {
                    if (_board[w, k] == 0)
                    {
                        full = false;
                    }
                }
            }
            if (full)  //if the board is full, check for valid moves, i.e. if there are two tiles with the same number placed next to each other
            {
                //Check Direction - DOWN:
                for (int w = 0; w < _boardSize - 1; w++)
                {
                    for (int k = 0; k < _boardSize; k++)
                    {
                        if (_board[w, k] == _board[w + 1, k]) 
                        {
                            return false;
                        }
                    }
                }
                //Check Direction - RIGHT:
                for (int w = 0; w < _boardSize; w++)
                {
                    for (int k = 0; k < _boardSize - 1; k++)
                    {
                        if (_board[w, k] == _board[w, k + 1])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else { return false; }
        }

        /* CheckValidMove combined with ValidCheck check if the playing board's state changes between current and previous move. If yes,
         * move was valid*/
        bool CheckValidMove(ref int[,] _board, ref int[,] copyBoard)  
        {
            for (int w = 0; w < _boardSize; w++)
            {
                for (int k = 0; k < _boardSize; k++)
                {
                    if (_board[w, k] != copyBoard[w, k])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        bool ValidCheck(ref Game Gra, ref int[,] backupBoard)
        {
            if (!Gra.CheckValidMove(ref Gra._gameBoard, ref backupBoard))
            {
                for (int w = 0; w < Gra._boardSize; w++)
                {
                    for (int k = 0; k < Gra._boardSize; k++)
                    {
                        Gra._gameBoard[w, k] = backupBoard[w, k];
                    }
                }
                Console.Clear();
                Console.Write("INVALID MOVE!\n");
                Gra.PrintBoard(ref Gra._gameBoard);
                return false;
            }
            else { return true; }
        }

        void CloneBoard(ref int[,] _board, ref int[,] backupBoard)  //copying the arrays for move validity checking
        {
            for (int w = 0; w < _boardSize; w++)
            {
                for (int k = 0; k < _boardSize; k++)
                {
                    backupBoard[w, k] = _board[w, k];
                }
            }
        }
        
        void testSpawn(ref int[,] _board) //spawning a test set of tiles for various scenarios checking
        {
            _board[0, 0] = 2;
            _board[0, 1] = 4;
            _board[0, 2] = 8;
            _board[0, 3] = 16;
            _board[1, 0] = 2;
            _board[1, 1] = 8;
            _board[1, 2] = 4;
            _board[1, 3] = 2;
            _board[2, 0] = 2;
            _board[2, 1] = 4;
            _board[2, 2] = 8;
            _board[2, 3] = 16;
            _board[3, 0] = 16;
            _board[3, 1] = 8;
            _board[3, 2] = 4;
            _board[3, 3] = 2;
        }


    }
}
