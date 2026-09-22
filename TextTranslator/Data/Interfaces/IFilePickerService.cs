namespace TextTranslator.Data.Interfaces
{
    public interface IFilePickerService
    {
        Task<FileResult?> PickImageAsync();
    }
}
