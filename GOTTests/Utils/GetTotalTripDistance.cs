using Xunit;
using GOT.Models;

namespace GOTTests.Utils {
    public class GetTotalTripDistance {
        [Fact]
        public void EmptyTripReturnsZero() {
            Trip trip = new Trip();

            int result = GOT.Models.Utils.GetTotalTripDistance(trip);

            Assert.Equal(0, result);
        }

        [Fact]
        public void NormalTripReturnsRightNumber() {
            Trip trip = new Trip() {
                PathTrips = new PathTrip[3] {
                    new PathTrip {
                        Path = new Path {
                            ElevationAB = 100,
                            DistanceAB = 1000,
                            ElevationBA = 0,
                            DistanceBA = 1000
                        },
                        IsFromAToB = true
                    },
                    new PathTrip {
                        Path = new Path {
                            ElevationAB = 200,
                            DistanceAB = 3000,
                            ElevationBA = 0,
                            DistanceBA = 2000
                        },
                        IsFromAToB = false
                    },
                    new PathTrip {
                        Path = new Path {
                            ElevationAB = 0,
                            DistanceAB = 4000,
                            ElevationBA = 100,
                            DistanceBA = 4000
                        },
                        IsFromAToB = true
                    }
                }
            };

            int result = GOT.Models.Utils.GetTotalTripDistance(trip);

            Assert.Equal(7000, result);
        }
    }
}
