using System.Text.Json;

namespace ConvertisseurDevises;

public partial class MainPage : ContentPage
{
    HttpClient client = new HttpClient();

    public MainPage()
    {
        InitializeComponent();

        deviseSourcePicker.Items.Add("EUR");
        deviseSourcePicker.Items.Add("USD");
        deviseSourcePicker.Items.Add("GBP");
        deviseSourcePicker.Items.Add("TND");
        deviseSourcePicker.Items.Add("JPY");
        deviseSourcePicker.Items.Add("CAD");

        deviseDestinationPicker.Items.Add("EUR");
        deviseDestinationPicker.Items.Add("USD");
        deviseDestinationPicker.Items.Add("GBP");
        deviseDestinationPicker.Items.Add("TND");
        deviseDestinationPicker.Items.Add("JPY");
        deviseDestinationPicker.Items.Add("CAD");

        deviseSourcePicker.SelectedIndex = 0;
        deviseDestinationPicker.SelectedIndex = 1;
    }

    private async void Convertir_Clicked(object sender, EventArgs e)
    {
        if (!double.TryParse(montantEntry.Text, out double montant))
        {
            await DisplayAlert("Erreur", "Montant invalide", "OK");
            return;
        }

        string source =
            deviseSourcePicker.SelectedItem.ToString();

        string destination =
            deviseDestinationPicker.SelectedItem.ToString();

        string url =
        $"https://v6.exchangerate-api.com/v6/5bc4897176867949d1dd2e93/latest/{source}";

        try
        {
            string json = await client.GetStringAsync(url);

            var data =
                JsonSerializer.Deserialize<ExchangeRateResponse>(json);

            double taux =
                data.ConversionRates[destination];

            double resultat = montant * taux;

            resultatLabel.Text =
                $"{montant} {source} = {resultat:F2} {destination}";
        }
        catch
        {
            await DisplayAlert(
                "Erreur",
                "Impossible de récupérer les taux",
                "OK");
        }
    }
}