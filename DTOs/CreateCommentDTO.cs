using System.ComponentModel.DataAnnotations;

namespace ApiContracts.DTOs;

public class CreateCommentDTO
{
    [Required]
    public string text { get; set; }
    
    [Required]
    public int userId { get; set; }
    
    [Required]
    public int postId { get; set; }
}