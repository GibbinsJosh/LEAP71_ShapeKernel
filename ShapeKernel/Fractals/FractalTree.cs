using System;
using System.Numerics;
using PicoGK;

namespace Leap71.ShapeKernel
{
    public class FractalTree : BaseShape
    {
        private readonly int _iterations;
        private readonly float _initialLength;
        private readonly float _branchingAngle;
        private readonly float _branchProbability;
        private readonly int _maxBranchAttempts;
        private readonly float _lengthRandomnessFactor;
        private readonly float _reductionFactor;
        private readonly float _rotationRandomnessFactor;
        private readonly float _divergenceRandomnessFactor;
        private readonly Vector3 _growthDirection;
        private readonly float _maxDivergenceAngle;
        private readonly bool _includeLeaves;
        private readonly bool _structuredTree;

        public FractalTree(
            int iterations,
            float initialLength,
            float branchingAngle,
            float branchProbability,
            int maxBranchAttempts,
            float lengthRandomnessFactor,
            float reductionFactor,
            float rotationRandomnessFactor,
            float divergenceRandomnessFactor,
            Vector3 growthDirection,
            float maxDivergenceAngle,
            bool includeLeaves = true,
            bool structuredTree = true)
        {
            _iterations = iterations;
            _initialLength = initialLength;
            _branchingAngle = branchingAngle;
            _branchProbability = branchProbability;
            _maxBranchAttempts = maxBranchAttempts;
            _lengthRandomnessFactor = lengthRandomnessFactor;
            _reductionFactor = reductionFactor;
            _rotationRandomnessFactor = rotationRandomnessFactor;
            _divergenceRandomnessFactor = divergenceRandomnessFactor;
            _growthDirection = Vector3.Normalize(growthDirection);
            _maxDivergenceAngle = (float)(maxDivergenceAngle * Math.PI / 180); // Angle converted to radians
            _includeLeaves = includeLeaves;
            _structuredTree = structuredTree;
        }

        public override Voxels voxConstruct()
        {
            var lattice = BuildLattice();
            return new Voxels();
        }

        public Lattice BuildLattice()
        {
            var lattice = new Lattice();
            var random = new Random();

            // Start with the base of the tree
            Vector3 baseStart = Vector3.Zero;
            Vector3 baseEnd = _growthDirection * _initialLength;

            // Add the initial branch
            AddBranch(lattice, baseStart, baseEnd, _initialLength/10, 0, random);

            return lattice;
        }

        private void AddBranch(Lattice lattice, Vector3 start, Vector3 end, float radius, int iteration, Random random)
        {
            if (iteration >= _iterations) return;

            // Calculate branch length with randomness
            float growthFactor = MathF.Pow(_reductionFactor, iteration);
            float length = _initialLength * growthFactor * (1f + _lengthRandomnessFactor * (float)(random.NextDouble() - 0.5));

            // Add the current branch to the lattice
            float radius1 = radius;
            float radius2 = radius * growthFactor;
            lattice.AddBeam(start, radius1, end, radius2, true);

            // Base direction for branch spreading
            Vector3 baseDirection = Vector3.Normalize(end - start) * length;

            // Generate new branches
            for (int currentBranch = 0; currentBranch < _maxBranchAttempts; currentBranch++)
            {
                // Check branch generation probability
                if (random.NextDouble() >= _branchProbability) continue;

                // Calculate evenly spaced angle based on branch index
                float spacingAngle = currentBranch * 2f * MathF.PI / _maxBranchAttempts;

                // Generate a rotation axis perpendicular to the base direction
                Vector3 rotationAxis = _structuredTree ? Vector3.UnitY : Vector3.Cross(baseDirection, Vector3.Normalize(new Vector3(
                    (float)random.NextDouble(),
                    (float)random.NextDouble(),
                    (float)random.NextDouble())
                ));
                rotationAxis = Vector3.Normalize(rotationAxis);


                // Apply base branching angle with divergence randomness
                float branchingAngle = _branchingAngle * (1f + _divergenceRandomnessFactor * (float)(random.NextDouble() - 0.5f));
                branchingAngle *= MathF.PI / 180f; // Convert to radians

                // Rotate the base direction by the branching angle
                Quaternion baseRotation = Quaternion.CreateFromAxisAngle(rotationAxis, branchingAngle);
                Vector3 rotatedDirection = Vector3.Transform(baseDirection, baseRotation);

                // Rotate around the node for even spacing
                Quaternion spacingRotation = Quaternion.CreateFromAxisAngle(Vector3.Normalize(baseDirection), spacingAngle);
                Vector3 branchDirection = Vector3.Transform(rotatedDirection, spacingRotation);

                // Add the new branch to the lattice
                Vector3 newStart = end;
                Vector3 newEnd = newStart + branchDirection;
                AddBranch(lattice, newStart, newEnd, radius2, iteration + 1, random);
            }
        }
        private void TryAddLeaves(Lattice lattice, Vector3 position, float radius, Random random)
        {
            if (!_includeLeaves) return;
            lattice.AddSphere(position, radius);
        }


    }
}
