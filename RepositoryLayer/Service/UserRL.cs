using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private SqlConnection sqlConnection;
        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
        public UserRegistration Register(UserRegistration user)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BooKStore"]);
                SqlCommand com = new SqlCommand("AddInfo", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@FullName", user.FullName);
                com.Parameters.AddWithValue("@MobileNo", user.MobileNo);
                com.Parameters.AddWithValue("@EmailId", user.EmailId);
                com.Parameters.AddWithValue("@Password", user.Password);
               
                this.sqlConnection.Open();
                int i = com.ExecuteNonQuery();
                //this.sqlConnection.Close();
                if (i >= 1)
                {
                    return user;
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
