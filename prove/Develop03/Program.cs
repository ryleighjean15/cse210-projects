using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("Philippians 4:13");
        Scripture scripture = new Scripture(reference, "I can do all things through Christ which strengtheneth me.");

        Random random = new Random();

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());

            Console.WriteLine("\n\nPress Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords(random);

            if (scripture.AllWordsHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());

                Console.WriteLine("\n\nAll words are hidden. The program will now exit.");
                break;
            }
        }
    }
}

class Reference
{
    public string Text { get; }

    public Reference(string text)
    {
        Text = text;
    }
}

class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public override string ToString()
    {
        return IsHidden ? "_____" : Text;
    }
}

class Scripture
{
    public Reference Reference { get; }
    private List<Word> Words { get; }

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public string GetDisplayText()
    {
        return $"{Reference.Text}\n\n{string.Join(" ", Words)}";
    }

    public void HideRandomWords(Random random)
    {
        int wordsToHide = random.Next(1, Math.Max(1, Words.Count / 10));

        for (int i = 0; i < wordsToHide; i++)
        {
            int index;
            do
            {
                index = random.Next(Words.Count);
            } while (Words[index].IsHidden);

            Words[index].Hide();
        }
    }

    public bool AllWordsHidden()
    {
        return Words.All(word => word.IsHidden);
    }
}
