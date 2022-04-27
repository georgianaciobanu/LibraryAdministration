using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimbioMed.Book.Dto {
   public class CreateBookInput {

        [Required]
        [MaxLength(BookConsts.MaxGeneralLength)]
        public string Title { get; set; }

        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }

    }
}
