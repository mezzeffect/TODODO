using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoSampleMobile.iOS.Services.Reports;
using TodoSampleMobile.Services.Reports;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReportRenderer))]
namespace TodoSampleMobile.iOS.Services.Reports
{
    public class ReportRenderer:IReportRenderer
    {
        public void Render()
        {
            //new XLabs.Forms.Charting.Controls.ChartRenderer();
        }
    }
}