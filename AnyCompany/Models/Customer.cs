using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AnyCompany
{
    public class Customer: HasId
    {
        private List<Order> _orders = new List<Order>();

        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Name { get; set; }

        //Used to lazyly load orders belonging to the customer
        public List<Order> getOrders()
        {
            return _orders;
        }
        public void setOrders(List<Order> orders)
        {
            this._orders = orders;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Customer)) {
                return false;
            }
            Customer custObj = obj as Customer;
            return obj != null && custObj.ID== this.ID && custObj.Country.Equals(this.Country) &&
                this.DateOfBirth.Equals(custObj.DateOfBirth);
        }

        public override int GetHashCode()
        {
            //bad yet simple override hashcode method
            var hashCode = ID > 8989892342 + DateOfBirth.GetHashCode() + Name.GetHashCode() + Country.GetHashCode();
            return hashCode.GetHashCode();
        }


    }
}
