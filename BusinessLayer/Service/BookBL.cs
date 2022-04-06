using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        public BookModel AddBook(BookModel book)
        {
            try
            {
                return this.bookRL.AddBook(book);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteBook(long BookId)
        {
            try
            {
                return this.bookRL.DeleteBook(BookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BookModel GetBookByBookId(long BookId)
        {
            try
            {
                return this.bookRL.GetBookByBookId(BookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UpdateBook UpdateBook(UpdateBook update)
        {
            try
            {
                return this.bookRL.UpdateBook(update);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
