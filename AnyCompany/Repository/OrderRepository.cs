using System;
using System.Data.SqlClient;

namespace AnyCompany
{
    public class OrderRepository
    {
        //Why different DB to Customer
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public bool Save(Order order)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);                
            try
            {                
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Orders(CustId, Amount, VAT,OrderNo) VALUES (@CustId, @Amount, @VAT,@OrderNo)" , connection);               
                
                command.Parameters.AddWithValue("@CustId", order.CustId);
                command.Parameters.AddWithValue("@Amount", order.Amount);
                command.Parameters.AddWithValue("@VAT", order.VAT);
                command.Parameters.AddWithValue("@OrderNo", order.OrderNo);

                return command.ExecuteNonQuery() > 0 ? true : false;
                 
            }
            catch (Exception ex)
            {
                //We can handle the exception according to business requirements.
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
