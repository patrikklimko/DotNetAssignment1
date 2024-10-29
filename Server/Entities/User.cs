namespace Entities;

public class User
{
    public string Name {get; set;}
    public int UserId {get; set;}
    public string Password {get; set;}

    public User(string name, int userId, string password)
    {
        UserId = userId;
        Name = name;
        Password = password;
    }

    public User()
    {
        throw new NotImplementedException();
    }
}