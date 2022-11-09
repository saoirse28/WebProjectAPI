#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using WebProjectAPI;
using WebProjectAPI.Interface;

namespace WebProjectAPI.Data.Entities
{
    [Table("Category")]
    [Index(nameof(Name), IsUnique = true, Name = "idxCategoryName")]
    public class Category
    {
        public Category(string name)
        {
            Name = name;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("CategoryName", TypeName = "varchar(50)")]
        [ConcurrencyCheck, MaxLength(50, ErrorMessage = "Category Name must be 50 characters or less"), MinLength(3, ErrorMessage = "Category Name must be 3 characters or more")]
        public string Name { get; set; }

        [Column("CategoryDescription", TypeName = "varchar(150)")]
        [MaxLength(150, ErrorMessage = "Category Description must be 150 characters or less")]
        public string Description { get; set; }
        
        public List<CategoryPhotos> CategoryPhotos { get; set; }
    }
}
