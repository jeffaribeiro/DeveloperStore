using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Rating : ValueObject
{
    public double Rate { get; set; }
    public int Count { get; set; }

    public Rating() { }

    public Rating(double rate, int count)
    {
        Rate = rate;
        Count = count;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Rate;
        yield return Count;
    }
}
