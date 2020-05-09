using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaver.Client.Models
{
    public class TransactionVM
    {
        public int MarketId { get; set; }

        public int CategoryId { get; set; }

        public string AdditionalNote { get; set; }
    }
}
