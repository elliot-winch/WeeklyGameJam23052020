using System;

public static class Extensions 
{
    public static T GetRandom<T>(this T[] array, Random random = null)
    {
        random = random ?? new Random();

        return array[random.Next(0, array.Length)];
    }
}
