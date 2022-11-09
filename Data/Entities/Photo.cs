using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebProjectAPI.Interface;

#nullable disable
namespace WebProjectAPI.Data.Entities
{
    [Table("Photos")]
    [Index(nameof(Title), IsUnique = false, Name = "idxPhotoTitle")]
    public class Photo
    {
        public Photo(string title, string description)
        {
            Title = title;  
            Description = description; 
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("PhotoTitle", TypeName = "varchar(150)")]
        [ConcurrencyCheck, MaxLength(150, ErrorMessage = "Photo Title must be 150 characters or less"), MinLength(3, ErrorMessage = "Photo Title must be 3 characters or more")]
        public string Title { get; set; }

        [Column("PhotoDescription", TypeName = "varchar(150)")]
        [MaxLength(150, ErrorMessage = "Photo Description must be 150 characters or less")]
        public string Description { get; set; }
        public List<CategoryPhotos> CategoryPhotos { get; set; }
        public List<Comment> Comments { get; set; }
        public List<PhotoReactions> PhotoReactions { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
