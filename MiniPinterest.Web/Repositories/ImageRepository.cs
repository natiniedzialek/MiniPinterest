using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace MiniPinterest.Web.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IConfiguration configuration;
        private readonly Account account;

        public ImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account
            (
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]
            );
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            Cloudinary cloudinary = new Cloudinary(account);

            // Upload
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            if(uploadResult != null && uploadResult.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("here");
                return uploadResult.SecureUrl.ToString();
            }

            return null;
        }
    }
}
