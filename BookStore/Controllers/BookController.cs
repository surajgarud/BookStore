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
                var book = this.bookBL.DeleteBook(BookId);
                if (!book)
                {
                    return this.BadRequest(new { Success = false, message = "failed to Delete the Book" });
                }
                else
                {
                    return this.Ok(new { Success = true, message = " Book is Deleted successfully ", data = book });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("{BookId}/Get")]
        public IActionResult GetBookByBookId(long BookId)
        {
            try
            {
                var book = this.bookBL.GetBookByBookId(BookId);
                if (book != null)
                {
                    return this.Ok(new { Success = true, message = "Book Detail Fetched Sucessfully", Response = book });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Correct Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
