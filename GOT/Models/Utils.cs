using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOT.Models {
    public static class Utils {
        public static int GetTripScore(Trip trip) {
            if ((trip.PathTrips?.Count() ?? 0) == 0) {
                return 0;
            }

            int score = 0;
            foreach (var pathTrip in trip.PathTrips) {
                score += GetPathTripScore(pathTrip);
            }

            return score;
        }

        public static int GetPathTripScore(PathTrip pathTrip) {
            return GetPathScore(pathTrip.Path, pathTrip.IsFromAToB);
        }

        public static int GetPathScore(Path path, bool isFromAToB) {
            int elevation = isFromAToB ? path.ElevationAB : path.ElevationBA;
            int distance = isFromAToB ? path.DistanceAB : path.DistanceBA;

            int elevationScore = elevation / 100 + (elevation % 100 >= 51 ? 1 : 0);
            int distanceScore = distance / 1000 + (distance % 1000 > 500 ? 1 : 0);

            return elevationScore + distanceScore;
        }

        public static int GetTotalTripDistance(Trip trip) {
            if ((trip.PathTrips?.Count() ?? 0) == 0) {
                return 0;
            }

            int distance = 0;
            foreach (var pathTrip in trip.PathTrips) {
                distance += pathTrip.IsFromAToB ? pathTrip.Path.DistanceAB : pathTrip.Path.DistanceBA;
            }
            return distance;
        }

        public static int GetTotalTripElevation(Trip trip) {
            if ((trip.PathTrips?.Count() ?? 0) == 0) {
                return 0;
            }

            int elevation = 0;
            foreach (var pathTrip in trip.PathTrips) {
                elevation += pathTrip.IsFromAToB ? pathTrip.Path.ElevationAB : pathTrip.Path.ElevationBA;
            }
            return elevation;
        }

        public static IEnumerable<Area> GetTripAreas(Trip trip) {
            if ((trip.PathTrips?.Count() ?? 0) == 0) {
                return new List<Area>();
            }

            List<Area> areas = new List<Area>();
            foreach (var pathTrip in trip.PathTrips) {
                if (!areas.Any(area => area.AreaId == pathTrip.Path.CheckpointA.AreaId)) {
                    areas.Add(pathTrip.Path.CheckpointA.Area);
                }
                if (!areas.Any(area => area.AreaId == pathTrip.Path.CheckpointB.AreaId)) {
                    areas.Add(pathTrip.Path.CheckpointB.Area);
                }
            }

            return areas;
        }
    }
}
