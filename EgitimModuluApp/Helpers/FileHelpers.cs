using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace EgitimModuluApp.Helpers
{
    public class FileHelpers
    {
        public static string UploadedFile<T>(T file) where T : IFormFile
        {
            string uniqueFileName = uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", uniqueFileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return uniqueFileName;
        }
    }
}