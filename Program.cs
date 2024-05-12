using System;
using System.Threading;
using System.Text;
using System.ComponentModel.Design;

namespace w19_Assignment_HangMan
{
    internal class Program
    {
        // ------- Global declarations ------- //

        public static bool intro = true;
        public static char playersChoice = default;             // Letter the player chooses
        public static int hp = 12;                              // Players starting health points
        public static int hit = 1;                              // A character a hit or not   
        public static int hitCntPos, hitCntNeg = 0;             // Counter for messaging
        public static string player = "";                       // Players name
        public static bool win = false;                         // Endgame variable
        public static bool message = false;                     // Messaging variable
        public static bool gameOver = false;                    // A lost game variable
        public static bool again = true;                        // Replay variable

        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello there player!");
            Thread.Sleep(1000);
            Console.WriteLine("Before we start");
            Thread.Sleep(500);
            do
            {
                Console.Write("Please enter you name! ");               // Checking validation
                player = Convert.ToString(Console.ReadLine());
            }
            while (player == "");

            Console.Clear();
            Console.WriteLine($"Welcome to the HangMan game {player}!");
            Thread.Sleep(2000);

            string answer = ""; 
            do
            {
                try
                {
                    Console.Write("Do you wanna see the game rules? (Y/N)");
                    answer = Convert.ToString(Console.ReadLine().ToUpper());
                }
                catch (Exception FormatException)                                     // In case an exception
                {
                    Console.WriteLine("Let's stick to Y or N!");
                }
            }
            while (answer != "Y" && answer != "N");

            if (answer == "N")
            {
                intro = false;
            }
            

            while (again)                                           // Preparation to start
            {
                List<char> chosen = new List<char>();               // Already chosen characters
                chosen.Clear();
                if (intro == true) { Introduction.Intro(); }
                Console.WriteLine("Let's get started!"); Thread.Sleep(2000);
                string word = WordCatcher.GetWord();                // Calling for the word
                char[] charsOfWord = word.ToCharArray();            // The random word
                char[] board = new char[charsOfWord.Length];        // Game board
                
                board = BoardFiller(charsOfWord, board, playersChoice); //- First run to fill with "_"
            
                Console.WriteLine(word); /////!!!!!!!!!!!!!!!
                
                while (!win && !gameOver)                           // The game begins 
                {

                    bool noErr = false;
                    do                                              // Type in a letter
                    {
                        Console.Write($"Choose a letter {player}! ");

                        try                                         // Check if a decimal
                        {
                            int nr; string ltr = Console.ReadLine();
                            bool x = int.TryParse(ltr, out nr);
                            if (x) { throw new FormatException(); }

                            playersChoice = Convert.ToChar(ltr.ToUpper());  // or if a letter
                            noErr = true;
                        }
                        catch (Exception FormatException)           // Check if empty
                        {
                            Console.WriteLine("You have to type in a letter");
                        }

                    }
                    while (!noErr);

                    if (chosen.Contains(playersChoice))                 // Letter was already chosen
                    {
                        Console.WriteLine("You have chosen this before");
                    }
                    else                                                // All good 
                    {
                        chosen.Add(playersChoice);
                        board = BoardFiller(charsOfWord, board, playersChoice);
                        sendMessage(message);                           // Calling messaging method 
                    }
                    win = !board.Contains('_');
                    gameOver = hp == 0;

                }
                again = EndGame();
            }
            Console.WriteLine("Thanks for the game!");
            Thread.Sleep(1000);
            Console.WriteLine("Goodbye!");

            Console.ReadKey();
        }

// -------------------------------- //

        static char[] BoardFiller(char[] charsOfWord, char[] board, char choice)                   //- Filling the board with characters
        {
            Console.Clear();
            Console.WriteLine("Your word is:");
            
            int i = 0;
            foreach (char c in charsOfWord)
            {
                if      (c == choice)                           // We have a match
                { 
                    board[i] = choice;
                    hit++;
                    message = true;
                } 
                else if (choice != default && c != choice)      // No match
                {
                    message = (hit != 0) ? true : false;
                }
                else if (c == ' ')                              // It's the space character
                {
                    board[i] = c;
                }
                else                                            // No match or first run
                {
                    board[i] = '_';
                }
                Console.Write($"{board[i]} ");
                i++;
            }

            if (hit == 0)
            {
                hp--; 
                message= false;
            }
            
            hit = 0;
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Your health is:");
            HPHandler(hp);
            Console.WriteLine();
            return board;
        }

// -------------------------------- //

        static void sendMessage(bool message)           // The messaging method
        {
            string[] msgArrayPos = new string[]
            {
                "Nice!","You get it!","Good move!","Brilliant!","How do you do this?","You're killing it!","You practiced it, didn't you?","You must be cheating!","HOW???","Impossible!"     
            };
            string[] msgArrayNeg = new string[]
            {
                "Nope!","Not that one.","Are you sure you know this game?","It must be hurt man!","I feel bad for you already...","Are you kidding me?","Why do you hate me so much???","I'm speechless...","It was a terrible idea.","Well, at least you try."
            };

            if (hitCntPos >= 10) { hitCntPos -= 3; }    // Overflow control
            if (hitCntNeg >= 10) { hitCntNeg -= 3; }    // Overflow control

            if (message)                                // In case of good answer
            {
                if (hitCntNeg != 0) { hitCntNeg=0; }
                Console.WriteLine(msgArrayPos[hitCntPos]);
                hitCntPos++;
            }

            else                                        // In case of wrong answer
            {
                if (hitCntPos != 0) { hitCntPos = 0; }
                Console.WriteLine(msgArrayNeg[hitCntNeg]);
                hitCntNeg++;
            }
        }

// -------------------------------- //

        static void HPHandler(int hp)
        {
            Console.OutputEncoding = Encoding.UTF8;             // Sets font encoding table
            
            if (hp > 6)                                         // Full HP
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (hp > 3 && hp <= 6)                         // Half HP
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else                                                // Low HP
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
           
            for (int i = 1; i <= hp; i++)                       // Drawing stars
            {
                Console.Write('\u2605'+" ");
            }
            
            Console.ForegroundColor = ConsoleColor.Gray;        // Set back to default
        }

        // -------------------------------- //

        static bool EndGame()
        {
            switch (win)
            {
                case true :
                    Console.Clear();
                    Console.WriteLine("Congratulations!");
                    Thread.Sleep(1000);
                    Console.WriteLine("You won!");
                    Thread.Sleep(2000);
                    break;
                case false :
                    Console.Clear();
                    Console.WriteLine("You're Dead.");
                    Thread.Sleep(1000);
                    Console.WriteLine("Kinda...");
                    Thread.Sleep(2000);
                    break;
            }

                string answer = "";
            do
            {
                try
                {
                    Console.Write("Do you wanna play again? (Y/N)");
                    answer = Convert.ToString(Console.ReadLine().ToUpper());
                }
                catch (Exception FormatException)            // In case an exception
                {
                    Console.WriteLine("Let's stick to Y or N!");
                }
            }
            while (answer != "Y" && answer != "N");
            
            if (answer == "Y")
            { again = true; }
            else { again = false; }
            
            win = false;
            gameOver = false;
            hp = 12;                               
            hit = 1;
            hitCntPos = 0;
            hitCntNeg = 0;
            message = false;
            playersChoice = default;
        
            return again;
        }

    }
}
