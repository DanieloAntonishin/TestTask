namespace Test_Task.Models;

public class Price
{
    private static PriceRange[] priceRange = {
    new PriceRange(){Value = 10,Chance = 75 },
    new PriceRange(){Value = 20,Chance = 10 },
    new PriceRange(){Value = 50,Chance = 5 },
    new PriceRange(){Value = 5,Chance = 10 }, };

    public static int GetRandomPrice()  
    {
        var rand= new Random();
        int chance = rand.Next(0, 100) + 1;     // random number in range
        int next = 0;                           // add previous result
        for (int index = 0; index < priceRange.Length; index++)
        {
            var pair = priceRange[index];       
            if (chance <= pair.Chance + next)   // comparer chance and current value chance with previous result
                return pair.Value;
            else
                next += pair.Chance;            // if chance higher add current and them will plus with next
        }
        return priceRange[0].Value;             // if happened some trouble
    }
}

public class PriceRange             // Class for price range and their chance
{
    public int Value { set; get; }
    public int Chance { set; get; }
}