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
    public class CartRL : ICartRL
    {
        
        private SqlConnection sqlConnection;
        public CartRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

     
        private IConfiguration Configuration { get; }

       
        public CartModel AddCart(CartModel cart, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BooKStore"]);
                SqlCommand cmd = new SqlCommand("AddToCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
                cmd.Parameters.AddWithValue("@Id", cart.Id);
                cmd.Parameters.AddWithValue("@userId", cart.userId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return cart;
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
        public CartModel UpdateCart(CartModel cart, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BooKStore"]);
                SqlCommand cmd = new SqlCommand("UpdateCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
                cmd.Parameters.AddWithValue("@CartId", cart.CartId);
                cmd.Parameters.AddWithValue("@userId", cart.userId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return cart;
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
        public bool DeleteCart(int cartId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BooKStore"]);
                SqlCommand cmd = new SqlCommand("DeleteCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CartId", cartId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
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
        public List<Cart> GetCartByUserId(int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BooKStore"]);
                SqlCommand cmd = new SqlCommand("GetCartbyUser", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", userId);
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<Cart> cart = new List<Cart>();
                    while (reader.Read())
                    {
                        BookModel booksModel = new BookModel();
                        Cart cartmodel = new Cart();

                        booksModel.BookName = reader["bookName"].ToString();
                        booksModel.AuthorName = reader["authorName"].ToString();
                        booksModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                        booksModel.DiscountedPrice = Convert.ToInt32(reader["DiscountedPrice"]);
                        booksModel.BookImage = reader["bookImage"].ToString();
                        cartmodel.Id = Convert.ToInt32(reader["Id"]);
                        cartmodel.CartId = Convert.ToInt32(reader["CartId"]);
                        cartmodel.Quantity = Convert.ToInt32(reader["Quantity"]);
                        cartmodel.Bookmodel = booksModel;
                        cart.Add(cartmodel);
                    }

                    this.sqlConnection.Close(); 
                    return cart;
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
