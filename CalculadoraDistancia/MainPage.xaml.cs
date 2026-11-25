using System.Globalization;
namespace CalculadoraDistancia;
public partial class MainPage : ContentPage
{
    public MainPage() => InitializeComponent();
    private static bool TryNumber(string? value, out double number)
    {
        // Aceita vírgula ou ponto como separador decimal, sem separador de milhar.
        return double.TryParse(value?.Trim().Replace(',', '.'), NumberStyles.Float,
            CultureInfo.InvariantCulture, out number) && double.IsFinite(number);
    }
    private static string Format(double value) => value.ToString("G10", CultureInfo.GetCultureInfo("pt-BR"));

    private void OnCalculate(object sender, EventArgs e)
    {
        Result.Text = "—";
        if (!TryNumber(X1.Text, out double x1) || !TryNumber(Y1.Text, out double y1) ||
            !TryNumber(X2.Text, out double x2) || !TryNumber(Y2.Text, out double y2))
        { Feedback.Text = "Informe quatro coordenadas numéricas válidas."; return; }
        double distance = MathRules.Distance(x1, y1, x2, y2);
        if (!double.IsFinite(distance)) { Feedback.Text = "Valores fora do intervalo de cálculo."; return; }
        Result.Text = Format(distance);
        Feedback.Text = "Distância linear em unidades do plano cartesiano.";
    }
    private void OnClear(object sender, EventArgs e)
    {
        X1.Text = Y1.Text = X2.Text = Y2.Text = string.Empty;
        Result.Text = "—"; Feedback.Text = "Preencha os quatro campos."; X1.Focus();
    }
}