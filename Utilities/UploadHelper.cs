
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace first_rest_api.Utilities {  
  public static class IOHelper
    {
        public static async Task<bool> FileCopy(string fullPath, IFormFile file)
        {
            using (var filestream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(filestream);
                filestream.Flush();
                filestream.Dispose();
                return true;
            }
        }
    }
}