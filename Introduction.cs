using System;
using System.Threading;
using System.IO;
using w19_Assignment_HangMan;

public class Introduction
{
    public static void sleep() { Thread.Sleep(3000); }
    public static void Intro()
    {
        Console.Clear();

        Console.WriteLine("GAME RULES:"); Thread.Sleep(1000);
        Console.WriteLine();
        Console.WriteLine("The game starts with an empty board, and I will think of a word."); sleep();
        Console.WriteLine("And I'm not gonna tell you what that is."); sleep();
        Console.WriteLine("You have to figure it out yourself by guessing letters."); sleep();
        Console.WriteLine("It will be tough!"); sleep();
        Console.WriteLine("The board contains as many underlines as many letters are in the word."); sleep();
        Console.WriteLine("You start with 12 stars representing your health."); sleep();
        Console.WriteLine("If your guess is correct, the letter will appear on the board."); sleep();
        Console.WriteLine("If not, you loose one star."); sleep();
        Console.WriteLine("It's kinda frustrating, and you don't want that, trust me!"); sleep();
        Console.WriteLine("If you loose all your stars, you're dead."); sleep();
        Console.WriteLine("Yap... That's life."); sleep();
        Console.WriteLine("I'll remember all the letters you chose,"); sleep();
        Console.WriteLine("   and if you pick a letter you have already chosen I'll let you know."); sleep();
        Console.WriteLine("You get no penalty for that."); sleep();
        Console.WriteLine("If you find all the letters for the word, you win."); sleep();
        Console.WriteLine("But that will never happen."); sleep();
        Console.WriteLine("NEVER!"); Thread.Sleep(1000);

        string answer = "";
        do
        {
            try
            {
                Console.Write("Do you wanna see the rules again? (Y/N)");
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
            Program.intro = false;
        }
        
    }
    
}
