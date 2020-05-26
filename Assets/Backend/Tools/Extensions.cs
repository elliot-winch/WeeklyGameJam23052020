using System;

public static class Extensions 
{
    public static T GetRandom<T>(this T[] array, Random random = null)
    {
        random = random ?? new Random();

        return array.Length > 0 ? array[random.Next(0, array.Length)] : default;
    }
}
