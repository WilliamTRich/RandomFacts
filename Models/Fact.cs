#pragma warning disable CS8618
namespace RandomFacts.Models;
using System.ComponentModel.DataAnnotations;
public class FactModel
{
    [Key]
    public int FactId { get; set; }
    [Required(ErrorMessage = "Fact is required.")]
    [UniqueFact]
    public string fact { get; set; }

    public List<Association> Associations { get; set; } = new List<Association>();
    public User? User { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public class UniqueFactAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Fact is required!");
        }

        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
        FactModel? fact = _context.Facts.FirstOrDefault(f => f.fact == value.ToString());
        if (fact != null)
        {
            return new ValidationResult($"{fact.FactId}");
        }
        else
        {
            return new ValidationResult("");
        }
    }
}