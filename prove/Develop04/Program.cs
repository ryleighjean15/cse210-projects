using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

abstract class MindfulnessActivity
{
    protected string Name;
    protected string Description;

    public MindfulnessActivity(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void StartActivity()
    {
        Console.WriteLine($"Starting {Name} Activity");
        Console.WriteLine(Description);
        Console.Write("Enter the duration of the activity in seconds: ");
        int duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        PauseWithAnimation(3);
        PerformActivity(duration);
        Console.WriteLine($"Good job! You have completed the {Name} Activity for {duration} seconds.");
        PauseWithAnimation(3);
    }

    protected void PauseWithAnimation(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"Starting in {i}...\r");
            Thread.Sleep(1000);
        }
        Console.Write(new string(' ', 20) + "\r");
    }

    protected abstract void PerformActivity(int duration);
}

class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() 
        : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    { }

    protected override void PerformActivity(int duration)
    {
        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.WriteLine("Breathe in...");
            PauseWithAnimation(4);
            Console.WriteLine("Breathe out...");
            PauseWithAnimation(4);
        }
    }
}

class ReflectionActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity() 
        : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    { }

    protected override void PerformActivity(int duration)
    {
        Console.WriteLine(prompts[new Random().Next(prompts.Count)]);
        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.WriteLine(questions[new Random().Next(questions.Count)]);
            PauseWithAnimation(5);
        }
    }
}

class ListingActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() 
        : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    { }

    protected override void PerformActivity(int duration)
    {
        Console.WriteLine(prompts[new Random().Next(prompts.Count)]);
        Console.WriteLine("You have a few seconds to prepare...");
        PauseWithAnimation(5);
        Console.WriteLine("Start listing items now!");

        DateTime startTime = DateTime.Now;
        List<string> items = new List<string>();

        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            items.Add(Console.ReadLine());
        }

        Console.WriteLine($"You listed {items.Count} items.");
        foreach (var item in items)
        {
            Console.WriteLine($"- {item}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, MindfulnessActivity> activities = new Dictionary<string, MindfulnessActivity>
        {
            { "1", new BreathingActivity() },
            { "2", new ReflectionActivity() },
            { "3", new ListingActivity() }
        };

        while (true)
        {
            Console.WriteLine("\nMindfulness App");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");

            Console.Write("Choose an activity: ");
            string choice = Console.ReadLine();

            if (choice == "4")
                break;

            if (activities.ContainsKey(choice))
                activities[choice].StartActivity();
            else
                Console.WriteLine("Invalid choice. Please try again.");
        }
    }
}
