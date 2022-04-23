using BasketModuleEntities.GeneralEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketModuleEntities.Models
{
    public class Product : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public int CurrencyID { get; set; }

        [ForeignKey("CurrencyID")]
        public virtual Currency Currency { get; set; }

        public virtual ICollection<BasketProduct> BasketProduct { get; set; }
    }
}
