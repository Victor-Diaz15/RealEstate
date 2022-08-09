using Microsoft.AspNetCore.Http;

namespace RealEstate.Core.Application.Interfaces.Services
{
    public interface IUploadFileService
    {
        string UploadFile(IFormFile file, string _basePath, bool isEditMode = false, string imgUrl = "");
    }
}
