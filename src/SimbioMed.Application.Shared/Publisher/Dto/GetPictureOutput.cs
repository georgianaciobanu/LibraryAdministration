using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Publisher.Dto {
    public class GetPictureOutput {

        public string Picture { get; set; }

        public GetPictureOutput(string picture) {
            Picture = picture;
        }
    }
}
