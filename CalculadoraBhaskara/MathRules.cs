namespace CalculadoraBhaskara;
public static class MathRules
{
    public static double Delta(double a, double b, double c) => b * b - 4 * a * c;
    public static (double X1, double X2) Roots(double a, double b, double delta)
    {
        if (a == 0 || delta < 0) throw new ArgumentException("Equação sem raízes reais de segundo grau.");
        return ((-b + Math.Sqrt(delta)) / (2 * a), (-b - Math.Sqrt(delta)) / (2 * a));
    }
}