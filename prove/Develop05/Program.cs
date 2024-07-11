using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }

    protected Goal(string name, int points)
    {
        Name = name;
        Points = points;
    }

    public abstract void RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetDetailsString();
}

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, int points) : base(name, points)
    {
        _isComplete = false;
    }

    public override void RecordEvent()
    {
        _isComplete = true;
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetDetailsString()
    {
        return $"{Name} [{(IsComplete() ? "X" : " ")}]";
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent() { }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetDetailsString()
    {
        return $"{Name} [ ]";
    }
}

class ChecklistGoal : Goal
{
    private int _timesCompleted;
    private int _targetCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, int points, int targetCount, int bonusPoints) : base(name, points)
    {
        _timesCompleted = 0;
        _targetCount = targetCount;
        _bonusPoints = bonusPoints;
    }

    public override void RecordEvent()
    {
        _timesCompleted++;
    }

    public override bool IsComplete()
    {
        return _timesCompleted >= _targetCount;
    }

    public override string GetDetailsString()
    {
        return $"{Name} [{_timesCompleted}/{_targetCount}] {(IsComplete() ? "[X]" : "[ ]")}";
    }

    public int GetBonusPoints()
    {
        return IsComplete() ? _bonusPoints : 0;
    }
}

class GoalManager
{
    private List<Goal> _goals;
    public int TotalPoints { get; private set; }

    public GoalManager()
    {
        _goals = new List<Goal>();
        TotalPoints = 0;
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void RecordEvent(string goalName)
    {
        Goal goal = _goals.FirstOrDefault(g => g.Name == goalName);
        if (goal != null)
        {
            goal.RecordEvent();
            TotalPoints += goal.Points;
            if (goal is ChecklistGoal checklistGoal)
            {
                TotalPoints += checklistGoal.GetBonusPoints();
            }
        }
    }

    public void ShowGoals()
    {
        foreach (var goal in _goals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(TotalPoints);
            foreach (var goal in _goals)
            {
                writer.WriteLine($"{goal.GetType().Name},{goal.Name},{goal.Points}");
            }
        }
    }

    public void LoadGoals(string filename)
    {
        if (File.Exists(filename))
        {
            _goals.Clear();
            using (StreamReader reader = new StreamReader(filename))
            {
                TotalPoints = int.Parse(reader.ReadLine());
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    string type = parts[0];
                    string name = parts[1];
                    int points = int.Parse(parts[2]);

                    if (type == nameof(SimpleGoal))
                    {
                        AddGoal(new SimpleGoal(name, points));
                    }
                    else if (type == nameof(EternalGoal))
                    {
                        AddGoal(new EternalGoal(name, points));
                    }
                    else if (type == nameof(ChecklistGoal))
                    {
                        // Load ChecklistGoal with additional data as needed
                    }
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.AddGoal(new SimpleGoal(" ", 1000));
        manager.AddGoal(new EternalGoal(" ", 100));
        manager.AddGoal(new ChecklistGoal(" ", 50, 10, 500));

        bool running = true;
        while (running)
        {
            Console.WriteLine("1. Add Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Show Goals");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. Show Score");
            Console.WriteLine("7. Exit");
            Console.Write("Select an option: ");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.Write("Enter goal type (Simple, Eternal, Checklist): ");
                    string type = Console.ReadLine();
                    Console.Write("Enter goal name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter points: ");
                    int points = int.Parse(Console.ReadLine());
                    if (type == "Simple")
                    {
                        manager.AddGoal(new SimpleGoal(name, points));
                    }
                    else if (type == "Eternal")
                    {
                        manager.AddGoal(new EternalGoal(name, points));
                    }
                    else if (type == "Checklist")
                    {
                        Console.Write("Enter target count: ");
                        int targetCount = int.Parse(Console.ReadLine());
                        Console.Write("Enter bonus points: ");
                        int bonusPoints = int.Parse(Console.ReadLine());
                        manager.AddGoal(new ChecklistGoal(name, points, targetCount, bonusPoints));
                    }
                    break;
                case 2:
                    Console.Write("Enter goal name: ");
                    string goalName = Console.ReadLine();
                    manager.RecordEvent(goalName);
                    break;
                case 3:
                    manager.ShowGoals();
                    break;
                case 4:
                    Console.Write("Enter filename: ");
                    string saveFilename = Console.ReadLine();
                    manager.SaveGoals(saveFilename);
                    break;
                case 5:
                    Console.Write("Enter filename: ");
                    string loadFilename = Console.ReadLine();
                    manager.LoadGoals(loadFilename);
                    break;
                case 6:
                    Console.WriteLine($"Total Points: {manager.TotalPoints}");
                    break;
                case 7:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
