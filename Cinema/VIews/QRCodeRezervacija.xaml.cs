using IronBarCode;

namespace Cinema.VIews;

public partial class QRCodeRezervacija : ContentPage
{
    private int rezervacijaId;
	public QRCodeRezervacija(int rezervacijaId)
	{
		InitializeComponent();
        this.rezervacijaId = rezervacijaId;
	}
    private void OnButtonClicked(object sender, EventArgs e)
    {
        string text = rezervacijaId.ToString();
        var qrCode = QRCodeWriter.CreateQrCode(text);
        var qrCodeBytes = qrCode.ToJpegBinaryData();
        qrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
    }
    private async void OnReturnButtonClicked(object sender, EventArgs e)
    {
        // Navigate back to Profil page
        await Navigation.PopAsync();
    }
}