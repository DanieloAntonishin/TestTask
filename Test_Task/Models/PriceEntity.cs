namespace Test_Task.Models;

public class PriceEntity : Entity
{
    public int UserId { get; set; }
    public int Price { get; set; }
    public int RequestCount { get; set; }
}
