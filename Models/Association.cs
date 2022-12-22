#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace RandomFacts.Models;
public class Association
{
    [Key]
    public int AssociationId { get; set; }

    public int UserId { get; set; }
    public int FactId { get; set; }

    public User? User { get; set; }
    public FactModel? Fact { get; set; }
}