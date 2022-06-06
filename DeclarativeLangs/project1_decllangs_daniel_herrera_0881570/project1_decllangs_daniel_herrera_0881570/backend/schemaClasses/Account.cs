using System.ComponentModel.DataAnnotations;

namespace project1_decllangs_daniel_herrera_0881570.backend.schemaClasses
{
    public class Account
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string UserId { get; set; }
        [Url]
        public string LoginUrl { get; set; }
        public string AccountNum { get; set; }
        [Required]
        public Password Password { get; set; }
    }
}
