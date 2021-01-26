using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GOT.Models;

namespace GOT.ViewModels {
    public class PathSelectionViewModel : Trip {
        public IEnumerable<Path> AvailablePaths { get; set; }
        public bool CanChooseBidirectionally { get; set; }

    }
}
