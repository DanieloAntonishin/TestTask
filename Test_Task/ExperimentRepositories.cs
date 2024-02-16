using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Test_Task.Models;

namespace Test_Task;

public class ExperimentRepositories : IExperimentRepositories
{
    private MsSqlDbContext _dbContext { get; set; }

    public ExperimentRepositories(
       MsSqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> GetButtonResultAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        try
        {
            string ?buttonColor;    // resault color for user
            var experiment = new ButtonColorEntity() { Id = Guid.NewGuid(), UserId = user.Id, ColorName = Color.GetRandomColor(), RequestCount = 1 };    // Create entity for table
            if (_dbContext.ButtonExperimentEntities.Select(e => e.UserId).Contains(user.Id))      // If user exit, update count of request to server
            {
                var ex = _dbContext.ButtonExperimentEntities.Where(e => e.UserId == user.Id);
                ex.ExecuteUpdate(u => u.SetProperty(r => r.RequestCount, ex.Select(o => o.RequestCount).FirstOrDefault() + 1));
                buttonColor = ex.Select(c=>c.ColorName).FirstOrDefault();   // take user color of button
            }
            else                                                                            // Add if not exist
            {
                var res = await _dbContext.ButtonExperimentEntities.AddAsync(experiment); 
                buttonColor = res.Entity.ColorName;
            }
            await _dbContext.SaveChangesAsync();
            return buttonColor;
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<int> GetPriceResultAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        try
        {
            int price = 0;    // resault price for user
            var experiment = new PriceEntity() { Id = Guid.NewGuid(), UserId = user.Id, Price = Price.GetRandomPrice(), RequestCount = 1 };    // Create entity for table
            if (_dbContext.PriceExperimentEntities.Select(e => e.UserId).Contains(user.Id))      // If user exit, update count of request to server
            {
                var ex = _dbContext.PriceExperimentEntities.Where(e => e.UserId == user.Id);
                ex.ExecuteUpdate(u => u.SetProperty(r => r.RequestCount, ex.Select(o => o.RequestCount).FirstOrDefault() + 1));
                price = ex.Select(c => c.Price).FirstOrDefault();   // take price for user
            }
            else                                                                            // Add if not exist
            {
                var res = await _dbContext.PriceExperimentEntities.AddAsync(experiment);
                price = res.Entity.Price;
            }
            await _dbContext.SaveChangesAsync();
            return price;
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public ExperimentStatistic GetStatisticAsync(string experimentName)
    {
        try
        {
            switch(experimentName)
            {
                case ExperimentName.BUTTON_COLORE_EXPERIMENT:
                    {
                        var userCount = _dbContext.ButtonExperimentEntities.Select(f => f.UserId).ToList();     // Get statistic for view
                        var reqCount = _dbContext.ButtonExperimentEntities.Sum(f => f.RequestCount);
                        var colorCount = _dbContext.ButtonExperimentEntities
                        .GroupBy(p => p.ColorName)
                        .Select(g => new { color = g.Key, count = g.Count() })
                        .ToDictionary(k => k.color, i => i.count);

                        var res = new ExperimentStatistic() { UserCount = userCount.Count, RequestCount = reqCount, ExpStatistic = colorCount };
                        return res;
                    }
                case ExperimentName.PRICE_EXPERIMENT:
                    {
                        var userCount = _dbContext.PriceExperimentEntities.Select(f => f.UserId).ToList();     // Get statistic for view
                        var reqCount = _dbContext.PriceExperimentEntities.Sum(f => f.RequestCount);
                        var priceCount = _dbContext.PriceExperimentEntities
                        .GroupBy(p => p.Price)
                        .Select(g => new { price = g.Key, count = g.Count() })
                        .ToDictionary(k => k.price.ToString(), i => i.count);

                        var res = new ExperimentStatistic() { UserCount = userCount.Count, RequestCount = reqCount, ExpStatistic = priceCount };
                        return res;
                    }
                default:
                    throw new Exception("Empty statistic");
            }
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
