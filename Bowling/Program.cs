using System.Net.NetworkInformation;

namespace Bowling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Bownling Game";
            Console.WriteLine("Let's bowl! \n");

            var game = new Game();
            PrintScoreBoard(game);

            while (!game.IsGameDone)
            {
                int pins = -1;
                pins = HandlePlayerInput(game, pins);
                HandleGame(game, pins);
            }

            Console.WriteLine($"\nGame over, thanks for playing! You're total score is {game.Score}.");
        }

        private static void HandleGame(Game game, int pins)
        {
            try
            {
                game.Roll(pins);
            }
            catch (InvalidFrameException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.Green;
                PrintScoreBoard(game);

                if (game.TotalRolls >= 20 && !game.IsGameDone)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"BONUS ROLL!");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
            }
        }

        private static int HandlePlayerInput(Game game, int pins)
        {
            try
            {
                if (!game.IsGameDone)
                    pins = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            }
            catch
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Only numeric input please!");
            }
            finally
            {
                if (pins != -1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"You knocked over {pins} pins!");
                }
            }

            return pins;
        }

        private static void PrintScoreBoard(Game game)
        {
            Console.WriteLine($"Current frame: {game.CurrentFrameNumber}");
            Console.WriteLine($"Current score: {game.Score}");
            Console.WriteLine(game.ToString());
            Console.WriteLine($"Enter number to roll:");
        }
    }
}

