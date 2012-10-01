namespace TestApplication
{
    partial class MultiInstance_MainForm
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
            this.btnAverageTimer32Example_Tick = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(17, 7);
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
            this.groupBox3.Location = new System.Drawing.Point(17, 66);
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
            this.groupBox2.Location = new System.Drawing.Point(17, 156);
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
            this.groupBox4.Controls.Add(this.btnAverageTimer32Example_Tick);
            this.groupBox4.Location = new System.Drawing.Point(17, 215);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 53);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "AverateTimer32Example";
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
            // MultiInstance_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 300);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MultiInstance_MainForm";
            this.Text = "MultiInstance.Test";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnAverageTimer32Example_Tick;
    }
}

