using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Triangles.Models;
using Triangles.Extentions;

namespace Triangles.Services
{
    public class TriangleService : ITriangleService
    {
        /// <summary>
        /// This method shows the info about a triangle in a form as follows:
        /// Here [side_1], [side_2], [side_3] are [side1], [side2], [side3] arranged in ascending order.
        /// </summary>
        /// <param name="triangle"> The method takes a triangle </param>
        /// <returns> The method returns information about a triangle </returns>
        public async Task<string> Info(Triangle triangle)
        {
            return await Task.Run(async () =>
            {
                double perimeter = await Perimeter(triangle);

                var sorted = new[] { triangle.Side1, triangle.Side2, triangle.Side3 }.OrderBy(o => o).ToArray();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Triangle: ");
                sb.AppendLine($"({sorted[0]}, {sorted[1]}, {sorted[2]})");
                sb.AppendLine($"({string.Format("{0:F2}", sorted[0] / perimeter) }, {string.Format("{0:F2}", sorted[1] / perimeter) }, {string.Format("{0:F2}", sorted[2] / perimeter) })");
                sb.AppendLine();
                sb.AppendLine($"Area = {string.Format("{0:F2}", await Area(triangle))}");
                sb.Append($"Perimeter = {perimeter}");

                return sb.ToString();
            });
        }

        /// <summary>
        /// This method calculates the area of a triangle
        /// </summary>
        /// <param name="triangle"> The method takes a triangle </param>
        /// <returns> The method returns the area of a triangle </returns>
        public async Task<double> Area(Triangle triangle)
        {
            return await Task.Run(async () =>
            {
                double semiPerimeter = await Perimeter(triangle) / 2;

                return Math.Sqrt(semiPerimeter * (semiPerimeter - triangle.Side1) * (semiPerimeter - triangle.Side2) * (semiPerimeter - triangle.Side3));
            });
        }

        /// <summary>
        /// This method calculates the perimeter of a triangle
        /// </summary>
        /// <param name="triangle"> The method takes a triangle </param>
        /// <returns> The method returns the perimeter of a triangle </returns>
        public async Task<double> Perimeter(Triangle triangle)
        {
            return await Task.Run(() => triangle.Side1 + triangle.Side2 + triangle.Side3);
        }

        /// <summary>
        /// This method returns True or False depending on the triangle is right-angled or not
        /// </summary>
        /// <param name="triangle"> The method takes a triangle  </param>
        /// <returns> The method returns True or False </returns>
        public async Task<bool> IsRightAngled(Triangle triangle)
        {
            return await Task.Run(() =>
            {
                return IsRightAngledOfSide(triangle.Side1, triangle.Side2, triangle.Side3)
                || IsRightAngledOfSide(triangle.Side2, triangle.Side3, triangle.Side1)
                || IsRightAngledOfSide(triangle.Side3, triangle.Side1, triangle.Side2);
            });
        }

        /// <summary>
        /// This method сompares the square of the hypotenuse and the sum of squares of the two sides of a triangle
        /// </summary>
        /// <param name="side1"> The method takes the first side of the triangle </param>
        /// <param name="side2"> The method takes the second side of the triangle </param>
        /// <param name="hypotenuse"> The method takes the hypotenuse of a triangle </param>
        /// <returns> The method returns True or False </returns>
        private bool IsRightAngledOfSide(double side1, double side2, double hypotenuse)
        {
            return Math.Pow(hypotenuse, 2).IsEqual(Math.Pow(side1, 2) + Math.Pow(side2, 2));
        }

        /// <summary>
        /// This method returns True or False depending on the triangle is equilateral or not
        /// </summary>
        /// <param name="triangle"> The method takes a triangle </param>
        /// <returns> The method returns True or False  </returns>
        public async Task<bool> IsEquilateral(Triangle triangle) =>
            await Task.Run(() => triangle.Side1.IsEqual(triangle.Side2) && triangle.Side1.IsEqual(triangle.Side3));

