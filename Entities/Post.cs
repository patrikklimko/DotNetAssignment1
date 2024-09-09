namespace Entities;

public class Post
{
    public int Id {get; set;}
    public string Title {get; set;}
    public string Content {get; set;}
    public User UserId {get; set;}
    public User User { get; set; }
    
    public Post(int id, string title, string content, User userId, User user)
    {
        Id = id;
        Title = title;
        Content = content;
        UserId = userId;
        User = user;
    }
}