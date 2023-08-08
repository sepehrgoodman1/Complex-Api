using Taav.Contracts.Attributes.Exceptions;

namespace Taav.Contracts.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class TaavPrecisionAttribute : Attribute
{
    public TaavPrecisionAttribute(int precision, int scale)
    {
        if (precision < 0)
        {
            throw new InvalidPrecisionException();
        }

        if (scale < 0)
        {
            throw new InvalidScaleException();
        }

        Precision = precision;
        Scale = scale;
    }

    public TaavPrecisionAttribute(int precision)
    {
        if (precision < 0)
        {
            throw new InvalidPrecisionException();
        }

        Precision = precision;
    }

    public int Precision { get; }
    public int? Scale { get; }
}