using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Publisher.Dto {
    public class GetPublisherForEditOutput:  EntityDto{

        public virtual int? CityId { get; set; }


        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Phone { get; set; }

        public Guid? PhotoId { get; set; }
        public string PhotoFileBytes { get; set; }
    }
}
