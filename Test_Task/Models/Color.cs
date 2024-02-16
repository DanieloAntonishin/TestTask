namespace Test_Task.Models;

public class Color
{
    private static string[] colorName = { "#FF0000", "#00FF00", "#0000FF" };

    public static string GetRandomColor()
    {
        var rand = new Random();
        return colorName[rand.Next(0, 3)];
    }
}
