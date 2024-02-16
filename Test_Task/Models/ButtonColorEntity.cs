namespace Test_Task.Models;

public class ButtonColorEntity: Entity
{
    public int UserId { get; set; }
    public string ColorName { get; set; }
    public int RequestCount { get; set; }
}