        /// <summary>
        /// This method returns True or False depending on two triangles are congruent or not
        /// </summary>
        /// <param name="x"> The method takes the first triangle </param>
        /// <param name="y"> The method takes a second triangle </param>
        /// <returns> The method returns True or False </returns>
        public async Task<bool> AreCongruent(Triangle x, Triangle y)
        {
            return await Task.Run(async () =>
           {
               var a1 = await Perimeter(x);
               var a2 = await Perimeter(y);

               return a1.IsEqual(a2);
           });
        }

        /// <summary>
        /// This method returns True or False depending on the triangle is isosceles or not
        /// </summary>
        /// <param name="triangle"> The method takes a triangle </param>
        /// <returns> The method returns True or False </returns>
        public async Task<bool> IsIsosceles(Triangle triangle) =>
            await Task.Run(() =>
                triangle.Side1.IsEqual(triangle.Side2)
                 || triangle.Side1.IsEqual(triangle.Side3)
                 || triangle.Side2.IsEqual(triangle.Side3));

        /// <summary>
        /// This method returns True or False depending on two triangles are similar or not
        /// </summary>
        /// <param name="tr1"> The method takes the first triangle </param>
        /// <param name="tr2"> The method takes a second triangle </param>
        /// <returns> The method returns True or False </returns>
        public async Task<bool> AreSimilar(Triangle tr1, Triangle tr2)
        {
            return await Task.Run(() =>
            {
                return
                ((tr1.Side1 / tr2.Side1).IsEqual(tr1.Side2 / tr2.Side2) && (tr1.Side1 / tr2.Side1).IsEqual(tr1.Side3 / tr2.Side3))
                || ((tr1.Side1 / tr2.Side2).IsEqual(tr1.Side2 / tr2.Side3) && (tr1.Side1 / tr2.Side2).IsEqual(tr1.Side3 / tr2.Side1))
                || ((tr1.Side1 / tr2.Side3).IsEqual(tr1.Side2 / tr2.Side1) && (tr1.Side1 / tr2.Side3).IsEqual(tr1.Side3 / tr2.Side2));
            });

        }

        /// <summary>
        /// This method returns the info about the triangle with greatest area 
        /// </summary>
        /// <param name="triangles"> The method takes an array of triangles </param>
        /// <returns> The method returns information about the triangle with the greatest area </returns>
        public async Task<string> InfoGreatestArea(Triangle[] triangles)
        {
            return await Task.Run(() =>
            {
                Triangle triangle = triangles.Aggregate((x, y) => this.Area(x).Result > this.Area(y).Result ? x : y);

                return Info(triangle);
            });
        }

        /// <summary>
        /// This method returns the info about the triangle with greatest perimeter 
        /// </summary>
        /// <param name="triangles"> The method takes an array of triangles </param>
        /// <returns> The method returns information about the triangle with the greatest perimeter </returns>
        public async Task<string> InfoGreatestPerimeter(Triangle[] triangles)
        {
            return await Task.Run(() =>
            {
                Triangle triangle = triangles.Aggregate((x, y) => this.Perimeter(x).Result > this.Perimeter(y).Result ? x : y);

                return Info(triangle);
            });
        }

        /// <summary>
        /// This method returns the info about the triangles chosen from the given array which are pairwise non-similar
        /// </summary>
        /// <param name="triangles"> The method takes an array of triangles </param>
        /// <returns> The method returns the info about the triangles chosen from the given array which are pairwise non-similar </returns>
        public async Task<string> NumbersPairwiseNotSimilar(Triangle[] triangles)
        {
            return await Task.Run(() =>
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < triangles.Length; i++)
                    for (int j = i + 1; j < triangles.Length; j++)
                        if (!AreSimilar(triangles[i], triangles[j]).Result)
                            sb.AppendLine($"({(i + 1)}, {(j + 1)})");

                return sb.ToString().TrimEnd(new char[] { '\r', '\n' });
            });
        }
    }
}