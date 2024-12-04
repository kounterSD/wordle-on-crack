using System.Globalization;

namespace wordle_on_crack;


public class PlayGame
{
    Dictionary<int, string> wordDict; //word dictionary
    Dictionary<int, string> guessed = new Dictionary<int, string>(); //guessed correct dictionary
    int tries=5; //how many tries you get
    Leaderboard leaderboard = new Leaderboard();
    public void StartGame(string username)
    {
        wordDict = FetchWord();
        
        //initialising the _____ display
        for (int i=0; i <wordDict.Count; i++)
        {
            guessed.Add(i, "_");
        };
        do
        {
            Console.WriteLine("\n"+DisplayGuessedWord());
            Console.WriteLine("Tries Available: " + tries);
            Console.WriteLine("Enter guess letter: ");
            char guess= Console.ReadKey().KeyChar;
            CheckGuess(guess.ToString());
            if (IfWon())
            {
                break;
            }
        } while (tries>0);
        
        //if user loses
        if (tries == 0)
        {
            Console.WriteLine("\nYou lost...the word was : \n"+ShowAnswer());
            Console.WriteLine("Your Score: "+ leaderboard.GetHighscore(username));
        }
        
        //if user wins
        if (tries != 0)
        {
            leaderboard.UpdateScore(username);
            Console.WriteLine("Congratulations!! You guessed the Word! Added 1 point!");
            Console.WriteLine("Your Score: "+ leaderboard.GetHighscore(username));

        }

    }

    //function return a dictionary[index, letter/char] of random word from the wordlist.txt file.
    public Dictionary<int, string> FetchWord()
    {
        string wordlist = "./../../../wordlist.txt";
        string[] words;
        string word;
        try
        {
            words = File.ReadAllLines(wordlist);
            Random rnd = new Random();
            word = words[rnd.Next(words.Length)].ToLower();
            Dictionary<int, string> theword = new Dictionary<int, string>();
            for (int i = 0; i < word.Length; i++)
            {
                theword.Add(i, word[i].ToString());
            }
            return theword;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    //Displays the guessed dict in string format.
    public string DisplayGuessedWord()
    {
        string guessedString = "";
        foreach (var item in guessed)
        {
            guessedString += item.Value;
        }
        return guessedString;
    }

    //]checks if the userguess is correct and changes (key,value) in the 'guessed dict'. Else tries--
    public void CheckGuess(string userGuess)
    {
        int occurences = 0;
        foreach (var item in wordDict)
        {
            if (userGuess == item.Value) 
            { 
                guessed[item.Key] = item.Value;
                occurences++;
            } 
        }

        if (occurences == 0)
        {
            tries--;
        }

    }
    
    //to see the answer//
    public string ShowAnswer()
    {
        string answer = "";
        foreach (var item in wordDict)
        {
             
            answer = answer + item.Value;
        }
        return answer;
    }

    public bool IfWon()
    {
        if (!DisplayGuessedWord().Contains("_"))
        {
            return true;
        }
        return false;
    }
    
}