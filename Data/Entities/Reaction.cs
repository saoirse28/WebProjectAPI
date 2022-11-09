#nullable disable
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using WebProjectAPI;

namespace WebProjectAPI.Data.Entities
{
    public class Reaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
