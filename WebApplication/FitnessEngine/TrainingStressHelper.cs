using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessEngine
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public static class TrainingStressHelper
    {
        //Takes average exercise HR, resting HR and maxHR and determines the effective level of effort of an exercise
        public static double HeartRateResponse(double hrExercise, double hrRest, double hrMax)
        {
            var maxDeviation = hrMax - hrRest;
            if (maxDeviation == 0)
            {
                throw new Exception("Invalid parameters");
            }
            return (hrExercise - hrRest) / maxDeviation;
        }


        public static double Trimp(double durationInMinutes, double hrResponse, bool male)
        {
            return durationInMinutes * hrResponse * HighIntensityWeightingFactor(male, hrResponse);
        }

        public static double Trimp(double durationInMinutes, double hrExercise, double hrRest, double hrMax, bool male)
        {
            var hrResponse = HeartRateResponse(hrExercise, hrRest, hrMax);
            return Trimp(durationInMinutes, hrResponse, male);
        }

        private static double HighIntensityWeightingFactor(bool male, double hrResponse)
        {
            var baseGenderWeighting = male ? 0.64 : 0.86;
            var expGenderWeighting = male ? 1.92 : 1.67;
            var e = 2.712;

            var factor = baseGenderWeighting * Math.Pow(e, expGenderWeighting * hrResponse);

            return factor;
        }
    }
}
