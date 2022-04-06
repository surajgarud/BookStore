using CommonLayer.Model;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public UserLogin Login(string EmailId, string Password)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand com = new SqlCommand("Login", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@EmailId", EmailId);
                com.Parameters.AddWithValue("@Password", Password);
                this.sqlConnection.Open();
                SqlDataReader rd = com.ExecuteReader();
                if (rd.HasRows)
                {
                    UserLogin user = new UserLogin();
                    while (rd.Read())
                    {
                        user.EmailId = Convert.ToString(rd["EmailId"] == DBNull.Value ? default : rd["EmailId"]);
                        //user.UserId = Convert.ToInt32(rd["UserId"] == DBNull.Value ? default : rd["UserId"]);
                        user.FullName = Convert.ToString(rd["FullName"] == DBNull.Value ? default : rd["FullName"]);
                    }

                    this.sqlConnection.Close();
                    user.Token = this.GenerateJWTToken(user); 
                    return user;
                }
                else
                {
                    this.sqlConnection.Close();
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
        public string GenerateJWTToken(UserLogin user)
        {
            // header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // payload
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "User"),
                new Claim("EmailId", user.EmailId),
                new Claim("Id", user.UserId.ToString()),
            };

            // signature
            var token = new JwtSecurityToken(
                this.Configuration["Jwt:Issuer"],
                this.Configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string ForgotPassword(string EmailId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand com = new SqlCommand("ForgotPassword", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@EmailId", EmailId);
                this.sqlConnection.Open();
                SqlDataReader rd = com.ExecuteReader();
                if (rd.HasRows)
                {
                    int UserId = 0;
                    while (rd.Read())
                    {
                        EmailId = Convert.ToString(rd["EmailId"] == DBNull.Value ? default : rd["EmailId"]);
                        UserId = Convert.ToInt32(rd["UserId"] == DBNull.Value ? default : rd["UserId"]);
                    }

                    this.sqlConnection.Close();
                    var token = this.GenerateJWTTokenForPassword(EmailId, UserId);
                    new MSMQModel().send(token);
                    return token;
                }
                else
                {
                    this.sqlConnection.Close();
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
        public string GenerateJWTTokenForPassword(string EmailId, int UserId)
        {
            // header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // payload
            var claims = new[]
            {
                new Claim("EmailId", EmailId),
                new Claim("UserId", UserId.ToString()),
            };

            // signature
            var token = new JwtSecurityToken(
                this.Configuration["Jwt:Issuer"],
                this.Configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public bool ResetPassword(string EmailId, string NewPassword, string ConfirmPassword)
        {
            try
            {
                if (NewPassword == ConfirmPassword)
                {
                    this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                    SqlCommand com = new SqlCommand("ResetPassword", this.sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    com.Parameters.AddWithValue("@EmailId", EmailId);
                    com.Parameters.AddWithValue("@Password", ConfirmPassword);
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
    }
}

