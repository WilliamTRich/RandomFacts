#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RandomFacts.Models;
public class User
{
    [Key]
    public int UserId { get; set; }
    [Required(ErrorMessage = "Username is required.")]
    [MinLength(3, ErrorMessage = "Username must be greater than two characters.")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    [UniqueEmail]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be greater than 7 characters.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    //List of Facts
    //List of Associations
    public List<FactModel> Facts { get; set; } = new List<FactModel>();
    public List<Association> Associations { get; set; } = new List<Association>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("Password")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }
}

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Email is required!");
        }

        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
        if (_context.Users.Any(u => u.Email == value.ToString()))
        {
            return new ValidationResult("Email must be unique.");
        }
        else
        {
            return ValidationResult.Success;
        }
    }
}