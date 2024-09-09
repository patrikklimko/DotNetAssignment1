namespace Entities;

public class Comment
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    public User UserId { get; set; }
    public Post PostId { get; set; }

    public Comment(int commentId, string text, User userId, Post postId)
    {
        CommentId = commentId;
        Text = text;
        UserId = userId;
        PostId = postId;
    }
    

}