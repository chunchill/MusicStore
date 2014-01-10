using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Core.Models
{
    public class UserCredit
    {
        public int UserCreditId { get; set; }
        public int UserId { get; set; }
        public decimal DebtAmount { get; set; }
        public DateTime DebtDate { get; set; }
        public string Description { get; set; }
    }
}
