#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjectAPI.Data.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public List<Photo> Photos { get; set; }
        public List<Comment> Comments { get; set; }
        public List<CommentReactions> CommentReactions { get; set; }
        public List<PhotoReactions> PhotoReactions { get; set; }
    }

    public class UserCred
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

