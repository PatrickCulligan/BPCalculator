using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;


namespace BPCalculator
{
    // BP categories
    public enum BPCategory
    {
        [Display(Name = "Low Blood Pressure")] Low,
        [Display(Name = "Ideal Blood Pressure")] Ideal,
        [Display(Name = "Pre-High Blood Pressure")] PreHigh,
        [Display(Name = "High Blood Pressure")] High,
        [Display(Name = "Requires Further assessment")] Additonal_Assessment_Required
    }

    public class BloodPressure
    {
        public const int SystolicMin = 70;
        public const int SystolicMax = 190;
        public const int DiastolicMin = 40;
        public const int DiastolicMax = 100;

        private int systolic;
        private int diastolic;

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic
        {
            get => systolic;
            set
            {
                if (value < SystolicMin || value > SystolicMax)
                {
                    throw new ArgumentOutOfRangeException($"Systolic value must be between {SystolicMin} and {SystolicMax}.");
                }
                systolic = value;
            }
        }

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic
        {
            get => diastolic;
            set
            {
                if (value < DiastolicMin || value > DiastolicMax)
                {
                    throw new ArgumentOutOfRangeException($"Diastolic value must be between {DiastolicMin} and {DiastolicMax}.");
                }
                diastolic = value;
            }
        }

        // calculate BP category
        public BPCategory Category
        {
            get
            {
                try
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
                        return BPCategory.Additonal_Assessment_Required;
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error calculating blood pressure category", ex);
                }
            }
        }
    }
}
