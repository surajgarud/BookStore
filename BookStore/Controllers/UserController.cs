using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult AddUser(UserRegistration Registration)
        {
            try
            {
                var user = this.userBL.Register(Registration);
                if (user != null)
                {
                    return this.Ok(new { Success = true, message = "User Added Sucessfully", });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Use Insertion Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpPost("ForgetPassword")]
        public IActionResult ForgotPassword(string EmailId)
        {
            try
            {
                var forgotPasswordToken = this.userBL.ForgotPassword(EmailId);
                if (forgotPasswordToken != null)
                    return this.Ok(new { Success = true, message = " Token Sent on Mail To Reset The Password", Response = forgotPasswordToken });
                else
                    return this.BadRequest(new { success = false, message = "Mail Sent UnSuccessful" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("login")]
        public IActionResult Login(string EmailId, string Password)
        {
            try
            {
                var result = this.userBL.Login(EmailId, Password);
                if (result != null)
                    return this.Ok(new { success = true, message = "Login Successful", data = result });
                else
                    return this.BadRequest(new { success = false, message = "Login UnSuccessful", data = result });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string NewPassword, string ConfirmPassword)
        {
            try
            {
                var EmailId = User.Claims.FirstOrDefault(e => e.Type == "EmailId").Value.ToString();
                if (this.userBL.ResetPassword(EmailId, NewPassword, ConfirmPassword))
                {
                    return this.Ok(new { Success = true, message = " Password Changed Sucessfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " Password Change Failed ! Try Again " });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

    }
}
