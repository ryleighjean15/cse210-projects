using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        string reference = "Proverbs 3:5-6";
        string scriptureText = "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.";

        List<string> words = scriptureText.Split(' ').ToList();
        Random random = new Random();

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{reference}\n");

            foreach (string word in words)
            {
                Console.Write(word + " ");
            }

            Console.WriteLine("\n\nPress Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            HideRandomWords(words, random);

            if (words.All(word => word == "_____"))
            {
                Console.Clear();
                Console.WriteLine($"{reference}\n");

                foreach (string word in words)
                {
                    Console.Write(word + " ");
                }

                Console.WriteLine("\n\nAll words are hidden. The program will now exit.");
                break;
            }
        }
    }

    static void HideRandomWords(List<string> words, Random random)
    {
        int wordsToHide = random.Next(1, Math.Max(1, words.Count / 10));

        for (int i = 0; i < wordsToHide; i++)
        {
            int index;
            do
            {
                index = random.Next(words.Count);
            } while (words[index] == "_____");

            words[index] = "_____";
        }
    }
}
