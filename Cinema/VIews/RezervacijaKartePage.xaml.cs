namespace Cinema.VIews;
using Microsoft.Maui.Controls;
using QRCoder;


public partial class RezervacijaKartePage : ContentPage
{
	public RezervacijaKartePage()
	{
		InitializeComponent();

       
    }

    private void OnGenerateClicked(object sender, EventArgs e)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(selectedSeatLabel.Text, QRCodeGenerator.ECCLevel.L);
        PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qRCode.GetGraphic(20);
        QrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
    }

    private void OnSeatSelected(object sender, EventArgs e)
    {
        // Handle seat selection event
        var selectedSeatButton = (Button)sender;
        string selectedSeat = selectedSeatButton.Text;
        selectedSeatLabel.Text = selectedSeat;
        // You can perform actions such as highlighting the selected seat or storing it for later use
    }

    private void OnConfirmBookingClicked(object sender, EventArgs e)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(selectedSeatLabel.Text, QRCodeGenerator.ECCLevel.L);
        PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qRCode.GetGraphic(20);
        QrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
        DisplayAlert("Booking Confirmed", "Your booking has been confirmed!", "OK");
    }
}