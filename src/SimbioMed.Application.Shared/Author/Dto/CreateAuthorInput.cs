using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimbioMed.Author.Dto {
  public  class CreateAuthorInput {

        [ForeignKey("CityId")]
        public virtual int? CityId { get; set; }
        public const int MaxNameLenght = 200;
        [Required]
        [MaxLength(MaxNameLenght)]
        public virtual string FirstName { get; set; }
        [Required]
        [MaxLength(MaxNameLenght)]
        public virtual string LastName { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual DateTime DateOfDeath { get; set; }
        public virtual GenderDto Gender { get; set; }
        public Guid? PhotoId { get; set; }
        public string PhotoFileBytes { get; set; }


    }
}
