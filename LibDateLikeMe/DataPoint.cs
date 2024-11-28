using static System.Math;
using static LibDateLikeMe.DataPointCalculations;

namespace LibDateLikeMe;

/// <summary>
/// Three-dimensional datapoint used for calculation. Coordinates are within
/// [-20; 20] as rational numbers.
/// </summary>
/// <param name="x">X-coordinate</param>
/// <param name="y">Y-coordinate</param>
/// <param name="z">Z-coordinate</param>
public readonly struct DataPoint(double x, double y, double z)
{
    /// <summary>
    /// Value of this DataPoint's coordinate in the X dimension.
    /// </summary>
    public readonly double X = ConvertNumberToValidInterval(x);
    /// <summary>
    /// Value of this DataPoint's coordinate in the Y dimension.
    /// </summary>
    public readonly double Y = ConvertNumberToValidInterval(y);
    /// <summary>
    /// Value of this DataPoint's coordinate in the Z dimension.
    /// </summary>
    public readonly double Z = ConvertNumberToValidInterval(z);


    /// <summary>
    /// Gets the dimensions as a string.
    /// </summary>
    /// <returns>The dimensions as a string.</returns>
    public override string ToString() => $"({X}, {Y}, {Z})";
}

public static class DataPointCalculations
{
    /// <summary>
    /// Internal method to deal with values that are outside the valid interval
    /// range [-20; 20].
    /// </summary>
    /// <param name="originalValue">The original value before checking.</param>
    /// <returns>
    /// The original value if in [-20; 20].
    /// 20 if the value is greater than 20.
    /// -20 if the value is lesser than -20.
    /// 0 if none of the above are truthy.
    /// </returns>
    internal static double ConvertNumberToValidInterval(double originalValue) =>
        originalValue is > -20.0 or < 20.0
            ? originalValue
            : originalValue switch
            {
                > 20.0 => 20.0,
                < -20.0 => -20.0,
                _ => 0.0
            };

    /// <summary>
    /// Determines if a comparePoint is within the likeness factor
    /// of a centerPoint.
    /// </summary>
    /// <param name="centerPoint">The point to take its center from.</param>
    /// <param name="comparePoint">The point that is being compared to the
    /// center.</param>
    /// <param name="likenessFactor">The likeness factor.</param>
    /// <returns>True if the comparePoint is within the likeness factor of the
    /// centerPoint, otherwise false.</returns>
    public static bool IsWithinLikenessFactor(
        this DataPoint centerPoint,
        DataPoint comparePoint,
        double likenessFactor
        ) =>
            Pow(comparePoint.X - centerPoint.X, 2) +
            Pow(comparePoint.Y - centerPoint.Y, 2) +
            Pow(comparePoint.Z - centerPoint.Z, 2)
            <= Pow(likenessFactor, 2);
}
