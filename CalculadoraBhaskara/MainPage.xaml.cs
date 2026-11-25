using System.Globalization;
namespace CalculadoraBhaskara;
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

    private void ResetResult()
    { Delta.Text = "Δ = —"; Root1.Text = "X₁ = —"; Root2.Text = "X₂ = —"; Root1.IsVisible = Root2.IsVisible = true; }
    private void OnCalculate(object sender, EventArgs e)
    {
        ResetResult();
        if (!TryNumber(A.Text, out double a) || !TryNumber(B.Text, out double b) || !TryNumber(C.Text, out double c))
        { Feedback.Text = "Informe três coeficientes numéricos válidos."; return; }
        if (a == 0) { Feedback.Text = "O coeficiente A deve ser diferente de zero."; return; }
        double delta = MathRules.Delta(a, b, c);
        if (!double.IsFinite(delta)) { Feedback.Text = "Valores fora do intervalo de cálculo."; return; }
        Delta.Text = $"Δ = {Format(delta)}";
        if (delta < 0)
        { Root1.IsVisible = false; Root2.IsVisible = false; Feedback.Text = "A equação não possui raízes reais"; return; }
        Root1.IsVisible = true;
        var (x1, x2) = MathRules.Roots(a, b, delta);
        if (!double.IsFinite(x1) || !double.IsFinite(x2)) { Feedback.Text = "Raízes fora do intervalo de cálculo."; return; }
        Root1.Text = $"X₁ = {Format(x1)}";
        if (delta == 0) { Root2.IsVisible = false; Feedback.Text = "A equação possui uma raiz real dupla."; }
        else { Root2.Text = $"X₂ = {Format(x2)}"; Feedback.Text = "A equação possui duas raízes reais."; }
    }
    private void OnClear(object sender, EventArgs e)
    { A.Text = B.Text = C.Text = string.Empty; ResetResult(); Feedback.Text = "Preencha os três coeficientes."; A.Focus(); }
}