using System;

public class SubscriptionValue<T>
{
    private T mValue;
    private Action<SubscriptionValue<T>> mOnValueChanged;

    public T Value
    {
        get => mValue;
        set
        {
            mValue = value;
            mOnValueChanged?.Invoke(this);
        }
    }

    public SubscriptionValue(T startingValue)
    {
        mValue = startingValue;
    }

    public void Subscribe(Action<SubscriptionValue<T>> action)
    {
        action?.Invoke(this);
        mOnValueChanged += action;
    }

    public void Unsubscribe(Action<SubscriptionValue<T>> action)
    {
        mOnValueChanged -= action;
    }
}
