using System.ComponentModel.DataAnnotations;

namespace cs_blogger.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string CreatorId { get; set; }

        public Account Creator { get; set; }

        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        public string Body { get; set; }

        public string ImgUrl { get; set; }

        public bool Published { get; set; }

    }
}