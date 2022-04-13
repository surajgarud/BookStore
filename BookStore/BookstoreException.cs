using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class BookstoreException : Exception
    {
        public enum ExceptionType
        {
            Empty_Parameter
        }
        private readonly ExceptionType type;
        public BookstoreException(ExceptionType Type,string message) : base(message)
        {
            this.type = Type;
        }
    }
}
