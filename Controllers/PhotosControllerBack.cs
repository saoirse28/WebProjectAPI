using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using WebProjectAPI.Data;
using WebProjectAPI.Data.Entities;
using WebProjectAPI.Interface;

namespace WebProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosControllerBack : ControllerBase
    {
        private readonly IPhotos _photos;
        private readonly ILogger<PhotosControllerBack> _logger;
        private readonly IMapper _mapper;
        public PhotosControllerBack(IPhotos photos, 
            ILogger<PhotosControllerBack> logger,
            IMapper mapper)
        {
            _photos = photos;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize(Policy = "AllUsers")]              
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos()
        {
            return Ok(await _photos.GetPhotos());
        }

        [Authorize(Roles = "Administrator,Member")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Photo>> GetPhoto(int id)
        {
            var photo = await _photos.GetPhoto(id);

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoto(int id, Photo photo)
        {
            if (id != photo.Id)
            {
                _logger.LogError("Invalid photo id.");
                return BadRequest();
            }

            if (!await _photos.PutPhoto(id, photo))
            {
                return NotFound();
            }

            return NoContent();
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Photo>> PostPhoto(Photo photo)
        {
            var ret = await _photos.PostPhoto(photo);
            return CreatedAtAction("GetPhoto", new { id = ret.Id }, ret);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            if (!await _photos.DeletePhoto(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
