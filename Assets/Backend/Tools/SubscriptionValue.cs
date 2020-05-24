using System;

public class SubscriptionValue<T>
{
    private T mValue;
    private Action<T> mOnValueChanged;

    public T Value
    {
        get => mValue;
        set
        {
            mValue = value;
            mOnValueChanged?.Invoke(mValue);
        }
    }

    public SubscriptionValue()
    {
        mValue = default;
    }

    public SubscriptionValue(T startingValue)
    {
        mValue = startingValue;
    }

    public void Subscribe(Action<T> action)
    {
        action?.Invoke(mValue);
        mOnValueChanged += action;
    }

    public void Unsubscribe(Action<T> action)
    {
        mOnValueChanged -= action;
    }
}
