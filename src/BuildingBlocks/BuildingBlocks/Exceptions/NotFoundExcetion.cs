using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class NotFoundExcetion:Exception
    {
        public NotFoundExcetion(string message):base(message)
        { 
        
        }
        public NotFoundExcetion(string name, object key):base($"Entity \"{name}\" ({key}) was not found")
        {

        }
    }
}
