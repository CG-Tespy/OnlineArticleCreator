using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace CGT.AspNetMvc
{
    public static class SecurityUtils
    {

        public static string Hashed(this string toHash, 
                                    HashAlgorithm hashAlgorithm, 
                                    Encoding encoding)
        {
            byte[] strAsBytes = encoding.GetBytes(toHash);
            byte[] hashedAsBytes = hashAlgorithm.ComputeHash(strAsBytes);
            return encoding.GetString(hashedAsBytes);
        }
    }
}
