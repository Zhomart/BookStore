using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    [Serializable]
    class Basket
    {
        public int quantity { get; set; }
        public List<Book> books { get; set; }
        public int total_price { get; set; }
    }
}
