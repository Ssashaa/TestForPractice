namespace Triangles.Models
{
    public class TriangleValidation
    {
        private Triangle _triangle;

        public TriangleValidation(Triangle triangle)
        {
            _triangle = triangle;
        }

        public bool IsValid()
        {
            return _triangle.Side1 > 0 && _triangle.Side2 > 0 && _triangle.Side3 > 0;
        }
    }
}
