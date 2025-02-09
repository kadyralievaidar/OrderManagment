using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagment.Models;

namespace OrderManagment.Feautures.Payment.Models;
public class Payment : BaseModel
{
    public decimal Amount { get; set; }
}
