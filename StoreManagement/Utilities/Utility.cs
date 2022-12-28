using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Utilities
{
    public class Utility<T>
    {
        public static bool IsNull(T obj)
        {
            if (obj == null)
                return true;
            return false;
        }

       

    }
}
