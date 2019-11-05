using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDive.Common.Models
{
    public class CostVsDepthChart: BaseChart
    {
        public CostVsDepthChart()
        {
            //TODO:refactor the code to retrieve thie information for a table
            // set the configuration for the chart
            base.ChartType = "Line";
            base.YAxisInverted = true;
            base.YAxisDescription = "Average of Depth";
            base.XAxisDescription = "Cost";
            base.Lines = new List<Line>()
            { new Line(){
                YAttributeName="depth",
                XAttributeName="cumWellCost",
                LineColor="Red",
                PointColor="Red",
                SourceObjectName="afEs",
                LegendName="AFEs",
                SteppedLine=true
            },
            new Line(){
                YAttributeName="maxDepth",
                XAttributeName="cumWellCost",
                LineColor="Blue",
                PointColor="Red",
                SourceObjectName="dailyCosts",
                LegendName="Actual",
                SteppedLine=false
            },
            new Line(){
                YAttributeName="maxDepth",
                XAttributeName="cumWellCost",
                LineColor="rgb(158, 157, 155, 0.5)",
                PointColor="",
                SourceObjectName="dailyCostsForOffsetWells",
                LegendName="Composite Best Offset",
                SteppedLine=false
            },
            new Line(){
                YAttributeName="maxDepth",
                XAttributeName="cumWellCost",
                LineColor="rgb(158, 157, 155, 0.5)",
                PointColor="",
                SourceObjectName="dailyMeanCosts",
                LegendName="Mean Offset",
                SteppedLine=true
            }
            };
            base.Variances = new List<Line>()
            { new Line(){
                YAttributeName="maxDepth",
                XAttributeName="cumWellCost",
                LineColor="Red",
                PointColor="Red",
                SourceObjectName="drillVarianceCosts",
                SteppedLine=false,
                Id="varianceCostId",
                TimeStampField="dateYmd"
            }
            };


        }

        public List<DrillAFE> AFEs { get; set; }

        public List<DrillDailyCost> DailyCosts { get; set; }

        public List<DrillMeanDailyCost> DailyMeanCosts { get; set; }

        public List<DrillDailyCost> DailyCostsForOffsetWells { get; set; }

        public List<DrillVarianceCost> DrillVarianceCosts { get; set; }

    }
}
