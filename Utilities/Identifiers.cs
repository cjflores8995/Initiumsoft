using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Identifiers: IHelpers
    {
        public string GenerateIdentifier()
        {
            int longitud = new Random().Next(30, 35);
            Guid miGuid = Guid.NewGuid();
            Guid miGuid2 = Guid.NewGuid();
            string token = Convert.ToBase64String(miGuid.ToByteArray());
            token = token.Replace("=", "").Replace("+", "").Replace("\\", "").Replace("/", "");

            string token2 = Convert.ToBase64String(miGuid2.ToByteArray());
            token2 = token2.Replace("=", "").Replace("+", "").Replace("\\", "").Replace("/", "");

            string token3 = ($"{DateTime.Now.Ticks.ToString()}{token}{token2}");

            return token3.Substring(0, longitud);
        }
    }
}
