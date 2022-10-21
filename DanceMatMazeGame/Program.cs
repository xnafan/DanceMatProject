using System;

namespace DanceMatMazeGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new DanceMatMazeGame())
                game.Run();
        }
    }
}
