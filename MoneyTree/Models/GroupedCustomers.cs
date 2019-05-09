using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTree.Models {

    public class GroupedCustomers {

        public string FirstLetter { get; set; }
        public List<Customer> CustomerList { get; set; }
    }
}
