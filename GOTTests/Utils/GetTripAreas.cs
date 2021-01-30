using GOT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GOTTests.Utils
{
    public class GetTripAreas
    {
        [Fact]
        public void EmptyTripReturnsNoArea()
        {
            Trip trip = new Trip();

            var result = GOT.Models.Utils.GetTripAreas(trip);

            Assert.Empty(result);
        }

        [Fact]
        public void NormalTripWithCheckpointsInOneAreaReturnsRightArea()
        {
            Area area = new Area { AreaId = 1, AreaName = "Tatry" };
            Trip trip = new Trip()
            {
                PathTrips = new PathTrip[3] {
                    new PathTrip {
                        Path = new Path {
                            CheckpointA = new Checkpoint { Area = area },
                            CheckpointB = new Checkpoint { Area = area }
                        }
                    },
                    new PathTrip {
                        Path = new Path {
                            CheckpointA = new Checkpoint { Area = area },
                            CheckpointB = new Checkpoint { Area = area }
                        }
                    },
                    new PathTrip {
                        Path = new Path {
                            CheckpointA = new Checkpoint { Area = area },
                            CheckpointB = new Checkpoint { Area = area }
                        }
                    }
                }
            };

            var result = GOT.Models.Utils.GetTripAreas(trip);

            Assert.All(result, item => Assert.Contains("Tatry", item.AreaName));
        }

        [Fact]
        public void NormalTripWithCheckpointsInSeveralAresaReturnsRightAreas()
        {
            Area firstArea = new Area { AreaId = 1, AreaName = "Tatry" };
            Area secondArea = new Area { AreaId = 2, AreaName = "Sudety" };
            Area thirdArea = new Area { AreaId = 3, AreaName = "Beskidy Wschodnie" };
            Area fourthArea = new Area { AreaId = 4, AreaName = "Beskidy Zachodnie" };
            Trip trip = new Trip()
            {
                PathTrips = new PathTrip[4] {
                    new PathTrip {
                        Path = new Path {
                            CheckpointA = new Checkpoint { Area = firstArea },
                            CheckpointB = new Checkpoint { Area = secondArea }
                        }
                    },
                    new PathTrip {
                        Path = new Path {
                            CheckpointA = new Checkpoint { Area = secondArea },
                            CheckpointB = new Checkpoint { Area = thirdArea }
                        }
                    },
                    new PathTrip {
                        Path = new Path {
                            CheckpointA = new Checkpoint { Area = thirdArea },
                            CheckpointB = new Checkpoint { Area = fourthArea }
                        }
                    },
                    new PathTrip {
                        Path = new Path {
                            CheckpointA = new Checkpoint { Area = fourthArea },
                            CheckpointB = new Checkpoint { Area = firstArea }
                        }
                    }

                }
            };

            var result = GOT.Models.Utils.GetTripAreas(trip);

            Assert.Collection(result.Select(a => a.AreaName).Distinct(), item => Assert.Contains("Tatry", item),
                                                                         item => Assert.Contains("Sudety", item),
                                                                         item => Assert.Contains("Beskidy Wschodnie", item),
                                                                         item => Assert.Contains("Beskidy Zachodnie", item));
        }
    }
}
