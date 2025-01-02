using System;
using System.Collections.Generic;
using System.Numerics;
using PicoGK;

namespace Leap71
{
    namespace ShapeKernel
    {
        public class KochSnowflake3D : BaseShape, ISurfaceBaseShape, IMeshBaseShape
        {
            private int m_nIterations;
            private float m_fInitialSize;

            public KochSnowflake3D(int nIterations, float fInitialSize)
            {
                m_nIterations = nIterations;
                m_fInitialSize = fInitialSize;
            }

            public override Voxels voxConstruct()
            {
                // Placeholder for voxel construction (not implemented in this example).
                throw new NotImplementedException();
            }

            public Mesh mshConstruct()
            {
                List<Vector3> vertices = new List<Vector3>();
                List<int> indices = new List<int>();

                // Generate the initial triangle.
                GenerateInitialTriangle(vertices);

                // Recursively apply Koch's Snowflake rules.
                for (int i = 0; i < m_nIterations; i++)
                {
                    ApplyKochSnowflakeRule(vertices, indices);
                }

                // Construct the mesh.
                Mesh mesh = new Mesh();
                foreach (var vertex in vertices)
                {
                    mesh.AddVertex(m_fnTrafo(vertex));
                }
                foreach (var index in indices)
                {
                    mesh.AddFace(index);
                }

                return mesh;
            }

            public Vector3 vecGetSurfacePoint(float fRatio1, float fRatio2, float fRatio3)
            {
                // Placeholder for surface point generation (not fully implemented).
                return new Vector3(fRatio1, fRatio2, fRatio3);
            }

            private void GenerateInitialTriangle(List<Vector3> vertices)
            {
                float halfSize = m_fInitialSize / 2.0f;

                // Define the initial equilateral triangle in 3D space.
                vertices.Add(new Vector3(-halfSize, 0, -halfSize)); // Vertex 1
                vertices.Add(new Vector3(halfSize, 0, -halfSize));  // Vertex 2
                vertices.Add(new Vector3(0, 0, halfSize));          // Vertex 3
            }

            private void ApplyKochSnowflakeRule(List<Vector3> vertices, List<int> indices)
            {
                List<Vector3> newVertices = new List<Vector3>();
                for (int i = 0; i < vertices.Count; i += 3)
                {
                    Vector3 v1 = vertices[i];
                    Vector3 v2 = vertices[(i + 1) % vertices.Count];
                    Vector3 v3 = vertices[(i + 2) % vertices.Count];

                    // Calculate new points for Koch subdivision.
                    Vector3 mid12 = Vector3.Lerp(v1, v2, 0.5f);
                    Vector3 mid23 = Vector3.Lerp(v2, v3, 0.5f);
                    Vector3 mid31 = Vector3.Lerp(v3, v1, 0.5f);

                    Vector3 peak12 = mid12 + Vector3.Normalize(Vector3.Cross(v2 - v1, mid12 - v1)) * (Vector3.Distance(v1, mid12) * 0.5f);

                    // Add subdivided triangle vertices.
                    newVertices.Add(v1);
                    newVertices.Add(mid12);
                    newVertices.Add(peak12);

                    newVertices.Add(mid12);
                    newVertices.Add(v2);
                    newVertices.Add(mid23);

                    newVertices.Add(peak12);
                    newVertices.Add(mid23);
                    newVertices.Add(v3);

                    newVertices.Add(mid23);
                    newVertices.Add(mid31);
                    newVertices.Add(v1);
                }

                vertices.Clear();
                vertices.AddRange(newVertices);
            }
        }
    }
}
