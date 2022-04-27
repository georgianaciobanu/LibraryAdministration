using SimbioMed.City.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Publisher.Dto {
    public class PublisherListDto {

        public int Id { get; set; }

        public CityListDto City { get; set; }
        public virtual int? CityId { get; set; }

       
        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Phone { get; set; }

        public Guid? PhotoId { get; set; }
        public string PhotoFileBytes { get; set; }
    }
}
