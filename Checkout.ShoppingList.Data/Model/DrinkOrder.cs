using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Checkout.ShoppingList.Data.Model
{
    public class DrinkOrder
    {
        [Key]
        [Required]
        public string Name { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Quantity is out of range")]
        public int Quantity { get; set; }


        //Override the equality operators to test for value equality over ref equality.
        public override bool Equals(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            DrinkOrder other = (DrinkOrder)obj;
            if(other.Name == Name && other.Quantity == Quantity)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

    }
}
