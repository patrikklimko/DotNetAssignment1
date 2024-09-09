namespace Entities;

public class User
{
    public string Name {get; set;}
    public int UserId {get; set;}

    public User(string name, int userId)
    {
        UserId = userId;
        Name = name;
    }
    
}