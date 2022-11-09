#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace WebProjectAPI.Data.Entities
{
    [PrimaryKey(nameof(ReactionId), nameof(PhotoId), nameof(UserId))]
    public class PhotoReactions
    {
        public int ReactionId { get; set; }
        public Reaction Reactions { get; set; }
        public int PhotoId { get; set; }
        public Photo Photos { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}


