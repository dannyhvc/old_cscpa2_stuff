using System.Runtime.Serialization;

namespace project1_decllangs_daniel_herrera_0881570.backend.schemaClasses
{
    public enum StrengthTextEnum
    {
        [EnumMember(Value = "very weak")]
        VeryWeak,
        [EnumMember(Value = "weak")]
        Weak,
        [EnumMember(Value = "good")]
        Good,
        [EnumMember(Value = "strong")]
        Strong,
        [EnumMember(Value = "very strong")]
        VeryStrong
    }
}
