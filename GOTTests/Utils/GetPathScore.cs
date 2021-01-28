using Xunit;
using GOT.Models;

namespace GOTTests.Utils {
    public class GetPathScore {
        [Fact]
        public void EmptyPathReturnsZero() {
            Path path = new Path();
            bool isFromAToB = true;

            int resultAB = GOT.Models.Utils.GetPathScore(path, isFromAToB);
            int resultBA = GOT.Models.Utils.GetPathScore(path, !isFromAToB);

            Assert.Equal(0, resultAB);
            Assert.Equal(0, resultBA);
        }

        [Fact]
        public void NormalPathReturnsRightNumber() {
            Path path = new Path { ElevationAB = 200, DistanceAB = 1000, ElevationBA = 0, DistanceBA = 1000 };
            bool isFromAToB = true;

            int resultAB = GOT.Models.Utils.GetPathScore(path, isFromAToB);
            int resultBA = GOT.Models.Utils.GetPathScore(path, !isFromAToB);

            Assert.Equal(3, resultAB);
            Assert.Equal(1, resultBA);
        }

        [Fact]
        public void MethodProperlyRoundsScore() {
            Path path = new Path { ElevationAB = 250, DistanceAB = 1500, ElevationBA = 251, DistanceBA = 1501 };
            bool isFromAToB = true;

            int resultAB = GOT.Models.Utils.GetPathScore(path, isFromAToB);
            int resultBA = GOT.Models.Utils.GetPathScore(path, !isFromAToB);

            Assert.Equal(3, resultAB);
            Assert.Equal(5, resultBA);
        }
    }
}
