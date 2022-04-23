using BasketModuleEntities.GeneralEntities;
using BasketModuleEntities.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BasketModuleEntities.Models
{
    public class BasketProduct : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BasketProductID { get; set; }

        /// <summary>
        /// Related BasketID
        /// </summary>
        public int BasketID { get; set; }

        /// <summary>
        /// item ID from product table
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// quantity of product
        /// </summary>
        public int Quantity { get; set; }


        [JsonIgnore]
        [ForeignKey("BasketID")]
        public virtual Basket Basket { get; set; }

        [JsonIgnore]
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }


        [NotMapped]
        public static BasketProductEntityValidator Validator
        {
            get
            {
                return new BasketProductEntityValidator();
            }
        }
    }
}
