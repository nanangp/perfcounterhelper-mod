using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PerformanceCounterHelper;
using System.Diagnostics;

namespace TestApplication
{
    public partial class SingleInstance_MainForm : Form
    {
        CounterHelper<SingleInstance_PerformanceCounters> counterHelper;

        public SingleInstance_MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            counterHelper = PerformanceHelper.CreateCounterHelper<SingleInstance_PerformanceCounters>();
            if (counterHelper == null)
            {
                MessageBox.Show("Counter not installed. Attempting to install.");
                PerformanceHelper.Install(typeof(SingleInstance_PerformanceCounters));
            }
        }

        private void btnCounterExample_Increase_Click(object sender, EventArgs e)
        {
            if (counterHelper.Increment(SingleInstance_PerformanceCounters.CounterExample) > 0)
                this.btnCounterExample_Decrease.Enabled = true;
        }

        private void btnCounterExample_Decrease_Click(object sender, EventArgs e)
        {
            if (counterHelper.Decrement(SingleInstance_PerformanceCounters.CounterExample) == 0)
                this.btnCounterExample_Decrease.Enabled = false;
        }

        private void btnAverageCountExample_Push_Click(object sender, EventArgs e)
        {
            long value = 0;
            if (!long.TryParse(txtAverageCountExample.Text, out value))
            {
                MessageBox.Show(String.Format("'{0}' is not a valid value.", txtAverageCountExample.Text), "Warning");
            }
            else
            {
                counterHelper.IncrementBy(SingleInstance_PerformanceCounters.AverageCountExample, value);
                //counterHelper.Increment(SingleInstance_PerformanceCounters.AverageCountExampleBase);

            }
        }

        private void btnAverageCountExample_RandomPush_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            long value = rnd.Next(0, 100);

            counterHelper.IncrementBy(SingleInstance_PerformanceCounters.AverageCountExample, value);
            //counterHelper.Increment(SingleInstance_PerformanceCounters.AverageCountExampleBase);
        }

        private void btnRateOfCountsPerSecondExample_Tick_Click(object sender, EventArgs e)
        {
            counterHelper.Increment(SingleInstance_PerformanceCounters.RateOfCountsPerSecondExample);
        }

        private void btnSimpleFractionExample_DividendPush_Click(object sender, EventArgs e)
        {
            long value = 0;
            if (!long.TryParse(txtSimpleFractionExampleDividend.Text, out value))
            {
                MessageBox.Show(String.Format("'{0}' is not a valid value.", txtSimpleFractionExampleDividend.Text), "Warning");
            }
            else
            {
                counterHelper.IncrementBy(SingleInstance_PerformanceCounters.SimpleFractionExample, value);
                //counterHelper.Increment(SingleInstance_PerformanceCounters.AverageCountExampleBase);

            }
        }

        private void btnSimpleFractionExample_DividendRandomPush_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            long value = rnd.Next(0, 100);

            counterHelper.IncrementBy(SingleInstance_PerformanceCounters.SimpleFractionExample, value);
            //counterHelper.Increment(SingleInstance_PerformanceCounters.AverageCountExampleBase);

        }

        private void btnSimpleFractionExample_DivisorPush_Click(object sender, EventArgs e)
        {
            long value = 0;
            if (!long.TryParse(txtSimpleFractionExampleDivisor.Text, out value))
            {
                MessageBox.Show(String.Format("'{0}' is not a valid value.", txtSimpleFractionExampleDivisor.Text), "Warning");
            }
            else
            {
                counterHelper.IncrementBaseBy(SingleInstance_PerformanceCounters.SimpleFractionExample, value);
                //counterHelper.Increment(SingleInstance_PerformanceCounters.AverageCountExampleBase);

            }

        }

        private void btnSimpleFractionExample_DivisorRandomPush_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            long value = rnd.Next(0, 100);

            counterHelper.IncrementBaseBy(SingleInstance_PerformanceCounters.SimpleFractionExample, value);
            //counterHelper.Increment(SingleInstance_PerformanceCounters.AverageCountExampleBase);
        }

        private void btnAverageTimer32Example_Tick_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = null;
            if (btnAverageTimer32Example_Tick.Tag == null)
            {
                btnAverageTimer32Example_Tick.Text = "Stop";
                stopwatch = Stopwatch.StartNew();
                btnAverageTimer32Example_Tick.Tag = stopwatch;
            }
            else
            {
                btnAverageTimer32Example_Tick.Text = "Start";
                stopwatch = (Stopwatch)btnAverageTimer32Example_Tick.Tag;
                stopwatch.Stop();
                counterHelper.IncrementBy(SingleInstance_PerformanceCounters.AverageTimer32Example, stopwatch.ElapsedTicks);
                btnAverageTimer32Example_Tick.Tag = null;
            }
        }
    }
}
