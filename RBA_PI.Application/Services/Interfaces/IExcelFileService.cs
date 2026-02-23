namespace RBA_PI.Application.Services.Interfaces
{
    public interface IExcelFileService
    {
        Task<string> SaveAsync(Stream stream, string fileName);
        void Delete(string path);
    }
}
