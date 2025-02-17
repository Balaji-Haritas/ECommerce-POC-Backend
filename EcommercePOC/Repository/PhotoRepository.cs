using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EcommercePOC.Helpers;
using EcommercePOC.RepositoryInterface;
using Microsoft.Extensions.Options;

namespace EcommercePOC.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly Cloudinary cloudinary;

        public PhotoRepository(IOptions<CloudinarySettings> config)
        {
            var cloudName = config.Value.CloudName;
            var apiKey = config.Value.ApiKey;
            var apiSecret = config.Value.ApiSecret;

            if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
            {
                throw new ArgumentException("Cloudinary settings are not configured correctly.");
            }

            var account = new Account(cloudName, apiKey, apiSecret);
            cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Folder = "da-net8"
                };
                uploadResult = await cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            return await cloudinary.DestroyAsync(deleteParams);
        }
    }
}
