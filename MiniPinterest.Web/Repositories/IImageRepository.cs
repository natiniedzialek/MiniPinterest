﻿namespace MiniPinterest.Web.Repositories
{
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile file); 
    }
}
