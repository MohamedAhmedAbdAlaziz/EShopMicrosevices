using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class InternalServerExcetion : Exception
    {
        public InternalServerExcetion(string message):base(message)
        {
        }

        public InternalServerExcetion(string message, string details)
        {
            Details = details;
        }
        public string? Details { get; }
    }
}
