
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;

namespace TeachBook.Web.Repositories
{
    public class UploadImg : IUploadImg
    {
        private readonly IConfiguration configuration;
        private Account account;
        public UploadImg(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(configuration.GetSection("Cloudinary")["CloudName"], configuration.GetSection("Cloudinary")["ApiKey"], configuration.GetSection("Cloudinary")["ApiSecret"]);
        }
        public async Task<string> UploadImgAsync(IFormFile file)
        {
            var cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,
                DisplayName = file.FileName
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            if (uploadResult != null && uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }
            throw new NotImplementedException();
        }
    }
}
