﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;

namespace Yconic.Application.Services.ClothePhotoServices
{
    public interface IClothePhotoService
    {
        Task<List<ClothePhoto>> GetAllClothePhotos();
        Task<ClothePhoto> GetClothePhotoById(Guid id);
        Task<ClothePhoto> CreateClothePhoto(ClothePhoto clothePhoto);
        Task<ClothePhoto> UpdateClothePhoto(ClothePhoto clothePhoto);
        Task DeleteClothePhoto(Guid id);
    }
}
