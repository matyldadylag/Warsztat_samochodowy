using System.ComponentModel.DataAnnotations;

public class CommentCreateDto
{
    public Guid ServiceOrderId { get; set; }
    [Required]
    public string Author { get; set; } = string.Empty;
    [Required]
    [MaxLength(1000)]
    public string Content { get; set; } = string.Empty;
}
