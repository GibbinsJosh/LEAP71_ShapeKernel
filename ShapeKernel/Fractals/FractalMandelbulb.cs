using System;
using System.Numerics;
using PicoGK;

namespace Leap71.ShapeKernel
{
    public class MandelbulbImplicit : IImplicit
    {
        private readonly int m_nIterations;
        private readonly float m_fPower;
        private readonly float m_fEscapeRadius;

        public MandelbulbImplicit(int nIterations = 8, float fPower = 8.0f, float fEscapeRadius = 2.0f)
        {
            m_nIterations = nIterations;
            m_fPower = fPower;
            m_fEscapeRadius = fEscapeRadius;
        }

        /// <summary>
        /// Computes the signed distance to the Mandelbulb fractal.
        /// </summary>
        public float fSignedDistance(in Vector3 point)
        {
            Vector3 z = point;
            float dr = 1.0f; // Derivative radius
            float r = 0.0f;

            for (int i = 0; i < m_nIterations; i++)
            {
                r = z.Length();
                if (r > m_fEscapeRadius || r == 0)
                {
                    return 0;
                }

                // Convert Cartesian to spherical coordinates
                float theta = MathF.Acos(z.Z / r);
                float phi = MathF.Atan2(z.Y, z.X);

                // Scale derivative radius
                dr = MathF.Pow(r, m_fPower - 1.0f) * m_fPower * dr + 1.0f;

                // Perform power and rotation
                float zr = MathF.Pow(r, m_fPower);
                theta *= m_fPower;
                phi *= m_fPower;

                // Convert back to Cartesian coordinates
                z = zr * new Vector3(
                    MathF.Sin(theta) * MathF.Cos(phi),
                    MathF.Sin(theta) * MathF.Sin(phi),
                    MathF.Cos(theta)
                );

                // Add the original point
                z += point;
            }

            return 0.5f * MathF.Log(r) * r / dr;
        }

        public float fGetValue(Vector3 point)
        {
            return fSignedDistance(point);
        }
    }
}