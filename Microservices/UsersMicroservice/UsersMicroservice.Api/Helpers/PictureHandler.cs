
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UsersMicroservice.Api.Exceptions;

namespace UsersMicroservice.Api.Utils
{
    public class PictureHandler
    {
        public static bool SaveImage(IFormFile image, string email)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(),"Images",email.Split('@')[0]+".jpg");
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                try
                {
                    image.CopyTo(stream);
                    return true;
                }
                catch 
                {
                    return false;
                }
            }
        }

        public async static Task<ActionResult<MemoryStream>> GetImage(string email)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Images", email.Split('@')[0] + ".jpg");
            var memory = new MemoryStream();
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
            }
            catch
            {
                throw new NotFoundException("Image doesn't exist");
            }
            
            return memory;
        }
    }
}
