using System;
using System.Collections.Generic;
using System.IO;

namespace DailyJournalApp
{
    class Program
    {
        // List of prompts
        static List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        // List to store journal entries
        static List<Entry> journal = new List<Entry>();

        // Entry class to store each journal entry
        class Entry
        {
            public string Prompt { get; set; }
            public string Response { get; set; }
            public DateTime Date { get; set; }

            public Entry(string prompt, string response, DateTime date)
            {
                Prompt = prompt;
                Response = response;
                Date = date;
            }

            public override string ToString()
            {
                return $"Prompt: {Prompt}\nResponse: {Response}\nDate: {Date}\n";
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        WriteNewEntry();
                        break;
                    case "2":
                        DisplayJournal();
                        break;
                    case "3":
                        SaveJournal();
                        break;
                    case "4":
                        LoadJournal();
                        break;
                    case "5":
                        Console.WriteLine("Exiting the program.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number from 1 to 5.");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Write entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal to a file");
            Console.WriteLine("4. Load journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice (1-5): ");
        }

        static void WriteNewEntry()
        {
            // Randomly select a prompt
            string prompt = prompts[new Random().Next(prompts.Count)];
            Console.WriteLine($"Prompt: {prompt}");
            Console.Write("Your response: ");
            string response = Console.ReadLine();

            Entry newEntry = new Entry(prompt, response, DateTime.Now);
            journal.Add(newEntry);

            Console.WriteLine("Entry recorded successfully.");
        }

        static void DisplayJournal()
        {
            if (journal.Count == 0)
            {
                Console.WriteLine("Journal is empty.");
            }
            else
            {
                foreach (var entry in journal)
                {
                    Console.WriteLine(entry);
                }
            }
        }

        static void SaveJournal()
        {
            Console.Write("Enter filename to save journal (e.g., journal.txt): ");
            string filename = Console.ReadLine();

            try
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    foreach (var entry in journal)
                    {
                        writer.WriteLine($"Prompt: {entry.Prompt}");
                        writer.WriteLine($"Response: {entry.Response}");
                        writer.WriteLine($"Date: {entry.Date}");
                        writer.WriteLine();
                    }
                }
                Console.WriteLine($"Journal saved successfully to {filename}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error: Could not save journal to {filename}. {e.Message}");
            }
        }

        static void LoadJournal()
        {
            Console.Write("Enter filename to load journal from (e.g., journal.txt): ");
            string filename = Console.ReadLine();

            try
            {
                journal.Clear(); // Clear current journal entries

                using (StreamReader reader = new StreamReader(filename))
                {
                    string line;
                    string prompt = null;
                    string response = null;
                    DateTime date = DateTime.MinValue;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("Prompt: "))
                        {
                            prompt = line.Substring(8);
                        }
                        else if (line.StartsWith("Response: "))
                        {
                            response = line.Substring(10);
                        }
                        else if (line.StartsWith("Date: "))
                        {
                            date = DateTime.Parse(line.Substring(6));
                            journal.Add(new Entry(prompt, response, date));
                        }
                    }
                }
                Console.WriteLine($"Journal loaded successfully from {filename}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error: Could not load journal from {filename}. {e.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine($"Error: Invalid format in {filename}");
            }
        }
    }
}
