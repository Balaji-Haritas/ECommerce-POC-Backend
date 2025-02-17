using CloudinaryDotNet.Actions;

namespace EcommercePOC.RepositoryInterface
{
    public interface IPhotoRepository
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
