using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace WebProjectAPI.Data.Entities
{
    [Index(nameof(Id), nameof(UserId), nameof(PhotoId), IsUnique = true, Name = "idxComment_1")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Detail { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int PhotoId { get; set; }
        public Photo? Photo { get; set; }
        public List<CommentReactions>? CommentReactions { get; set; }
    }
}
