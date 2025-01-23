using System;
using System.Linq;

namespace XafNet9Ai.Module.PivotChart
{
    public class PivotField
    {
        public string PropertyName { get; set; }
        public string Caption { get; set; }
        public string Area { get; set; } // Data, Row, Column, Filter
        public int AreaIndex { get; set; }
        public PivotSummaryType SummaryType { get; set; }
        public string Format { get; set; }
        public bool IsExpanded { get; set; }
        public SortOrder? SortOrder { get; set; }
        public FilterSettings FilterSettings { get; set; }
        public LayoutSettings LayoutSettings { get; set; }
    }
}
