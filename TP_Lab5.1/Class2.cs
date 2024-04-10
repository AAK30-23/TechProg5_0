using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



internal class Class2
{
    public double G(double x, double y)
    {
        try
        {
            return y / Math.Sin(-Math.Pow(x, 2));
        }
        catch
        {
            return double.NaN;
        }
    }
}
