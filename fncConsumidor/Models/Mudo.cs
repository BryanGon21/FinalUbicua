

namespace fncConsumidor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    public class Mudo
    {
        [Key]
        public string NameDevice { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
