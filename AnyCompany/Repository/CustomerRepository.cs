using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        //Why different DBs
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        public static List<Customer> Load()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectCommand = "SELECT c.ID, c.Name, c.DateTime, c.Country," +
                       "o.ID [OrderId], o.Amount, o.VAT , o.OrderNo FROM  Customer c LEFT JOIN Orders o ON c.ID = o.CustId";

                    SqlCommand command = new SqlCommand(selectCommand, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.Country = reader["Country"].ToString();
                        customer.Name = reader["Name"].ToString();
                        customer.ID = Convert.ToInt32(reader["ID"]);
                        customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                        if (!Object.Equals(null, reader["OrderId"]))
                        {
                            Customer dbCustomer = (Customer)customers.Select(cus => cus.ID == customer.ID);
                            if (dbCustomer == null)
                            {
                                //Object pass by reference 
                                dbCustomer = customer;
                            }
                            Order order = new Order();
                            order.ID = Convert.ToInt32(reader["OrderId"]);
                            order.Amount = Convert.ToDouble(reader["Amount"]);
                            order.VAT = Convert.ToDouble(reader["Vat"].ToString());
                            order.OrderNo = reader["OrderNo"].ToString();
                            dbCustomer.getOrders().Add(order);
                        }
                        customers.Add(customer);

                    }
                    return customers;
                }
                catch (Exception ex)
                {
                    //
                    return customers;
                }
                finally
                {
                    //Verify if  connection object is not disposed yet before closing
                    if (connection != null)
                    {
                        connection.Close();
                    }
                }
            }
        }
        public static Customer Load(int custId)
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                Customer customer = new Customer();

                try
                {
                    connection.Open();
                    string selectCommand = "SELECT c.ID, c.Name, c.DateTime, c.Country," +
                       "o.ID [OrderId], o.Amount, o.VAT , o.OrderNo FROM  Customer c where c.ID =@ID ";

                    SqlCommand command = new SqlCommand(selectCommand, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        customer.Country = reader["Country"].ToString();
                        customer.Name = reader["Name"].ToString();
                        customer.ID = Convert.ToInt32(reader["ID"]);
                        customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                        if (!Object.Equals(null, reader["OrderId"]))
                        {
                            Order order = new Order();
                            order.ID = Convert.ToInt32(reader["OrderId"]);
                            order.Amount = Convert.ToDouble(reader["Amount"]);
                            order.VAT = Convert.ToDouble(reader["Vat"].ToString());
                            order.OrderNo = reader["OrderNo"].ToString();
                            customer.getOrders().Add(order);
                        }
                        break;
                    }
                    return customer ;
                }
                catch (Exception ex)
                {
                    //
                    return customer;
                }
                finally
                {
                    //Verify if  connection object is not disposed yet before closing
                    if (connection != null)
                    {
                        connection.Close();
                    }
                }
            }
        }

    }
 }

