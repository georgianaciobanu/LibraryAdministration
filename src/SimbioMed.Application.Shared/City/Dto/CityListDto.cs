using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimbioMed.City.Dto {

    public class CityListDto {

        public int Id { get; set; }

        public virtual string CityName { get; set; }

        public virtual string Country { get; set; }

    }
}
