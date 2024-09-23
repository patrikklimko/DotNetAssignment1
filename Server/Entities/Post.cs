namespace Entities;

public class Post
{
    public int Id {get; set;}
    public string Title {get; set;}
    public string Content {get; set;}
    public int UserId {get; set;}
    public string User { get; set; }
    
    public Post(int id, string title, string content, int userId, string user)
    {
        Id = id;
        Title = title;
        Content = content;
        UserId = userId;
        User = user;
    }

    public Post(string title, string content, int userId)
    {
        throw new NotImplementedException();
    }
}