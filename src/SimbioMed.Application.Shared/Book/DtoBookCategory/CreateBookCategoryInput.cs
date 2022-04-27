using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimbioMed.Book.DtoBookCategory {
    public class CreateBookCategoryInput {

        [ForeignKey("BookId")]
        public int BookId { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
    }
}
