using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PcBuild
    {
        public PcBuild() 
        { 
            BuildProducts = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string CustomerId { get; set; } = null!;
        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }
        public bool PreBuild { get; set; }
        public ICollection<Product> BuildProducts { get; set; }
    }
}
