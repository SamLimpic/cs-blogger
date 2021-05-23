using System.ComponentModel.DataAnnotations;

namespace cs_blogger.Models
{
    public class Blog
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string CreatorId { get; set; }

        // [Required]
        public Account Creator { get; set; }

        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public string ImgUrl { get; set; }

        [Required]
        public bool Published { get; set; }

    }
}