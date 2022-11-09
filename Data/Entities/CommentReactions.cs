#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjectAPI.Data.Entities
{
    [PrimaryKey(nameof(CommentId), nameof(ReactionId), nameof(UserId))]
    public class CommentReactions
    {
        public int CommentId { get; set; }
        public Comment Comment { get; set; }

        public int ReactionId { get; set; }
        public Reaction Reaction { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}


