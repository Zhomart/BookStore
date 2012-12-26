using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    [Serializable]
    class Store
    {
        public List<Book> books { get; set; }

        public List<string> categories { get {
            var cs= new List<string>();

            foreach (var b in books)
                if (!cs.Contains(b.category))
                    cs.Add(b.category);

            return cs;
        } }

    }
}
