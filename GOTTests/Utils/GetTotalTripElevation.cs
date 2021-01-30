using GOT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GOTTests.Utils
{
    public class GetTotalTripElevation
    {
        [Fact]
        public void EmptyTripReturnsZero()
        {
            Trip trip = new Trip();

            int result = GOT.Models.Utils.GetTotalTripElevation(trip);

            Assert.Equal(0, result);
        }

        [Fact]
        public void NormalTripReturnsRightNumber()
        {
            Trip trip = new Trip()
            {
                PathTrips = new PathTrip[3] {
                    new PathTrip {
                        Path = new Path {
                            ElevationAB = 250,
                            DistanceAB = 2500,
                            ElevationBA = 25,
                            DistanceBA = 2500
                        },
                        IsFromAToB = false
                    },
                    new PathTrip {
                        Path = new Path {
                            ElevationAB = 300,
                            DistanceAB = 3000,
                            ElevationBA = 150,
                            DistanceBA = 2000
                        },
                        IsFromAToB = false
                    },
                    new PathTrip {
                        Path = new Path {
                            ElevationAB = 45,
                            DistanceAB = 3600,
                            ElevationBA = 155,
                            DistanceBA = 4000
                        },
                        IsFromAToB = true
                    }
                }
            };

            int result = GOT.Models.Utils.GetTotalTripElevation(trip);

            Assert.Equal(220, result);
        }
    }
}
