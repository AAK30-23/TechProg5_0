using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Lab5._1
{
    internal class Class2
    {
        public double G(double x, double y)
        {
            try
            {
                return Math.Sqrt((x * x * x - 8) / y);
            }
            catch
            {
                return double.NaN;
            }
        }
    }
}