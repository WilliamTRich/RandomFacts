#pragma warning disable CS8618
namespace RandomFacts.Models;
public class MyViewModel
{
    public User User { get; set; }
    public List<User> Users { get; set; }

    public FactModel Fact { get; set; }
    public List<FactModel> Facts { get; set; }

    public Association Association { get; set; }
    public List<Association> Associations { get; set; }
}