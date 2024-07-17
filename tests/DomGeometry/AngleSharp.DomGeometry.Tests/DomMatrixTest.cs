using AngleSharp.DomGeometry.Dom;

namespace AngleSharp.DomGeometry.Tests
{
    public class DomMatrixTest
    {

        private static bool MatricesEqual(IDomMatrixReadOnly a, IDomMatrixReadOnly b)
        {
            return (a.M11 == b.M11) && (a.M21 == b.M21) && (a.M31 == b.M31) && (a.M41 == b.M41) &&
                (a.M12 == b.M12) && (a.M22 == b.M22) && (a.M32 == b.M32) && (a.M42 == b.M42) &&
                (a.M13 == b.M13) && (a.M23 == b.M23) && (a.M33 == b.M33) && (a.M43 == b.M43) &&
                (a.M14 == b.M14) && (a.M24 == b.M24) && (a.M34 == b.M34) && (a.M44 == b.M44);
        }

        [Test]
        public void TransformStringTest()
        {
            var matrix = new DomMatrix("matrix(1, 0, 0, 2, 5, 0)");
            var matrix2 = new DomMatrix("matrix(1, 0, 0, 1, 0, 0) scale(1, 2) translate(5, 0)");
            var expectedResult = new DomMatrix(1, 0, 0, 2, 5, 0);

            if (!matrix.Is2D || !matrix2.Is2D)
            {
                Assert.Fail("Matrices incorrectly marked as 3D!");
            }

            if (!MatricesEqual(matrix, expectedResult) || !MatricesEqual(matrix2, expectedResult))
            {
                Assert.Fail("Matrices not properly loaded from string!");
            }

            Assert.Pass();
        }

        [Test]
        public void RotateTest()
        {
            const double MAX_DIVERGENCE = 1e-9;

            var rotationMatrix = new DomMatrix().RotateSelf(0, 0, 30);

            IDomPoint point = new DomPoint(1, 0);
            point = rotationMatrix.TransformPoint(point);

            double expectedX = Math.Cos(Math.PI / 6);
            double expectedY = Math.Sin(Math.PI / 6);

            if ((Math.Abs(point.X - expectedX) > MAX_DIVERGENCE) || (Math.Abs(point.Y - expectedY) > MAX_DIVERGENCE)
                || (point.Z != 0) || (point.W != 1))
            {
                Assert.Fail("Rotation failed");
            }

            Assert.Pass();
        }

        [Test]
        public void InverseTest()
        {
            var matrix = new DomMatrix(5, 2, 6, 2, 6, 2, 6, 3, 6, 3, 3, 6, 8, 8, 8, 7);
            var inverse = matrix.Inverse();

            if (!matrix.MultiplySelf(inverse).IsIdentity)
            {
                Assert.Fail("Matrix inverse failed!");
            }

            Assert.Pass();
        }

    }
}
