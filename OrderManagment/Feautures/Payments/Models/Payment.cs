using OrderManagment.Models;

namespace OrderManagment.Feautures.Payment.Models;
public class Payment : BaseModel
{
    public decimal Amount { get; set; }
}
