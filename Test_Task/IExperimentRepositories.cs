using Test_Task.Models;

namespace Test_Task;

public interface IExperimentRepositories
{
    Task<string> GetButtonResultAsync(User user);
    Task<int> GetPriceResultAsync(User user);
    ExperimentStatistic GetStatisticAsync(string experimentName);
}
