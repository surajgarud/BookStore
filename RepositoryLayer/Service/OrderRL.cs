using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class OrderRL : IOrderRL
    {
        private SqlConnection sqlConnection;
        public OrderRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
        public OrderModel AddOrder(OrderModel order, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("AddOrders", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookQuantity", order.Quantity);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@BookId", order.BookId);
                cmd.Parameters.AddWithValue("@AddressId", order.AddressId);
                this.sqlConnection.Open();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                this.sqlConnection.Close();
                if (i == 3)
                {
                    return null;
                }

                if (i == 2)
                {
                    return null;
                }
                else
                {
                    return order;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        public List<OrderModel> GetAllOrder(int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("GetOrders", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", userId);
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<OrderModel> orderModels = new List<OrderModel>();
                    while (reader.Read())
                    {
                        OrderModel orderModel = new OrderModel();
                        BookModel bookModel = new BookModel();
                        orderModel.OrderId = Convert.ToInt32(reader["OrdersId"]);
                        orderModel.UserId = Convert.ToInt32(reader["UserId"]);
                        orderModel.BookId = Convert.ToInt32(reader["bookId"]);
                        orderModel.AddressId = Convert.ToInt32(reader["AddressId"]);
                        orderModel.TotalPrice = Convert.ToInt32(reader["TotalPrice"]);
                        orderModel.Quantity = Convert.ToInt32(reader["BookQuantity"]);
                        orderModel.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                        bookModel.BookName = reader["bookName"].ToString();
                        bookModel.AuthorName = reader["authorName"].ToString();
                        //bookModel.Rating = Convert.ToInt32(reader["Rating"]);
                        //bookModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                        //bookModel.DiscountedPrice = Convert.ToInt32(reader["DiscountedPrice"]);
                        bookModel.BookImage = reader["bookImage"].ToString();
                        orderModel.BookModel = bookModel;
                        orderModels.Add(orderModel);
                    }

                    this.sqlConnection.Close();
                    return orderModels;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
}
