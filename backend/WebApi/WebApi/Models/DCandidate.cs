using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models;

public class DCandidate
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [Phone]
    [StringLength(16)]
    public string Mobile { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Range(18, 100, ErrorMessage = "Donor must be at least 18 years old.")]
    public int Age { get; set; }

    [Required]
    [RegularExpression(
        "^(A|B|AB|O)[+-]$",
        ErrorMessage = "Use a valid blood group, e.g. A+ or O-.")]
    [Column(TypeName = "nvarchar(3)")]
    public string BloodGroup { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Address { get; set; } = string.Empty;
}