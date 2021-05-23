using System.ComponentModel.DataAnnotations;

namespace cs_blogger.Models
{
    public class Comment
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public string CreatorId { get; set; }

        // [Required]
        public Account Creator { get; set; }

        [Required]
        public int BlogId { get; set; }

        // [Required]
        public Blog Blog { get; set; }

        [Required]
        [MaxLength(240)]
        public string Body { get; set; }

    }
}