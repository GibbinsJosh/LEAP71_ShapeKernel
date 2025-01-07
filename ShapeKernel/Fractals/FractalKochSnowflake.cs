using System;
using System.Collections.Generic;
using System.Numerics;
using PicoGK;

namespace Leap71.ShapeKernel
{
    public class KochSnowflake
    {
        private readonly int m_nIterations;
        private readonly float m_fSideLength;

        public KochSnowflake(int nIterations = 5, float fSideLength = 1.0f)
        {
            m_nIterations = nIterations;
            m_fSideLength = fSideLength;
        }

        /// <summary>
        /// Generates the Koch Snowflake as a point cloud.
        /// </summary>
        /// <returns>A list of points representing the Koch Snowflake.</returns>
        public List<Vector3> GeneratePointCloud()
        {
            // Start with an equilateral triangle
            List<Vector3> points = new List<Vector3>
            {
                new Vector3(-m_fSideLength / 2f, -MathF.Sqrt(3) * m_fSideLength / 6f, 0),  // Bottom-left vertex
                new Vector3(m_fSideLength / 2f, -MathF.Sqrt(3) * m_fSideLength / 6f, 0),   // Bottom-right vertex
                new Vector3(0.0f, MathF.Sqrt(3) * m_fSideLength / 3f, 0)     // Top vertex
            };

            // Close the triangle by repeating the first vertex
            points.Add(points[0]);

            // Perform recursive subdivision
            for (int i = 0; i < m_nIterations; i++)
            {
                points = Subdivide(points);
            }

            return points;
        }

        /// <summary>
        /// Subdivides the edges of the shape to generate the next iteration of the Koch Snowflake.
        /// </summary>
        private List<Vector3> Subdivide(List<Vector3> points)
        {
            List<Vector3> newPoints = new List<Vector3>();

            for (int i = 0; i < points.Count - 1; i++)
            {
                Vector3 p0 = points[i];
                Vector3 p1 = points[i + 1];

                // Compute the subdivision points
                Vector3 a = Vector3.Lerp(p0, p1, 1.0f / 3.0f);
                Vector3 b = Vector3.Lerp(p0, p1, 2.0f / 3.0f);
                Vector3 midpoint = (p0 + p1) / 2.0f;

                // Compute the peak of the new triangle
                Vector3 direction = Vector3.Normalize(new Vector3(-(p1.Y - p0.Y), p1.X - p0.X, 0));
                float length = Vector3.Distance(p0, p1) / (2.0f * MathF.Sqrt(3.0f));
                Vector3 peak = midpoint + direction * length;

                // Add the points to the new list
                newPoints.Add(p0);
                newPoints.Add(a);
                newPoints.Add(peak);
                newPoints.Add(b);
            }

            // Add the last point
            newPoints.Add(points[^1]);

            return newPoints;
        }
    }
}