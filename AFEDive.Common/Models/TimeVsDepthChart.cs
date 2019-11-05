using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDive.Common.Models
{
   public class TimeVsDepthChart : BaseChart
    {
        public TimeVsDepthChart()
        {
            // set the configuration for the chart
            base.ChartType = "Line";
            base.YAxisInverted = true;
            base.YAxisDescription = "Average of Depth";
            base.XAxisDescription = "Days";
            base.Lines = new List<Line>()
            { new Line(){
                YAttributeName="depth",
                XAttributeName="cumWellDuration",
                LineColor="Red",
                PointColor="Red",
                SourceObjectName="afEs",
                LegendName="AFEs",
                SteppedLine=true
            },
            new Line(){
                YAttributeName="depth",
                XAttributeName="cumWellDuration",
                LineColor="Blue",
                PointColor="Red",
                SourceObjectName="drillTimeSummaries",
                LegendName="Actual",
                SteppedLine=false
            },
            new Line()
            {
                YAttributeName="depth",
                XAttributeName="cumWellDuration",
                LineColor="rgb(158, 157, 155, 0.5)",
                PointColor="",
                SourceObjectName="drillTimeSummariesOffsetWell",
                LegendName="Composite Best Offset",
                SteppedLine=false
            },
            new Line()
            {
                YAttributeName="depth",
                XAttributeName="cumWellDuration",
                LineColor="rgb(158, 157, 155, 0.5)",
                PointColor="",
                SourceObjectName="drillMeanTimeSummaries",
                LegendName="Mean Offset",
                SteppedLine=true
            }
            };
            base.Variances = new List<Line>()
            { new Line(){
                YAttributeName="depth",
                XAttributeName="cumWellDuration",
                LineColor="Red",
                PointColor="Red",
                SourceObjectName="drillVarianceDurations",
                SteppedLine=false,
                Id="varianceDurationId",
                TimeStampField="recordedDate"
            }
            };


        }

        public List<DrillAFE> AFEs { get; set; }

        public List<DrillTimeSummary> DrillTimeSummaries { get; set; }

        public List<DrillTimeSummary> DrillTimeSummariesOffsetWell { get; set; }
        public List<DrillMeanTimeSummary> DrillMeanTimeSummaries { get; set; }

        public List<DrillVarianceDuration> DrillVarianceDurations { get; set; }

    }
}
