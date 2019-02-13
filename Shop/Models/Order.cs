using System.Collections.Generic;

namespace Shop.Models
{
    public class Order
    {
        public static double TotalSum = 0;

        public static List<Item> ItemsInOrder = new List<Item>();
    }
}
