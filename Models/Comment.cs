using System.ComponentModel.DataAnnotations;

namespace cs_blogger.Models
{
    public class Comment
    {

        public int Id { get; set; }

        public string CreatorId { get; set; }

        public Account Creator { get; set; }

        [Required]
        public int Blog { get; set; }

        [Required]
        [MaxLength(240)]
        public string Body { get; set; }

    }
}