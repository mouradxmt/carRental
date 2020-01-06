using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Models.viewModel
{
    public class HistoryVM
    {
        public Voiture Voiture { get; set; }
        
        public List<ApplicationUser>User { get; set; }
        public List<Location> locations { get; set; }

        public ApplicationUser getUser(int locataireID)
        {
            return this.User.First(a => a.LocataireId == locataireID);
        }
    }
}
