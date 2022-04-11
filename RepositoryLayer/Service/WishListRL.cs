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
    public class WishListRL :IWishListRL
    {

        private SqlConnection sqlConnection;
        public WishListRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }


        private IConfiguration Configuration { get; }
        public string AddToWishlist(int bookId, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BooKStore"]);
                SqlCommand cmd = new SqlCommand("AddInWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                this.sqlConnection.Open();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                this.sqlConnection.Close();
                if (i == 2)
                {
                    return "Book is Already in Wishlist";
                }

                if (i == 1)
                {
                    return "Choose Correct BookID";
                }
                else
                {
                    return "Book added in Wishlist";
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
        public bool DeleteFromWishlist(int userId, int wishlistId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("DeleteFromWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@WishlistId", wishlistId);
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
        public List<WishListModel> GetAllFromWishlist(int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BooKStore"]);
                SqlCommand cmd = new SqlCommand("GetAllRecordsFromWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserId", userId);
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<WishListModel> wishModel = new List<WishListModel>();
                    while (reader.Read())
                    {
                        BookModel bookModel = new BookModel();
                        WishListModel wish = new WishListModel();
                        bookModel.BookName = reader["bookName"].ToString();
                        bookModel.AuthorName = reader["authorName"].ToString();
                        bookModel.OriginalPrice = Convert.ToInt32(reader["originalPrice"]);
                        bookModel.DiscountedPrice = Convert.ToInt32(reader["discountedPrice"]);
                        bookModel.BookImage = reader["bookImage"].ToString();
                        wish.WishlistId = Convert.ToInt32(reader["WishlistId"]);
                        wish.UserId = Convert.ToInt32(reader["UserId"]);
                        wish.BookId = Convert.ToInt32(reader["BookId"]);
                        wish.Bookmodel = bookModel;
                        wishModel.Add(wish);
                    }

                    this.sqlConnection.Close();
                    return wishModel;
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
