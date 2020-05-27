using System;

[Serializable]
public class Personality
{
    public int Naivety;
    public int Disillusionment;
    public int Spirituality;

    public static int operator *(Personality a, Personality b)
    {
        if(a == null || b == null)
        {
            return 0;
        }

        return a.Naivety * b.Naivety
            + a.Disillusionment * b.Disillusionment
            + a.Spirituality * b.Spirituality;
    }
}
