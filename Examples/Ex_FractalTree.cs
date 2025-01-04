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
                int iterations = 6; // Number of fractal iterations, be careful with this. The max number of branches goes as branch attempts ^ number of iterations
                float initialLength = 25f; // Initial branch length
                float branchingAngle = 30f; // Angle between branches in degrees
                float maxDivergenceAngle = 35f; // Max divergence from growth direction
                int maxBranchAttempts = 6; // Maximum number of attempts to create a branch
                float reductionFactor = 0.75f; // Reduction factor for branch size from the previous branch
                Vector3 growthDirection = new Vector3(1, 0, 0); // Tree grows upwards
                
                float branchProbability = 0.8f; // Probability of adding a branch
                float lengthRandomnessFactor = 0.25f; // Randomness factor for branch lengths
                float rotationRandomnessFactor = 0.15f; // Randomness factor for branch rotations
                float divergenceRandomnessFactor = 0.2f; // Randomness factor for branch divergence

                bool includeLeaves = false; // Enable or disable leaves

                // Adjust randomness factors to 0 for a structured, repeatable tree
                bool structuredTree = true; // Set to true for a fully structured tree
                if (structuredTree)
                {
                    branchProbability = 1.0f;
                    lengthRandomnessFactor = 0.0f;
                    rotationRandomnessFactor = 0.0f;
                    divergenceRandomnessFactor = 0.0f;
                }

                // Create an instance of the FractalTree class
                var fractalTree = new FractalTree(
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
                    structuredTree
                );

                // Construct the fractal tree lattice
                var lattice = fractalTree.BuildLattice();

                // Show the coordinate frame
                Sh.PreviewFrame(new LocalFrame(), 10.0f);

                // Preview the lattice in ShapeKernel with a specific color
                Sh.PreviewLattice(lattice, structuredTree ? Cp.clrBlueberry : Cp.clrGreen);

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
