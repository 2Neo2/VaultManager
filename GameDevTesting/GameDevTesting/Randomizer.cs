using DateTime = System.DateTime;

namespace GameDevTesting;

/// <summary>
/// Randomizer class.
/// </summary>
public class Randomizer
{
    private static Random _random = new Random();
    
    /// <summary>
    /// Get random string for fields using LINQ.
    /// </summary>
    /// <param name="length">Length of generated string.</param>
    /// <returns></returns>
    public static string GetRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// Get random int for fields.
    /// </summary>
    /// <returns>Random integer.</returns>
    public static int GetRandomInt()
    {
        return _random.Next(100);
    }

    /// <summary>
    /// Get random date time for fields.
    /// </summary>
    /// <returns>Random date time.</returns>
    public static DateTime GetRandomDateTime()
    {
        DateTime start = new DateTime(1995, 1, 1);
        int range = (DateTime.Today - start).Days; 
        return start.AddDays(_random.Next(range));
    }
}