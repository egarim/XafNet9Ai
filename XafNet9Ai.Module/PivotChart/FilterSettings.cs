using System;
using System.Linq;

namespace XafNet9Ai.Module.PivotChart
{
    using System.Collections.Generic;
    public class FilterSettings
    {
        public string FilterType { get; set; } // Value, Set, TopN, etc.
        public object FilterValue { get; set; }
        public bool ShowGrandTotals { get; set; } = true;
        public bool ShowColumnTotals { get; set; } = true;
        public bool ShowRowTotals { get; set; } = true;
        public List<object> FilteredValues { get; set; }
    }
}
