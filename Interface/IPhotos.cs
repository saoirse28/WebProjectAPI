using Microsoft.AspNetCore.Mvc;
using WebProjectAPI.Data.Entities;

namespace WebProjectAPI.Interface
{
    public interface IPhotos
    {
        public Task<IEnumerable<Photo>> GetPhotos();
        public Task<Photo> GetPhoto(int id);
        public Task<bool> PutPhoto(int id, Photo photo);
        public Task<Photo> PostPhoto(Photo photo);
        public Task<bool> DeletePhoto(int id);
        public bool PhotoExists(int id);

    }
}
