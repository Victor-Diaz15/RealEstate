using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Helpers
{
    public class PropertyCodeGenerator
    {
        public string PropertyCodeGen()
        {
            Random randomNumber = new Random();
            int number = randomNumber.Next(1, 10000000);
            string generatedCode = number.ToString("0000000");
            return generatedCode;
        }
    }
}
