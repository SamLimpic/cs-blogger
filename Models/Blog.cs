using System.ComponentModel.DataAnnotations;

namespace cs_blogger.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string CreatorId { get; set; }

        public Account Creator { get; set; }

        [Required]
        public string Title { get; set; }

        public string Body { get; set; } = "No Body";

        public string ImgUrl { get; set; } = "http://www.fillmurray.com/250/250";

        public bool Published { get; set; }

    }
}