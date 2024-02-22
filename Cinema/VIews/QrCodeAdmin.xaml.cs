namespace Cinema.VIews;

public partial class QrCodeAdmin : ContentPage
{
	public QrCodeAdmin()
	{
		InitializeComponent();
	}
    void OnScanButtonClicked(object sender, EventArgs e)
    {
        OpenCamera();
    }

    public async void OpenCamera()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);
            }
        }
    }
}