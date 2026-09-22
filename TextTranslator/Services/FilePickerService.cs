using System.Runtime.InteropServices;

using TextTranslator.Data.Interfaces;

namespace TextTranslator.Services
{
    public class FilePickerService : IFilePickerService
    {
        public async Task<FileResult?> PickImageAsync()
        {
            try
            {
                return await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    /*
                    return await FilePicker.Default.PickAsync(new PickOptions
                    {
                        PickerTitle = "Válassz képfájlt",
                        FileTypes = FilePickerFileType.Images
                    });
                    */
                    var result = await FilePicker.Default.PickAsync(new PickOptions());
                    return result;
                });
            }
            catch (COMException comEx)
            {
                System.Diagnostics.Debug.WriteLine($"FilePicker COM error: 0x{comEx.HResult:X} - {comEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FilePicker error: {ex}");
                return null;
            }
        }
    }
}
