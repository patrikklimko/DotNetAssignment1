namespace Entities;

public class Comment
{
    public int CommentId { get; set; }
    public string Text { get; set; }

    public Comment(int commentId, string text)
    {
        CommentId = commentId;
        Text = text;
    }
    

}