using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    [Serializable]
    class Book
    {
        public int id { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public int price { get; set; }
        public string category { get; set; }
        public string[] tags { get; set; }

    }
}
