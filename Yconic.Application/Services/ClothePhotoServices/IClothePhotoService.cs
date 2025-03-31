using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.ClothePhotoDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.ClothePhotoServices
{
    public interface IClothePhotoService
    {
        Task<ApiResult<List<ClothePhotoDto>>> GetAllClothePhotos();
        Task<ApiResult<ClothePhotoDto>> GetClothePhotoById(Guid id);
        Task<ApiResult<ClothePhotoDto>> CreateClothePhoto(ClothePhoto clothePhoto);
        Task<ApiResult<List<ClothePhotoDto>>> AddClothePhotos(Guid id,AddClothePhotosDto clothePhotos);

        Task<ApiResult<ClothePhotoDto>> UpdateClothePhoto(ClothePhoto clothePhoto);
        Task DeleteClothePhoto(Guid id);
    }
}
