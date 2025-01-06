using PicoGK;
using System.Numerics;

namespace Leap71.ShapeKernelExamples
{
    using ShapeKernel;
    class MandelbulbImplicitShowCase
    {
        public static void Task()
        {
            try
            {
                // Define the implicit function for the Mandelbulb
                MandelbulbImplicit oImplicit = new MandelbulbImplicit(nIterations: 2, fPower: 8.0f, fEscapeRadius: 10.0f);

                // Define bounding box for the Mandelbulb
                BBox3 oBounds = new BBox3(
                    new Vector3(-2.0f, -2.0f, -2.0f), // Minimum corner
                    new Vector3(2.0f, 2.0f, 2.0f)     // Maximum corner
                );

                // Generate the voxel field
                Voxels oVoxels = new Voxels(oImplicit, oBounds);

                // Render and visualize the Mandelbulb
                Sh.PreviewVoxels(oVoxels, Cp.clrPitaya);
            }
            catch (Exception e)
            {
                Library.Log($"Failed to generate Mandelbulb: {e.Message}");
            }
        }
    }
}
