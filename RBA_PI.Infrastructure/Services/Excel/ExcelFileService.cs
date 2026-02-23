using Microsoft.AspNetCore.Hosting;
using RBA_PI.Application.Services.Interfaces;

namespace RBA_PI.Infrastructure.Services.Excel
{
    public class ExcelFileService : IExcelFileService
    {
        private readonly string _folder;

        public ExcelFileService(IWebHostEnvironment env)
        {
            _folder = Path.Combine(env.ContentRootPath, "Uploads");
            Directory.CreateDirectory(_folder);
        }

        public void Delete(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        public async Task<string> SaveAsync(Stream stream, string fileName)
        {
            string path = Path.Combine(_folder, $"{Guid.NewGuid()}_{fileName}");
            using FileStream fs = File.Create(path);
            await stream.CopyToAsync(fs);
            return path;
        }
    }
}
