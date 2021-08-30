using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Services.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public CustomerDto Customer { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }
    }
}
