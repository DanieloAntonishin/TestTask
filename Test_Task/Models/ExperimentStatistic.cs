namespace Test_Task.Models;

public class ExperimentStatistic
{
    public int UserCount { get; set; }
    public Dictionary<string,int>? ExpStatistic { get; set; }
    public int RequestCount { get; set; }
}
