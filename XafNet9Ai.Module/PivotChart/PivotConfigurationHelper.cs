using System;
using System.Linq;

namespace XafNet9Ai.Module.PivotChart
{
    using System;
    using System.Collections.Generic;
    // Example usage/helper class
    public static class PivotConfigurationHelper
    {
        public static string SerializeConfiguration(PivotConfiguration config)
        {
            return System.Text.Json.JsonSerializer.Serialize(config);

        }

        public static PivotConfiguration DeserializeConfiguration(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<PivotConfiguration>(json);

        }

        // Example method to create a sample configuration
        public static PivotConfiguration CreateSampleSalesAnalysisConfig()
        {
            return new PivotConfiguration
            {
                Name = "Monthly Sales by Customer",
                Description = "Analyzes monthly sales performance by customer",
                EntityFullName = "YourNamespace.Invoice",
                EntityCaption = "Invoice",
                PivotTitle = "Sales Analysis",
                IsShared = true,
                CreatedBy = "System",
                CreatedOn = DateTime.UtcNow,

                RowFields = new List<PivotField>
            {
                new PivotField
                {
                    PropertyName = "Customer.Name",
                    Caption = "Customer",
                    Area = "Row",
                    AreaIndex = 0,
                    IsExpanded = true
                }
            },

                ColumnFields = new List<PivotField>
            {
                new PivotField
                {
                    PropertyName = "Date",
                    Caption = "Month",
                    Area = "Column",
                    AreaIndex = 0,
                    Format = "MM/yyyy"
                }
            },

                DataFields = new List<PivotField>
            {
                new PivotField
                {
                    PropertyName = "TotalAmount",
                    Caption = "Sales Amount",
                    Area = "Data",
                    AreaIndex = 0,
                    SummaryType = PivotSummaryType.Sum,
                    Format = "C2",
                    LayoutSettings = new LayoutSettings
                    {
                        NumberFormat = "#,##0.00",
                        AutoFitEnabled = true,
                        HorizontalAlignment = "Right"
                    }
                }
            },

                FilterFields = new List<PivotField>
            {
                new PivotField
                {
                    PropertyName = "Customer.Region",
                    Caption = "Region",
                    Area = "Filter",
                    AreaIndex = 0,
                    FilterSettings = new FilterSettings
                    {
                        ShowGrandTotals = true,
                        ShowColumnTotals = true,
                        ShowRowTotals = true
                    }
                }
            }
            };
        }
    }
}
