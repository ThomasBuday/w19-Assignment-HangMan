using System;
using System.IO;
                
public class WordCatcher
{
    public static string GetWord()
    {
        int serNr = new Random().Next(1, 58109);        //- Seting random nr for a random word
        string word = "";

        try
        {
            string dataPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\data\\wordlist.txt";  //- Word database access

            Dictionary<int, string> myDict = new Dictionary<int, string>();

            int i = 1;
            foreach (string line in File.ReadLines(dataPath))   //- Filling up directory from file
            {
                myDict.Add(i, line);
                i++;
            }

            word = myDict[serNr];                               //- Geting the random file
            word = word.ToUpper();                              //- All uppercase
        }
        catch (Exception e)                                     //- In case whatever happens
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        
        return word;
    }
}