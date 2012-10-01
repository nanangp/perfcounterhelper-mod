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
    public partial class MultiInstance_MainForm : Form
    {
        CounterHelper<MultiInstance_PerformanceCounters> counterHelper;

        public MultiInstance_MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            counterHelper = PerformanceHelper.CreateCounterHelper<MultiInstance_PerformanceCounters>();
        }

        private void btnCounterExample_Increase_Click(object sender, EventArgs e)
        {
            if (counterHelper.Increment(MultiInstance_PerformanceCounters.CounterExample) > 0)
                this.btnCounterExample_Decrease.Enabled = true;
        }

        private void btnCounterExample_Decrease_Click(object sender, EventArgs e)
        {
            if (counterHelper.Decrement(MultiInstance_PerformanceCounters.CounterExample) == 0)
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
                counterHelper.IncrementBy(MultiInstance_PerformanceCounters.AverageCountExample, value);
                //counterHelper.Increment(MultiInstance_PerformanceCounters.AverageCountExampleBase);

            }
        }

        private void btnAverageCountExample_RandomPush_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            long value = rnd.Next(0, 100);

            counterHelper.IncrementBy(MultiInstance_PerformanceCounters.AverageCountExample, value);
            //counterHelper.Increment(MultiInstance_PerformanceCounters.AverageCountExampleBase);
        }

        private void btnRateOfCountsPerSecondExample_Tick_Click(object sender, EventArgs e)
        {
            counterHelper.Increment(MultiInstance_PerformanceCounters.RateOfCountsPerSecondExample);
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
                counterHelper.IncrementBy(MultiInstance_PerformanceCounters.AverageTimer32Example, stopwatch.ElapsedMilliseconds);
                btnAverageTimer32Example_Tick.Tag = null;
            }
        }
    }
}
