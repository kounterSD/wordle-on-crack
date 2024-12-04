using System;

namespace wordle_on_crack
{
    
    class WordleOnCrack
    {
        private static void Main()
        {
            PlayGame game = new PlayGame();
            Leaderboard leaderboard = new Leaderboard();
            Player player = new Player();
            
            //Welcome Screen
            Console.WriteLine("########-Welcome-to-########\n######-Wordle-on-Crack-#####");
            Console.WriteLine("Enter your username: ");
            player.Username = Console.ReadLine();
            string username = player.Username;
            
            //creates new user, if usernotfound
            leaderboard.CreatePlayer(username);
            Console.WriteLine("-1. Play\n-2. View Leaderboard");

            //Main Menu    
            int option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    
                    game.StartGame(username);
                    break;
                
                case 2:
                    leaderboard.ShowLeaderboard();
                    break;
            }
        }
    }
}
