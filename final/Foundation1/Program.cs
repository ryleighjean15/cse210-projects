using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        
        List<Video> videos = new List<Video>();

        Video video1 = new Video("Video 1 Title", "Author 1", 300);
        video1.AddComment(new Comment("User1", "Great video!"));
        video1.AddComment(new Comment("User2", "Very informative."));
        video1.AddComment(new Comment("User3", "Thanks for sharing!"));
        videos.Add(video1);

        Video video2 = new Video("Video 2 Title", "Author 2", 450);
        video2.AddComment(new Comment("User4", "Awesome content!"));
        video2.AddComment(new Comment("User5", "Loved it!"));
        video2.AddComment(new Comment("User6", "Keep it up!"));
        videos.Add(video2);

        Video video3 = new Video("Video 3 Title", "Author 3", 600);
        video3.AddComment(new Comment("User7", "Interesting topic."));
        video3.AddComment(new Comment("User8", "Nice explanation."));
        video3.AddComment(new Comment("User9", "Helpful video."));
        videos.Add(video3);

        Video video4 = new Video("Video 4 Title", "Author 4", 200);
        video4.AddComment(new Comment("User10", "Fantastic video!"));
        video4.AddComment(new Comment("User11", "Well done!"));
        video4.AddComment(new Comment("User12", "Clear and concise."));
        videos.Add(video4);

       
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");
            foreach (Comment comment in video.Comments)
            {
                Console.WriteLine($"- {comment.Name}: {comment.Text}");
            }
            Console.WriteLine();
        }
    }
}

class Video
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int Length { get; private set; }
    public List<Comment> Comments { get; private set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }
}

class Comment
{
    public string Name { get; private set; }
    public string Text { get; private set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}
