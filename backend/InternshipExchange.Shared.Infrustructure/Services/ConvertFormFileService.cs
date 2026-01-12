using Microsoft.AspNetCore.Http;

namespace Modules.Common.Infrastructure.Services
{
    public class ConvertFormFileService
    {
        public static async Task<byte[]> ConvertToByteArrAsync(IFormFile file)
        {
            if (file != null)
            {
                byte[] bytes = Array.Empty<byte>();
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                bytes = ms.ToArray();

                return bytes;
            }
            else
            {
                throw new ArgumentNullException(nameof(file), "File cannot be null");
            }
        }
    }
}
