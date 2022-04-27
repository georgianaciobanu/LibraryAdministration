using SimbioMed.City.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SimbioMed.Store.Dto {
    public class StoreListDto {

        public int Id { get; set; }

        public virtual string StoreName { get; set; }

        public virtual string Address { get; set; }

        public virtual int? CityId { get; set; }

        public CityListDto City { get; set; }


    }
}
