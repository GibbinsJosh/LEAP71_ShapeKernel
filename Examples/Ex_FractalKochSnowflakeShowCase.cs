using System;
using System.Numerics;
using PicoGK;

namespace Leap71
{
    using ShapeKernel;

    namespace ShapeKernelExamples
    {
        public class KochSnowflakeShowCase
        {
            public static void Task()
            {
                try
                {
                    // Create the Koch Snowflake generator
                    KochSnowflake snowflake = new KochSnowflake(nIterations: 5, fSideLength: 100.0f);

                    // Generate the point cloud
                    List<Vector3> points = snowflake.GeneratePointCloud();

                    // Visualize the point cloud
                    Sh.PreviewPointCloud(points, 0.1f, Cp.clrFrozen, 0.9f, 0.4f, 0.7f);
                }
                catch (Exception e)
                {
                    Library.Log($"Failed to generate Koch Snowflake: {e.Message}");
                }
            }
        }
    }
}
