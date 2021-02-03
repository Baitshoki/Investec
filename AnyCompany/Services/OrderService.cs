using System;

namespace AnyCompany
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository;


        public OrderService(OrderRepository orderRepository) {
            this.orderRepository = orderRepository;
        }

        public bool PlaceOrder(Order order, int customerId)
        {
            Customer customer = CustomerRepository.Load(customerId);
            if (customer ==null) {
                throw new CustomerNotFoundException("Customer with Id "+customerId+" Not Found");
            }
            if(order.Amount == 0)
                return false;

            if (customer.Country == "UK")
                order.VAT = 0.2d;
            else
                order.VAT = 0;

            //Create a new order ref, this case a Guid should do even though its not ideal 
            order.OrderNo = Guid.NewGuid().ToString();
            return orderRepository.Save(order); ;
        }
    }
}
