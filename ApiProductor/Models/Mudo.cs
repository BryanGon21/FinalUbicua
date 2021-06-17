using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProductor.Models
{
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
