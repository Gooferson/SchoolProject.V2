using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Shared.Domain
{
    public class Reviews : BaseDomainModel
    {
        public string? RevTittle { get; set; }
        public string? RevContent { get; set; }
        public string? RevRating { get; set; }
        public virtual User? User { get; set; }
        public int? UserID { get; set; }
        public virtual CheckOutTransaction? CheckOutTransaction { get; set; }
        public int? TransactionID { get; set; }
    }
}
