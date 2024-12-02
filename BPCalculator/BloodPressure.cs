using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace BPCalculator
{
    // BP categories
    public enum BPCategory
    {
        [Display(Name = "Low Blood Pressure")] Low,
        [Display(Name = "Ideal Blood Pressure")] Ideal,
        [Display(Name = "Pre-High Blood Pressure")] PreHigh,
        [Display(Name = "High Blood Pressure")] High
    };

    public class BloodPressure
    {
        public const int SystolicMin = 70;
        public const int SystolicMax = 190;
        public const int DiastolicMin = 40;
        public const int DiastolicMax = 100;

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }                       // mmHG

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }                      // mmHG

        // calculate BP category
        public BPCategory Category
        {
            get
            {
                if (Systolic < 90 && Diastolic < 60)
                {
                    return BPCategory.Low;
                }
                else if (Systolic >= 90 && Systolic <= 120 && Diastolic >= 60 && Diastolic <= 80)
                {
                    return BPCategory.Ideal;
                }
                else if ((Systolic > 120 && Systolic < 140) || (Diastolic > 80 && Diastolic < 90))
                {
                    return BPCategory.PreHigh;
                }
                else if (Systolic >= 140 || Diastolic >= 90)
                {
                    return BPCategory.High;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Invalid blood pressure values");
                }
            }
        }

        // Method to generate a plot of systolic vs diastolic values
        public PlotModel GenerateBloodPressureGraph()
        {
            var plotModel = new PlotModel { Title = "Blood Pressure Chart" };

            // Adding axes for the graph
            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Measurement Number",
                Minimum = 0,
                Maximum = 10
            });

            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Blood Pressure (mmHg)",
                Minimum = 30,
                Maximum = 200
            });

            // Adding systolic series
            var systolicSeries = new LineSeries
            {
                Title = "Systolic",
                MarkerType = MarkerType.Circle
            };

            // Adding diastolic series
            var diastolicSeries = new LineSeries
            {
                Title = "Diastolic",
                MarkerType = MarkerType.Square
            };

            // Assuming we have some sample data for demonstration
            for (int i = 0; i < 10; i++)
            {
                systolicSeries.Points.Add(new DataPoint(i, Systolic));
                diastolicSeries.Points.Add(new DataPoint(i, Diastolic));
            }

            plotModel.Series.Add(systolicSeries);
            plotModel.Series.Add(diastolicSeries);

            return plotModel;
        }
    }
}
