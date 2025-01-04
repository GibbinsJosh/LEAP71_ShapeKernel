//
// SPDX-License-Identifier: CC0-1.0
//
// This example code file is released to the public under Creative Commons CC0.
// See https://creativecommons.org/publicdomain/zero/1.0/legalcode
//
// To the extent possible under law, LEAP 71 has waived all copyright and
// related or neighboring rights to this LEAP 71 ShapeKernel Example Code.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS
// OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.Numerics;
using Leap71.ShapeKernel;
using PicoGK;

namespace Leap71.ShapeKernelExamples
{
    class FractalTreeExample
    {
        public static void Task()
        {
            try
            {
                // Define parameters for the fractal tree
                int iterations = 5; // Number of fractal iterations, be careful with this. The max number of branches goes as branch attempts ^ number of iterations
                float initialLength = 25f; // Initial branch length
                float branchingAngle = 30f; // Angle between branches in degrees
                float maxDivergenceAngle = 45f; // Max divergence from growth direction
                int maxBranchAttempts = 6; // Maximum number of attempts to create a branch
                float reductionFactor = 0.75f; // Reduction factor for branch size from the previous branch
                Vector3 growthDirection = new Vector3(1, 0, 0); // Tree grows upwards
                
                float branchProbability = 0.75f; // Probability of adding a branch
                float lengthRandomnessFactor = 0.25f; // Randomness factor for branch lengths
                float rotationRandomnessFactor = 0.5f; // Randomness factor for branch rotations
                float divergenceRandomnessFactor = 0.2f; // Randomness factor for branch divergence

                bool includeLeaves = false; // Enable or disable leaves

                // Create an instance of the FractalTree class
                var fractalTreeStructured = new FractalTree(
                    iterations,
                    initialLength,
                    branchingAngle,
                    branchProbability,
                    maxBranchAttempts,
                    lengthRandomnessFactor,
                    reductionFactor,
                    rotationRandomnessFactor,
                    divergenceRandomnessFactor,
                    growthDirection,
                    maxDivergenceAngle,
                    includeLeaves,
                    true
                );

                var fractalTreeUnstructured = new FractalTree(
                    iterations,
                    initialLength,
                    branchingAngle,
                    branchProbability,
                    maxBranchAttempts,
                    lengthRandomnessFactor,
                    reductionFactor,
                    rotationRandomnessFactor,
                    divergenceRandomnessFactor,
                    -growthDirection,
                    maxDivergenceAngle,
                    includeLeaves,
                    false
                );

                // Construct the fractal tree lattices
                var latticeStructured = fractalTreeStructured.BuildLattice();
                var latticeUnstructured = fractalTreeUnstructured.BuildLattice();

                // Preview the lattices in ShapeKernel with specific colors
                Sh.PreviewLattice(latticeStructured, Cp.clrBlueberry);
                Sh.PreviewLattice(latticeUnstructured, Cp.clrGreen);

                // Log success message
                Library.Log("Fractal tree example completed successfully.");
            }
            catch (Exception e)
            {
                // Log any errors encountered during execution
                Library.Log($"Failed to generate fractal tree: \n{e.Message}");
            }
        }
    }
}
