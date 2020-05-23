using System;

[Serializable]
public class Cult 
{
    public int StartingWealth;
    public int StartingLoyality;
    public int StartingReputation;

    public SubscriptionValue<int> Wealth { get; private set; }
    public SubscriptionValue<int> Loyality { get; private set; }
    public SubscriptionValue<int> Reputation { get; private set; }

    public Cult()
    {
        Wealth = new SubscriptionValue<int>(StartingWealth);
    }
}
