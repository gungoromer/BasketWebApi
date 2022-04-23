using BasketModuleEntities.GeneralEntities;
using BasketModuleEntities.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketModuleEntities.Models
{
    public class Basket : Base
    {
        /// <summary>
        /// Primarykey of table
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BasketID { get; set; }

        /// <summary>
        /// Create date of basket
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Owner of basket
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Only one record of a user can be active at time t.
        /// </summary>
        public bool IsActive { get; set; }

        public virtual ICollection<BasketProduct> BasketProduct { get; set; }

        [NotMapped]
        public static BasketEntityValidator Validator
        {
            get
            {
                return new BasketEntityValidator();
            }
        }
    }
}
