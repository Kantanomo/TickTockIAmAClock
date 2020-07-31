namespace TickTockIAmAClock
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.TickRateNum = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.TickRateNum)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tickrate";
            // 
            // TickRateNum
            // 
            this.TickRateNum.Location = new System.Drawing.Point(12, 13);
            this.TickRateNum.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.TickRateNum.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.TickRateNum.Name = "TickRateNum";
            this.TickRateNum.Size = new System.Drawing.Size(100, 20);
            this.TickRateNum.TabIndex = 2;
            this.TickRateNum.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.TickRateNum.ValueChanged += new System.EventHandler(this.TickRateNum_ValueChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 39);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(161, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Native 60 (Requires 60 FPS)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(178, 59);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.TickRateNum);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "It\'s all so relative";
            ((System.ComponentModel.ISupportInitialize)(this.TickRateNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown TickRateNum;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

