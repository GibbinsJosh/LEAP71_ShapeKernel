using System;
using System.Numerics;
using PicoGK;

namespace Leap71
{
    using ShapeKernel;

    namespace ShapeKernelExamples
    {
        class KochSnowflakeExample
        {
            public static void Task()
            {
                // Step 1: Create a KochSnowflake3D instance with 3 iterations and initial size 50
                int iterations = 3;
                float initialSize = 50.0f;
                KochSnowflake3D kochSnowflake = new KochSnowflake3D(iterations, initialSize);

                // Step 2: Construct the fractal mesh
                Mesh fractalMesh = kochSnowflake.mshConstruct();

                // Step 3: Visualize the initial triangle and fractal structure
                Console.WriteLine("Initial Koch Snowflake Triangle:");
                foreach (var vertex in fractalMesh.Vertices)
                {
                    Console.WriteLine($"Vertex: {vertex.X}, {vertex.Y}, {vertex.Z}");
                }

                // Optionally preview the fractal (assuming a visualization library is available)
                Sh.PreviewMesh(fractalMesh, Cp.clrSkyBlue);

                Console.WriteLine("Koch Snowflake Fractal generated and visualized successfully.");
            }
        }
    }
}
