using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PracticaProgramada_Grupo3.Models
{
    public class TicTacToeService
    {
        private string[,] board = new string[3, 3];

        public TicTacToeService()
        {
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = string.Empty;
                }
            }
        }

        public bool MakePlayerMove(int row, int col)
        {
            if (board[row, col] == string.Empty)
            {
                board[row, col] = "X"; // Asume que el jugador es siempre "X"
                return true;
            }

            return false;
        }

        public void MakeServerMove()
        {
            // Aquí implementarías la lógica para que el servidor haga su movimiento.
            // Por simplicidad, puedes empezar haciendo un movimiento aleatorio, y luego implementar una estrategia más sofisticada.
        }

        // Método para verificar el estado del juego (victoria, derrota, empate)
        public string CheckGameState()
        {
            return "";
        }

        public async Task RegisterGameAsync(string userId, string result, TimeSpan duration)
        {
            var game = new Game
            {
                UserId = userId,
                Result = result,
                GameDuration = duration,
                DatePlayed = DateTime.Now
            };

            // Asume que _context es tu DbContext de EF Core
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
        }

    }

}

