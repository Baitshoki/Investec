
//using System.ComponentModel.a

using AnyCompany.Models;
using System.Threading.Tasks;

namespace AnyCompany
{
    public class Order : HasId
    {     
        public int CustId { get; set; }
        public double Amount { get; set; }
        public double VAT { get; set; }
        public string OrderNo { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
