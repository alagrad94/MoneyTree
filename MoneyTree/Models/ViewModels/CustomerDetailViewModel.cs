using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTree.Models.ViewModels {

    public class CustomerDetailViewModel {

        public int Id { get; set; }

        public Customer Customer { get; set; }

        public List<Project> CustomerProjects { get; set; }
    }
}
