using Microsoft.AspNetCore.Mvc;
using System.Linq;
using PracticaProgramada_Grupo3.Models; 
using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace PracticaProgramada_Grupo3.Controllers
{
    public class GameController : Controller
    {

        private static char[] board = Enumerable.Repeat(' ', 9).ToArray();
        private static Random random = new Random();
        private readonly Tarea02Context _context;
        private static Stopwatch stopwatch = new Stopwatch();

        public GameController(Tarea02Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Play(int cellIndex)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            bool isFirstMove = board.All(c => c == ' ');
            if (isFirstMove)
            {
                stopwatch.Restart();
                stopwatch.Start(); 
            }
            if (board[cellIndex] == ' ')
            {
                board[cellIndex] = 'X'; 

                if (CheckWin('X'))
                {
                    var game = new Game
                    {
                        UserId = (int)userId,
                        Result = "Win",
                        GameDuration = stopwatch.Elapsed,
                        DatePlayed = DateTime.Now
                    };
                    _context.Games.Add(game);
                    await _context.SaveChangesAsync();
                    stopwatch.Restart();
                    

                    var message = "¡Has ganado!";
                    board = Enumerable.Repeat(' ', 9).ToArray(); // Reiniciar el tablero
                    return Json(new { board, message });
                }
                else if (CheckDraw())
                {
                    var game = new Game
                    {
                        UserId = (int)userId,
                        Result = "Tie",
                        GameDuration = stopwatch.Elapsed,
                        DatePlayed = DateTime.Now
                    };
                    _context.Games.Add(game);
                    await _context.SaveChangesAsync();
                    stopwatch.Restart();

                    var message = "Empate.";
                    board = Enumerable.Repeat(' ', 9).ToArray(); 
                    return Json(new { board, message });
                }

                // Turno de la IA
                MakeAIMove();
                if (CheckWin('O'))
                {
                    var game = new Game
                    {
                        UserId = (int)userId,
                        Result = "Loss",
                        GameDuration = stopwatch.Elapsed,
                        DatePlayed = DateTime.Now
                    };
                    _context.Games.Add(game);
                    await _context.SaveChangesAsync();
                    stopwatch.Restart();

                    var message = "La IA ha ganado.";
                    board = Enumerable.Repeat(' ', 9).ToArray(); 
                    return Json(new { board, message });
                }
                else if (CheckDraw())
                {
                    var game = new Game
                    {
                        UserId = (int)userId,
                        Result = "Tie",
                        GameDuration = stopwatch.Elapsed,
                        DatePlayed = DateTime.Now
                    };
                    _context.Games.Add(game);
                    await _context.SaveChangesAsync();
                    stopwatch.Restart();


                    var message = "Empate.";
                    board = Enumerable.Repeat(' ', 9).ToArray(); 
                    return Json(new { board, message });
                }
            }

            return Json(new { board, message = string.Empty });
        }


        [HttpPost]
        public IActionResult Restart()
        {
            board = Enumerable.Repeat(' ', 9).ToArray();
            stopwatch.Restart();
            return Json(new { board, message = "Juego reiniciado." });
        }

        private bool CheckWin(char player)
        {
            int[,] winConditions = new int[,]
            {
                {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
                {0, 3, 6}, {1, 4, 7}, {2, 5, 8},
                {0, 4, 8}, {2, 4, 6}
            };

            for (int i = 0; i < winConditions.GetLength(0); i++)
            {
                if (board[winConditions[i, 0]] == player &&
                    board[winConditions[i, 1]] == player &&
                    board[winConditions[i, 2]] == player)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckDraw()
        {
            return board.All(c => c != ' ');
        }

        private void MakeAIMove()
        {
            int bestScore = int.MinValue;
            int move = -1;
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == ' ')
                {
                    board[i] = 'O';
                    int score = Minimax(board, 0, false);
                    board[i] = ' ';
                    if (score > bestScore)
                    {
                        bestScore = score;
                        move = i;
                    }
                }
            }

            if (move != -1)
            {
                board[move] = 'O';
            }
        }
        private int Minimax(char[] board, int depth, bool isMaximizing)
        {
            if (CheckWin('O'))
            {
                return 1;
            }
            else if (CheckWin('X'))
            {
                return -1;
            }
            else if (CheckDraw())
            {
                return 0;
            }

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] == ' ')
                    {
                        int score = Minimax(board, depth + 1, false);
                        board[i] = ' '; 
                        bestScore = Math.Max(score, bestScore);
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] == ' ')
                    {
                        board[i] = 'X';
                        int score = Minimax(board, depth + 1, true);
                        board[i] = ' '; 
                        bestScore = Math.Min(score, bestScore);
                    }
                }
                return bestScore;
            }
        }

    }
}
