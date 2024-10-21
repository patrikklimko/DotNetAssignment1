using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";
    public List<Comment> comments = new List<Comment>();
    
    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment>? comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);
        this.comments = comments; // Update field
        int maxId = comments.Count > 0 ? comments.Max(c => c.CommentId) : 0;
        var newComment = new Comment { CommentId = maxId + 1, Text = comment.Text, PostId = comment.PostId, UserId = comment.UserId };
        comments.Add(newComment);
        this.comments = comments; // Ensure the main field is updated
        await SaveToFileAsync();
        return newComment; // Return the created comment
    }

    public async Task UpdateAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment>? comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);
        this.comments = comments; // Update field
        Comment? existingComment = comments.SingleOrDefault(c => c.CommentId == comment.CommentId);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID {comment.CommentId} not found");
        }
        comments.Remove(existingComment);
        comments.Add(comment);
        this.comments = comments; // Update field
        await SaveToFileAsync();
    }

    public async Task DeleteAsync(int postId)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment>? comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);
        this.comments = comments; // Update field
        Comment? existingComment = comments.SingleOrDefault(c => c.CommentId == postId);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{postId}' not found");
        }
        comments.Remove(existingComment);
        this.comments = comments; // Update field
        await SaveToFileAsync();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment>? comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);
        Comment? singleCommentGet = comments.SingleOrDefault(c => c.CommentId == id);
        if (singleCommentGet is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID {id} not found"); 
        }
        return singleCommentGet; // Direct return, no need for Task.FromResult
    }

    public IQueryable<Comment> GetMany()
    {
        string commentsAsJson = File.ReadAllText(filePath); // Read synchronously
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!; // Deserialize
        return comments.AsQueryable(); // Convert list to IQueryable
    }


    public async Task<IQueryable<Comment>> GetManyAsync()
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }
    
    private async Task SaveToFileAsync()
    {
        string commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }
}
