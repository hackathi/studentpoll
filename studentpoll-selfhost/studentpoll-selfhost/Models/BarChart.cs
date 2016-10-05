using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace studentpoll.Models
{
    public class BarChart
    {
        public string[] labels { get; set; }
        public ChartDataset[] datasets { get; set; }
    }
}