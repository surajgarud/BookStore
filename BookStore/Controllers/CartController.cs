using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly ICartBL cartBL;

        
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [HttpPost("Add")]
        public IActionResult AddCart(CartModel cart)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var cartData = this.cartBL.AddCart(cart, UserId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Book Added in Cart ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "cart Add failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
        [HttpPut("Update")]
        public IActionResult UpdateCart(CartModel cart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var cartData = this.cartBL.UpdateCart(cart, userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Book Updated in Cart ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "cart Update failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteCart(int cartId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.cartBL.DeleteCart(cartId))
                {
                    return this.Ok(new { success = true, message = "Book Deleted from Cart " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "cart Delete  failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
        [HttpGet("{UserId}/ GetCartDetails")]
        public IActionResult GetCartDetails()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var cartData = this.cartBL.GetCartByUserId(UserId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Cart Data Fetched Successfully " , response = cartData 
});
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User Id is Wrong" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
    }
}
