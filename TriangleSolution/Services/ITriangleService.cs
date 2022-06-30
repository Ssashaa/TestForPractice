using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Triangles.Models;

namespace Triangles.Services
{
    public interface ITriangleService
    {
        Task<string> Info(Triangle triangle);

        Task<double> Area(Triangle triangle);

        Task<double> Perimeter(Triangle triangle);

        Task<bool> IsRightAngled(Triangle triangle);

        Task<bool> IsEquilateral(Triangle triangle);

        Task<bool> AreCongruent(Triangle x, Triangle y);

        Task<bool> IsIsosceles(Triangle triangle);

        Task<bool> AreSimilar(Triangle tr1, Triangle tr2);

        Task<string> InfoGreatestArea(Triangle[] triangles);

        Task<string> InfoGreatestPerimeter(Triangle[] triangles);

        Task<string> NumbersPairwiseNotSimilar(Triangle[] triangles);
    }
}
