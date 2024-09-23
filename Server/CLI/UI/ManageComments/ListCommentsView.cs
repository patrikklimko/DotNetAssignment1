using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ListCommentsView
{
    private readonly ICommentRepository commentRepository;

    public ListCommentsView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public void Show()
    {
        Console.WriteLine();
        ViewCommentsAsync();
    }
    
    private void ViewCommentsAsync()
    {
        IEnumerable<Comment> manyAsync = commentRepository.GetMany();
        List<Comment> comments = manyAsync.OrderBy(c => c.CommentId).ToList();
        
        Console.WriteLine("Comments:");
        Console.WriteLine("[");
        foreach (Comment comment in comments)
        {
            Console.WriteLine($"\tID: {comment.CommentId}, Content: {comment.Text}, UserId: {comment.UserId}, PostId: {comment.PostId}");
        }

        Console.WriteLine("]");
        Console.WriteLine();
    }
}