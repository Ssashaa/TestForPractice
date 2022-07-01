using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Triangles.Models;
using Triangles.Services;
using System.Net;
using System.Linq;

namespace Triangles.Controllers
{
    public class TriangleController : Controller
    {
        private ITriangleService _triangleService;

        public TriangleController(ITriangleService triangleService)
        {
            _triangleService = triangleService;
        }

        /// <summary>
        /// The method checks if the given triangle exists and invokes the Info method
        /// </summary>
        /// <param name="triangle"> The method takes a triangle </param>
        /// <returns> The method returns information about a triangle </returns>
        [HttpGet]
        public string Info([FromQuery] Triangle triangle)
        {
            if (!new TriangleValidation(triangle).IsValid())
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return "Triangle has incorrect a side(s).";
            }

            return Task.Run(() => _triangleService.Info(triangle)).Result;
        }

        /// <summary>
        /// The method checks if the given triangle exists and invokes the Area method
        /// </summary>
        /// <param name="triangle"> The method takes a triangle </param>
        /// <returns> The method returns the area of a triangle </returns>
        [HttpGet]
        public string Area([FromQuery] Triangle triangle)
        {
            if (!new TriangleValidation(triangle).IsValid())
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return "Triangle has incorrect a side(s).";
            }

            double res = Task.Run(() => _triangleService.Area(triangle)).Result;
            return string.Format("{0:F4}", res);
        }

        /// <summary>
        /// The method checks if the given triangle exists and invokes the Perimeter method
        /// </summary>
        /// <param name="triangle"> The method takes a triangle </param>
        /// <returns> The method returns the perimeter of a triangle </returns>
        [HttpGet]
        public string Perimeter([FromQuery] Triangle triangle)
        {
            if (!new TriangleValidation(triangle).IsValid())
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return "Triangle has incorrect a side(s).";
            }

            return Task.Run(() => _triangleService.Perimeter(triangle)).Result.ToString();
        }

        /// <summary>
        /// The method checks if the given triangle exists and invokes the IsRightAngled method
        /// </summary>
        /// <param name="triangle"> The method takes a triangle </param>
        /// <returns> The method returns True or False </returns>
        [HttpGet]
        public bool IsRightAngled([FromQuery] Triangle triangle)
        {
            if (!new TriangleValidation(triangle).IsValid())
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return false;
            }

            return Task.Run(() => _triangleService.IsRightAngled(triangle)).Result;
        }

        /// <summary>
        /// The method checks if the given triangle exists and invokes the IsEquilateral method
        /// </summary>
        /// <param name="triangle"> The method takes a triangle </param>
        /// <returns> The method returns True or False </returns>
        [HttpGet]
        public bool IsEquilateral([FromQuery] Triangle triangle)
        {
            if (!new TriangleValidation(triangle).IsValid())
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return false;
            }

            return Task.Run(() => _triangleService.IsEquilateral(triangle)).Result;
        }

        /// <summary>
        /// The method checks if the given triangles exists and invokes the AreCongruent method
        /// </summary>
        /// <param name="tr1"> The method takes the first triangle </param>
        /// <param name="tr2"> The method takes a second triangle </param>
        /// <returns> The method returns True or False </returns>
        [HttpGet]
        public bool AreCongruent([FromQuery] Triangle tr1, Triangle tr2)
        {
            if (!new TriangleValidation(tr1).IsValid() || !new TriangleValidation(tr2).IsValid())
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return false;
            }

            return Task.Run(() => _triangleService.AreCongruent(tr1, tr2)).Result;
        }

        /// <summary>
        /// The method checks if the given triangle exists and invokes the IsIsosceles method
        /// </summary>
        /// <param name="triangle"> The method takes a triangle </param>
        /// <returns> The method returns True or False </returns>
        [HttpGet]
        public bool IsIsosceles([FromQuery] Triangle triangle)
        {
            if (!new TriangleValidation(triangle).IsValid())
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return false;
            }

            return Task.Run(() => _triangleService.IsIsosceles(triangle)).Result;
        }

        /// <summary>
        /// The method checks if the given triangles exists and invokes the AreSimilar method
        /// </summary>
        /// <param name="tr1"> The method takes the first triangle </param>
        /// <param name="tr2"> The method takes a second triangle </param>
        /// <returns> The method returns True or False </returns>
        public bool AreSimilar([FromQuery] Triangle tr1, Triangle tr2)
        {
            if (!new TriangleValidation(tr1).IsValid() || !new TriangleValidation(tr2).IsValid())
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return false;
            }

            return Task.Run(() => _triangleService.AreSimilar(tr1, tr2)).Result;
        }

        /// <summary>
        /// The method checks if the given triangles exists and invokes the InfoGreatestPerimeter method
        /// </summary>
        /// <param name="tr"> The method takes an array of triangles </param>
        /// <returns> The method returns information about the triangle with the greatest perimeter </returns>
        [ActionName("GreatesByPerimeter")]
        public string InfoGreatestPerimeter([FromQuery] Triangle[] tr)
        {
            if (tr.Any(t => !new TriangleValidation(t).IsValid()))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return "Triangle has incorrect a side(s).";
            }

            return Task.Run(() => _triangleService.InfoGreatestPerimeter(tr)).Result;
        }

        /// <summary>
        /// The method checks if the given triangles exists and invokes the NumbersPairwiseNotSimilar method
        /// </summary>
        /// <param name="tr"> The method takes an array of triangles </param>
        /// <returns> The method returns the info about the triangles chosen from the given array which are pairwise non-similar </returns>
        [ActionName("PairwiseNonSimilar")]
        public string NumbersPairwiseNotSimilar([FromQuery] Triangle[] tr)
        {
            if (tr.Any(t => !new TriangleValidation(t).IsValid()))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return "Triangle has incorrect a side(s).";
            }

            return Task.Run(() => _triangleService.NumbersPairwiseNotSimilar(tr)).Result;
        }
    }
}
