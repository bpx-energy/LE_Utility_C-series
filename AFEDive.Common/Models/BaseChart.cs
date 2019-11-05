using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDive.Common.Models
{
    /// <summary>
    /// Base Class for Chart Definition
    /// </summary>
    public abstract class BaseChart
    {
        public string ChartType { get; set; }
        public bool YAxisInverted  { get; set; }
        public string YAxisDescription { get; set; }
        public string XAxisDescription { get; set; }
        public List<Line> Lines { get; set; }
        public List <Line> Variances { get; set; }

    }

    /// <summary>
    /// Definition for Line
    /// </summary>
    public class Line
    {
        public string SourceObjectName { get; set; }
        public string XAttributeName { get; set; }
        public string YAttributeName { get; set; }
        public string LineColor { get; set; }
        public string PointColor { get; set; }
        public bool SteppedLine { get; set; }
        public bool ImageUrl { get; set; }
        public string LegendName { get; set; }
        public string Id { get; set; }
        public string TimeStampField { get; set; }
    }

}
