using System;

namespace Triangles.Extentions
{
    public static class DoubleExtentions
    {      
        public static bool IsEqual(this double side1, double side2)
        {
            return Math.Abs(side1 - side2) < side1 * 0.00001;
        }    
    }
}
