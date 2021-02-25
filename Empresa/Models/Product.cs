using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Empresa.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Producto { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }
        public string URL_Imagen { get; set; }
    }
}
