using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DALException : Exception
    {
        public DALException(String message) : base(message)
        {
            
        }

        public DALException(String message, Exception e): base(message, e)
        { 
           
        }
    }
}
