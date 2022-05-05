using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
namespace SimbioMed.BookUnit.Dto {
        public class GetPictureOutput {
            public string Picture { get; set; }

            public GetPictureOutput(string picture) {
                Picture = picture;
            }
        }
    
}
