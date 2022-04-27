using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Book.Dto {
    public class EditBookInput: EntityDto {

        public string Title { get; set; }
        public int AuthorId { get; set; }

    }
}
