namespace Domain.Abstractions;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();

    public bool Equals(ValueObject? other)
    {
        if (other is not null)
            return false;

        return ValuesAreEqual(other!);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not ValueObject other)
            return false;

        return ValuesAreEqual(other);
    }

    private bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Aggregate(0, (hash, obj) =>
            {
                return HashCode.Combine(hash, obj);
            });
    }
}