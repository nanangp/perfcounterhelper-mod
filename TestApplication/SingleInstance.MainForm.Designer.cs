namespace TestApplication
{
    partial class SingleInstance_MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCounterExample_Increase = new System.Windows.Forms.Button();
            this.btnCounterExample_Decrease = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAverageCountExample_RandomPush = new System.Windows.Forms.Button();
            this.txtAverageCountExample = new System.Windows.Forms.TextBox();
            this.btnAverageCountExample_Push = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRateOfCountsPerSecondExample_Tick = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnSimpleFractionExample_DivisorRandomPush = new System.Windows.Forms.Button();
            this.txtSimpleFractionExampleDivisor = new System.Windows.Forms.TextBox();
            this.btnSimpleFractionExample_DivisorPush = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnSimpleFractionExample_DividendRandomPush = new System.Windows.Forms.Button();
            this.txtSimpleFractionExampleDividend = new System.Windows.Forms.TextBox();
            this.btnSimpleFractionExample_DividendPush = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnAverageTimer32Example_Tick = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCounterExample_Increase
            // 
            this.btnCounterExample_Increase.Location = new System.Drawing.Point(6, 19);
            this.btnCounterExample_Increase.Name = "btnCounterExample_Increase";
            this.btnCounterExample_Increase.Size = new System.Drawing.Size(75, 23);
            this.btnCounterExample_Increase.TabIndex = 0;
            this.btnCounterExample_Increase.Text = "Increase";
            this.btnCounterExample_Increase.UseVisualStyleBackColor = true;
            this.btnCounterExample_Increase.Click += new System.EventHandler(this.btnCounterExample_Increase_Click);
            // 
            // btnCounterExample_Decrease
            // 
            this.btnCounterExample_Decrease.Enabled = false;
            this.btnCounterExample_Decrease.Location = new System.Drawing.Point(112, 19);
            this.btnCounterExample_Decrease.Name = "btnCounterExample_Decrease";
            this.btnCounterExample_Decrease.Size = new System.Drawing.Size(75, 23);
            this.btnCounterExample_Decrease.TabIndex = 2;
            this.btnCounterExample_Decrease.Text = "Decrease";
            this.btnCounterExample_Decrease.UseVisualStyleBackColor = true;
            this.btnCounterExample_Decrease.Click += new System.EventHandler(this.btnCounterExample_Decrease_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCounterExample_Increase);
            this.groupBox1.Controls.Add(this.btnCounterExample_Decrease);
            this.groupBox1.Location = new System.Drawing.Point(17, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 53);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CounterExample";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnAverageCountExample_RandomPush);
            this.groupBox3.Controls.Add(this.txtAverageCountExample);
            this.groupBox3.Controls.Add(this.btnAverageCountExample_Push);
            this.groupBox3.Location = new System.Drawing.Point(17, 65);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 84);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "AverageCountExample";
            // 
            // btnAverageCountExample_RandomPush
            // 
            this.btnAverageCountExample_RandomPush.Location = new System.Drawing.Point(48, 48);
            this.btnAverageCountExample_RandomPush.Name = "btnAverageCountExample_RandomPush";
            this.btnAverageCountExample_RandomPush.Size = new System.Drawing.Size(92, 23);
            this.btnAverageCountExample_RandomPush.TabIndex = 6;
            this.btnAverageCountExample_RandomPush.Text = "Random Push";
            this.btnAverageCountExample_RandomPush.UseVisualStyleBackColor = true;
            this.btnAverageCountExample_RandomPush.Click += new System.EventHandler(this.btnAverageCountExample_RandomPush_Click);
            // 
            // txtAverageCountExample
            // 
            this.txtAverageCountExample.Location = new System.Drawing.Point(87, 22);
            this.txtAverageCountExample.Name = "txtAverageCountExample";
            this.txtAverageCountExample.Size = new System.Drawing.Size(100, 20);
            this.txtAverageCountExample.TabIndex = 5;
            // 
            // btnAverageCountExample_Push
            // 
            this.btnAverageCountExample_Push.Location = new System.Drawing.Point(6, 22);
            this.btnAverageCountExample_Push.Name = "btnAverageCountExample_Push";
            this.btnAverageCountExample_Push.Size = new System.Drawing.Size(75, 23);
            this.btnAverageCountExample_Push.TabIndex = 3;
            this.btnAverageCountExample_Push.Text = "Push";
            this.btnAverageCountExample_Push.UseVisualStyleBackColor = true;
            this.btnAverageCountExample_Push.Click += new System.EventHandler(this.btnAverageCountExample_Push_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRateOfCountsPerSecondExample_Tick);
            this.groupBox2.Location = new System.Drawing.Point(17, 155);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 53);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RateOfCountsPerSecondExample";
            // 
            // btnRateOfCountsPerSecondExample_Tick
            // 
            this.btnRateOfCountsPerSecondExample_Tick.Location = new System.Drawing.Point(56, 19);
            this.btnRateOfCountsPerSecondExample_Tick.Name = "btnRateOfCountsPerSecondExample_Tick";
            this.btnRateOfCountsPerSecondExample_Tick.Size = new System.Drawing.Size(75, 23);
            this.btnRateOfCountsPerSecondExample_Tick.TabIndex = 0;
            this.btnRateOfCountsPerSecondExample_Tick.Text = "Tick";
            this.btnRateOfCountsPerSecondExample_Tick.UseVisualStyleBackColor = true;
            this.btnRateOfCountsPerSecondExample_Tick.Click += new System.EventHandler(this.btnRateOfCountsPerSecondExample_Tick_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Location = new System.Drawing.Point(223, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(233, 232);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "SimpleFractionExample";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnSimpleFractionExample_DivisorRandomPush);
            this.groupBox6.Controls.Add(this.txtSimpleFractionExampleDivisor);
            this.groupBox6.Controls.Add(this.btnSimpleFractionExample_DivisorPush);
            this.groupBox6.Location = new System.Drawing.Point(16, 110);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(200, 84);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Divisor";
            // 
            // btnSimpleFractionExample_DivisorRandomPush
            // 
            this.btnSimpleFractionExample_DivisorRandomPush.Location = new System.Drawing.Point(48, 48);
            this.btnSimpleFractionExample_DivisorRandomPush.Name = "btnSimpleFractionExample_DivisorRandomPush";
            this.btnSimpleFractionExample_DivisorRandomPush.Size = new System.Drawing.Size(92, 23);
            this.btnSimpleFractionExample_DivisorRandomPush.TabIndex = 6;
            this.btnSimpleFractionExample_DivisorRandomPush.Text = "Random Push";
            this.btnSimpleFractionExample_DivisorRandomPush.UseVisualStyleBackColor = true;
            this.btnSimpleFractionExample_DivisorRandomPush.Click += new System.EventHandler(this.btnSimpleFractionExample_DivisorRandomPush_Click);
            // 
            // txtSimpleFractionExampleDivisor
            // 
            this.txtSimpleFractionExampleDivisor.Location = new System.Drawing.Point(87, 22);
            this.txtSimpleFractionExampleDivisor.Name = "txtSimpleFractionExampleDivisor";
            this.txtSimpleFractionExampleDivisor.Size = new System.Drawing.Size(100, 20);
            this.txtSimpleFractionExampleDivisor.TabIndex = 5;
            // 
            // btnSimpleFractionExample_DivisorPush
            // 
            this.btnSimpleFractionExample_DivisorPush.Location = new System.Drawing.Point(6, 22);
            this.btnSimpleFractionExample_DivisorPush.Name = "btnSimpleFractionExample_DivisorPush";
            this.btnSimpleFractionExample_DivisorPush.Size = new System.Drawing.Size(75, 23);
            this.btnSimpleFractionExample_DivisorPush.TabIndex = 3;
            this.btnSimpleFractionExample_DivisorPush.Text = "Push";
            this.btnSimpleFractionExample_DivisorPush.UseVisualStyleBackColor = true;
            this.btnSimpleFractionExample_DivisorPush.Click += new System.EventHandler(this.btnSimpleFractionExample_DivisorPush_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnSimpleFractionExample_DividendRandomPush);
            this.groupBox5.Controls.Add(this.txtSimpleFractionExampleDividend);
            this.groupBox5.Controls.Add(this.btnSimpleFractionExample_DividendPush);
            this.groupBox5.Location = new System.Drawing.Point(16, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 84);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Dividend";
            // 
            // btnSimpleFractionExample_DividendRandomPush
            // 
            this.btnSimpleFractionExample_DividendRandomPush.Location = new System.Drawing.Point(48, 48);
            this.btnSimpleFractionExample_DividendRandomPush.Name = "btnSimpleFractionExample_DividendRandomPush";
            this.btnSimpleFractionExample_DividendRandomPush.Size = new System.Drawing.Size(92, 23);
            this.btnSimpleFractionExample_DividendRandomPush.TabIndex = 6;
            this.btnSimpleFractionExample_DividendRandomPush.Text = "Random Push";
            this.btnSimpleFractionExample_DividendRandomPush.UseVisualStyleBackColor = true;
            this.btnSimpleFractionExample_DividendRandomPush.Click += new System.EventHandler(this.btnSimpleFractionExample_DividendRandomPush_Click);
            // 
            // txtSimpleFractionExampleDividend
            // 
            this.txtSimpleFractionExampleDividend.Location = new System.Drawing.Point(87, 22);
            this.txtSimpleFractionExampleDividend.Name = "txtSimpleFractionExampleDividend";
            this.txtSimpleFractionExampleDividend.Size = new System.Drawing.Size(100, 20);
            this.txtSimpleFractionExampleDividend.TabIndex = 5;
            // 
            // btnSimpleFractionExample_DividendPush
            // 
            this.btnSimpleFractionExample_DividendPush.Location = new System.Drawing.Point(6, 22);
            this.btnSimpleFractionExample_DividendPush.Name = "btnSimpleFractionExample_DividendPush";
            this.btnSimpleFractionExample_DividendPush.Size = new System.Drawing.Size(75, 23);
            this.btnSimpleFractionExample_DividendPush.TabIndex = 3;
            this.btnSimpleFractionExample_DividendPush.Text = "Push";
            this.btnSimpleFractionExample_DividendPush.UseVisualStyleBackColor = true;
            this.btnSimpleFractionExample_DividendPush.Click += new System.EventHandler(this.btnSimpleFractionExample_DividendPush_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnAverageTimer32Example_Tick);
            this.groupBox7.Location = new System.Drawing.Point(17, 214);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(200, 53);
            this.groupBox7.TabIndex = 13;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "AverateTimer32Example";
            // 
            // btnAverageTimer32Example_Tick
            // 
            this.btnAverageTimer32Example_Tick.Location = new System.Drawing.Point(56, 19);
            this.btnAverageTimer32Example_Tick.Name = "btnAverageTimer32Example_Tick";
            this.btnAverageTimer32Example_Tick.Size = new System.Drawing.Size(75, 23);
            this.btnAverageTimer32Example_Tick.TabIndex = 0;
            this.btnAverageTimer32Example_Tick.Text = "Start";
            this.btnAverageTimer32Example_Tick.UseVisualStyleBackColor = true;
            this.btnAverageTimer32Example_Tick.Click += new System.EventHandler(this.btnAverageTimer32Example_Tick_Click);
            // 
            // SingleInstance_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 320);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "SingleInstance_MainForm";
            this.Text = "SingleInstance.Test";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCounterExample_Increase;
        private System.Windows.Forms.Button btnCounterExample_Decrease;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtAverageCountExample;
        private System.Windows.Forms.Button btnAverageCountExample_Push;
        private System.Windows.Forms.Button btnAverageCountExample_RandomPush;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRateOfCountsPerSecondExample_Tick;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnSimpleFractionExample_DivisorRandomPush;
        private System.Windows.Forms.TextBox txtSimpleFractionExampleDivisor;
        private System.Windows.Forms.Button btnSimpleFractionExample_DivisorPush;
        private System.Windows.Forms.Button btnSimpleFractionExample_DividendRandomPush;
        private System.Windows.Forms.TextBox txtSimpleFractionExampleDividend;
        private System.Windows.Forms.Button btnSimpleFractionExample_DividendPush;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnAverageTimer32Example_Tick;
    }
}

