using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjectAPI.Data;
using WebProjectAPI.Data.Entities;
using WebProjectAPI.Interface;

#nullable disable
namespace WebProjectAPI.Services
{
    public class PhotosService : IPhotos
    {
        private readonly ProjectApiDBContext _context;

        public PhotosService(ProjectApiDBContext context)
        {
            _context=context;
        }

        public async Task<bool> DeletePhoto(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo != null)
            {
                return false;
            }

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            return await _context.Photos.FindAsync(id);
        }

        public async Task<IEnumerable<Photo>> GetPhotos()
        {
            return await _context.Photos.ToListAsync();
        }

        public bool PhotoExists(int id)
        {
            return _context.Photos.Any(e => e.Id == id);
        }

        public async Task<Photo> PostPhoto(Photo photo)
        {
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return photo;
        }

        public async Task<bool> PutPhoto(int id, Photo photo)
        {
            _context.Entry(photo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
                {
                    return false; 
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
    }
}
