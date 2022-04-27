using SimbioMed.City.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Author.Dto {
  public  class AuthorListDto {

        public int Id { get; set; }

        public CityListDto City { get; set; }
        public virtual int? CityId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual DateTime DateOfDeath { get; set; }
        public virtual GenderDto Gender { get; set; }
        public Guid? PhotoId { get; set; }
        public string PhotoFileBytes { get; set; }


    }
}
