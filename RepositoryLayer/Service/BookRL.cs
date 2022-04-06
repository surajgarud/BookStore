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
    }
}
