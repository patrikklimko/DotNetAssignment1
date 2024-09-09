using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository
{
    List<Comment> comments = new List<Comment>();

    public Task<Comment> AddAsync(Comment comment)
    {
        comment.CommentId = comments.Any()
            ? comments.Max(c => c.CommentId) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(c => c.CommentId == comment.CommentId);
        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.CommentId}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int commentId)
    {
        Comment? commentToRemove = comments.SingleOrDefault(c => c.CommentId == commentId);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException($"Comment with ID '{commentId}' not found");
        }

        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int commentId)
    {
        Comment? comment = comments.SingleOrDefault(c => c.CommentId == commentId);
        if (comment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{commentId}' not found");
        }

        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GetManyAsync()
    {
        return comments.AsQueryable();
    }
}