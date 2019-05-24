using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTree.Models.ViewModels {

    public class CustomerDetailViewModel {

        public int Id { get; set; }

        public Customer Customer { get; set; }

        public List<Project> CustomerCompletedProjects { get; set; }

        public List<Project> CustomerCurrentProjects { get; set; }

        public List<Estimate> CustomerCurrentEstimates { get; set; }

        public List<Estimate> CustomerPastEstimates { get; set; }
    }
}
