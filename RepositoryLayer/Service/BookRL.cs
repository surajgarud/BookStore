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
    public class BookRL : IBookRL
    {
        private SqlConnection sqlConnection;
        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
        public BookModel AddBook(BookModel book)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BooKStore"]);
                SqlCommand com = new SqlCommand("AddBookDetails", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@BookName", book.BookName);
                com.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                com.Parameters.AddWithValue("@Rating", book.Rating);
                com.Parameters.AddWithValue("@TotalView", book.TotalView);
                com.Parameters.AddWithValue("@OriginalPrice", book.OriginalPrice);
                com.Parameters.AddWithValue("@DiscountedPrice", book.DiscountedPrice);
                com.Parameters.AddWithValue("@BookDetails", book.BookDetails);
                com.Parameters.AddWithValue("@BookImage", book.BookImage);

                this.sqlConnection.Open();
                int i = com.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return book;
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
        public UpdateBook UpdateBook(UpdateBook update)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BooKStore"]);
                SqlCommand com = new SqlCommand("UpdateBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@BookId", update.BookId);
                com.Parameters.AddWithValue("@BookName", update.BookName);
                com.Parameters.AddWithValue("@AuthorName", update.AuthorName);
                com.Parameters.AddWithValue("@OriginalPrice", update.OriginalPrice);
                com.Parameters.AddWithValue("@DiscountedPrice", update.DiscountedPrice);
                com.Parameters.AddWithValue("@BookDetails", update.BookDetails);
                com.Parameters.AddWithValue("@BookImage", update.BookImage);

                this.sqlConnection.Open();
                int i = com.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return update;
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
        public bool DeleteBook(long BookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BooKStore"]);
                SqlCommand com = new SqlCommand("DeleteBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@BookId",BookId);
                this.sqlConnection.Open();
                int i = com.ExecuteNonQuery();
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
        public BookModel GetBookByBookId(long BookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BooKStore"]);
                SqlCommand cmd = new SqlCommand("GetBookByBookId", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookId", BookId);
                this.sqlConnection.Open();
                BookModel bookModel = new BookModel();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //bookModel.BookId = Convert.ToInt32(reader["BookId"]);
                        bookModel.BookName = reader["BookName"].ToString();
                        bookModel.AuthorName = reader["AuthorName"].ToString();
                        bookModel.Rating = Convert.ToInt32(reader["Rating"]);
                        bookModel.TotalView = Convert.ToInt32(reader["TotalView"]);
                        bookModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                        bookModel.DiscountedPrice = Convert.ToInt32(reader["DiscountedPrice"]);
                        bookModel.BookDetails = reader["BookDetails"].ToString();
                        bookModel.BookImage = reader["BookImage"].ToString();
                    }

                    this.sqlConnection.Close();
                    return bookModel;
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
        public List<BookModel> GetAllBooks()
        {
            try
            {
                List<BookModel> book = new List<BookModel>();
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:Bookstore"]);
                SqlCommand cmd = new SqlCommand("GetAllBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        book.Add(new BookModel
                        {
                            BookId = Convert.ToInt32(reader["bookId"]),
                            BookName = reader["bookName"].ToString(),
                            AuthorName = reader["authorName"].ToString(),
                            Rating = Convert.ToInt32(reader["rating"]),
                            TotalView = Convert.ToInt32(reader["TotalView"]),
                            DiscountedPrice = Convert.ToInt32(reader["DiscountedPrice"]),
                            OriginalPrice = Convert.ToInt32(reader["originalPrice"]),
                            BookDetails = reader["BookDetails"].ToString(),
                            BookImage = reader["bookImage"].ToString(),
                        });
                    }

                    this.sqlConnection.Close();
                    return book;
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
