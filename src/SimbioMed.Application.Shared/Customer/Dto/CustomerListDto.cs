using SimbioMed.City.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Customer.Dto {
  public class CustomerListDto {
        public int Id { get; set; }

        public CityListDto City { get; set; }
        public virtual int? CityId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }


    }
}
