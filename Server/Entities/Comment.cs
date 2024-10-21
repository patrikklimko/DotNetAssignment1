namespace Entities;

public class Comment
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }

    public Comment(int commentId, string text, int userId, int postId)
    {
        CommentId = commentId;
        Text = text;
        UserId = userId;
        PostId = postId;
    }

    public Comment()
    {
        CommentId = 0;
        Text = string.Empty;
        UserId = 0;
        PostId = 0;
    }
}