using System.ComponentModel.DataAnnotations;

namespace SimbioMed.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}