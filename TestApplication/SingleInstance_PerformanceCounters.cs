using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerformanceCounterHelper;

namespace TestApplication
{
    [PerformanceCounterCategoryAttribute("TestApplication", System.Diagnostics.PerformanceCounterCategoryType.SingleInstance, "Información acerca de la aplicación de Test de PerformanceCounterHelper.")]
    enum SingleInstance_PerformanceCounters
    {
        [PerformanceCounterAttribute("CounterExample", "Cantidad de ejemplo", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
        CounterExample,

        [PerformanceCounterAttribute("AverageCountExample", "Contador promedio de ejemplo", System.Diagnostics.PerformanceCounterType.AverageCount64)]
        AverageCountExample,

        [PerformanceCounterAttribute("RateOfCountsPerSecondExample", "medidor por segundo de ratios de ejemplo", System.Diagnostics.PerformanceCounterType.RateOfCountsPerSecond64)]
        RateOfCountsPerSecondExample,

        [PerformanceCounterAttribute("SimpleFractionExample", "fraccion de ejemplo", System.Diagnostics.PerformanceCounterType.SampleFraction, false)]
        SimpleFractionExample,

        [PerformanceCounterAttribute("AverageTimer32Example", "Contar promedio de tiempo de ejemplo", System.Diagnostics.PerformanceCounterType.AverageTimer32, true)]
        AverageTimer32Example
 
    }
}