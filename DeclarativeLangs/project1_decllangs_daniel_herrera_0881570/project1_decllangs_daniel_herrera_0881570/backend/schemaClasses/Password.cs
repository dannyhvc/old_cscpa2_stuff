using System.ComponentModel.DataAnnotations;

namespace project1_decllangs_daniel_herrera_0881570.backend.schemaClasses
{
    public class Password
    {
        [Required]
        public string Value { get; set; }
        [Required]
        [Range(0, 100)]
        public int StrengthNum { get; set; }
        [Required]
        [EnumDataType(typeof(StrengthTextEnum))]
        public string StrengthText { get; set; }
        public string LastReset { get; set; }
    }
}
