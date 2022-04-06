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
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }
        [HttpPost("AddBook")]
        public IActionResult AddBook(BookModel book)
        {
            try
            {
                var user = this.bookBL.AddBook(book);
                if (user != null)
                {
                    return this.Ok(new { Success = true, message = "Book Details Added Sucessfully", });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Book Details Insertion Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpPost("UpdateBook")]
        public IActionResult UpdateBook(UpdateBook update)
        {
            try
            {
                var user = this.bookBL.UpdateBook(update);
                if (user != null)
                {
                    return this.Ok(new { Success = true, message = "Book Details Updated", });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Book Update Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(long BookId)
        {
            try
            {
                var notes = this.bookBL.DeleteBook(BookId);
                if (!notes)
                {
                    return this.BadRequest(new { Success = false, message = "failed to Delete the Book" });
                }
                else
                {
                    return this.Ok(new { Success = true, message = " Book is Deleted successfully ", data = notes });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
