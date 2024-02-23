using IronBarCode;
using QRCoder;

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
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(rezervacijaId.ToString(), QRCodeGenerator.ECCLevel.L);
        PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qRCode.GetGraphic(20);
        qrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
    }
    private async void OnReturnButtonClicked(object sender, EventArgs e)
    {
        // Navigate back to Profil page
        await Navigation.PopAsync();
    }
}