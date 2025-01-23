using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafNet9Ai.Module.PivotChart
{
    using System;
    using System.Collections.Generic;


    public class PivotConfiguration
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public string EntityFullName { get; set; }
        public string EntityCaption { get; set; }
        public string PivotTitle { get; set; }
        public bool IsShared { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public List<PivotField> DataFields { get; set; } = new();
        public List<PivotField> RowFields { get; set; } = new();
        public List<PivotField> ColumnFields { get; set; } = new();
        public List<PivotField> FilterFields { get; set; } = new();
    }
}
