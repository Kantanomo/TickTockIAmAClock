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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.TickRateNum = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.advancedButton = new System.Windows.Forms.Button();
            this.userObjectAddress = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tickPanel = new System.Windows.Forms.Panel();
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
            this.TickRateNum.Size = new System.Drawing.Size(41, 20);
            this.TickRateNum.TabIndex = 2;
            this.TickRateNum.Value = new decimal(new int[] {
            60,
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
            // advancedButton
            // 
            this.advancedButton.Location = new System.Drawing.Point(12, 62);
            this.advancedButton.Name = "advancedButton";
            this.advancedButton.Size = new System.Drawing.Size(152, 23);
            this.advancedButton.TabIndex = 4;
            this.advancedButton.Text = "Advanced (Debug)";
            this.advancedButton.UseVisualStyleBackColor = true;
            this.advancedButton.Click += new System.EventHandler(this.advancedButton_Click);
            // 
            // userObjectAddress
            // 
            this.userObjectAddress.Location = new System.Drawing.Point(12, 91);
            this.userObjectAddress.Name = "userObjectAddress";
            this.userObjectAddress.Size = new System.Drawing.Size(152, 20);
            this.userObjectAddress.TabIndex = 5;
            this.toolTip1.SetToolTip(this.userObjectAddress, "Player Object Address");
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 100;
            // 
            // tickPanel
            // 
            this.tickPanel.Location = new System.Drawing.Point(179, 11);
            this.tickPanel.Name = "tickPanel";
            this.tickPanel.Size = new System.Drawing.Size(400, 200);
            this.tickPanel.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 219);
            this.Controls.Add(this.tickPanel);
            this.Controls.Add(this.userObjectAddress);
            this.Controls.Add(this.advancedButton);
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
        private System.Windows.Forms.Button advancedButton;
        public System.Windows.Forms.TextBox userObjectAddress;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel tickPanel;
    }
}

