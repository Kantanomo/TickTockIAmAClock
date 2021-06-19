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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.getTickRatePage = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.getRateMaster = new System.Windows.Forms.NumericUpDown();
            this.getRateReset = new System.Windows.Forms.Button();
            this.adjustTickIntPage = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.adjustIntMaster = new System.Windows.Forms.NumericUpDown();
            this.adjustIntReset = new System.Windows.Forms.Button();
            this.getDeltaPage = new System.Windows.Forms.TabPage();
            this.getDeltaDeactivate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.deltaTick = new System.Windows.Forms.NumericUpDown();
            this.getDeltaMultiPage = new System.Windows.Forms.TabPage();
            this.isInterpolating = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.descriptorLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.advancedTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TickRateNum)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.getTickRatePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.getRateMaster)).BeginInit();
            this.adjustTickIntPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adjustIntMaster)).BeginInit();
            this.getDeltaPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deltaTick)).BeginInit();
            this.statusStrip1.SuspendLayout();
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
            this.advancedTip.SetToolTip(this.advancedButton, "Enabling Advanced Mode will cause you to have to restart your game to enable orig" +
        "inal functionality.");
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.getTickRatePage);
            this.tabControl1.Controls.Add(this.adjustTickIntPage);
            this.tabControl1.Controls.Add(this.getDeltaPage);
            this.tabControl1.Controls.Add(this.getDeltaMultiPage);
            this.tabControl1.Location = new System.Drawing.Point(179, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(408, 226);
            this.tabControl1.TabIndex = 6;
            // 
            // getTickRatePage
            // 
            this.getTickRatePage.Controls.Add(this.label3);
            this.getTickRatePage.Controls.Add(this.getRateMaster);
            this.getTickRatePage.Controls.Add(this.getRateReset);
            this.getTickRatePage.Location = new System.Drawing.Point(4, 22);
            this.getTickRatePage.Name = "getTickRatePage";
            this.getTickRatePage.Padding = new System.Windows.Forms.Padding(3);
            this.getTickRatePage.Size = new System.Drawing.Size(400, 200);
            this.getTickRatePage.TabIndex = 0;
            this.getTickRatePage.Text = "Get_Tick_Rate";
            this.getTickRatePage.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Change All";
            // 
            // getRateMaster
            // 
            this.getRateMaster.Location = new System.Drawing.Point(87, 173);
            this.getRateMaster.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.getRateMaster.Name = "getRateMaster";
            this.getRateMaster.Size = new System.Drawing.Size(72, 20);
            this.getRateMaster.TabIndex = 1;
            this.getRateMaster.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.getRateMaster.ValueChanged += new System.EventHandler(this.getRateMaster_ValueChanged);
            // 
            // getRateReset
            // 
            this.getRateReset.Location = new System.Drawing.Point(6, 171);
            this.getRateReset.Name = "getRateReset";
            this.getRateReset.Size = new System.Drawing.Size(75, 23);
            this.getRateReset.TabIndex = 0;
            this.getRateReset.Text = "Reset All (60)";
            this.getRateReset.UseVisualStyleBackColor = true;
            this.getRateReset.Click += new System.EventHandler(this.getRateReset_Click);
            // 
            // adjustTickIntPage
            // 
            this.adjustTickIntPage.Controls.Add(this.label4);
            this.adjustTickIntPage.Controls.Add(this.adjustIntMaster);
            this.adjustTickIntPage.Controls.Add(this.adjustIntReset);
            this.adjustTickIntPage.Location = new System.Drawing.Point(4, 22);
            this.adjustTickIntPage.Name = "adjustTickIntPage";
            this.adjustTickIntPage.Padding = new System.Windows.Forms.Padding(3);
            this.adjustTickIntPage.Size = new System.Drawing.Size(400, 200);
            this.adjustTickIntPage.TabIndex = 1;
            this.adjustTickIntPage.Text = "adjust_int";
            this.adjustTickIntPage.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Change All";
            // 
            // adjustIntMaster
            // 
            this.adjustIntMaster.Location = new System.Drawing.Point(87, 173);
            this.adjustIntMaster.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.adjustIntMaster.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.adjustIntMaster.Name = "adjustIntMaster";
            this.adjustIntMaster.Size = new System.Drawing.Size(72, 20);
            this.adjustIntMaster.TabIndex = 4;
            this.adjustIntMaster.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.adjustIntMaster.ValueChanged += new System.EventHandler(this.adjustIntMaster_ValueChanged);
            // 
            // adjustIntReset
            // 
            this.adjustIntReset.Location = new System.Drawing.Point(6, 171);
            this.adjustIntReset.Name = "adjustIntReset";
            this.adjustIntReset.Size = new System.Drawing.Size(75, 23);
            this.adjustIntReset.TabIndex = 3;
            this.adjustIntReset.Text = "Reset All (60)";
            this.adjustIntReset.UseVisualStyleBackColor = true;
            this.adjustIntReset.Click += new System.EventHandler(this.adjustIntReset_Click);
            // 
            // getDeltaPage
            // 
            this.getDeltaPage.Controls.Add(this.getDeltaDeactivate);
            this.getDeltaPage.Controls.Add(this.label2);
            this.getDeltaPage.Controls.Add(this.deltaTick);
            this.getDeltaPage.Location = new System.Drawing.Point(4, 22);
            this.getDeltaPage.Name = "getDeltaPage";
            this.getDeltaPage.Padding = new System.Windows.Forms.Padding(3);
            this.getDeltaPage.Size = new System.Drawing.Size(400, 200);
            this.getDeltaPage.TabIndex = 2;
            this.getDeltaPage.Text = "Get_Tick_Delta";
            this.getDeltaPage.UseVisualStyleBackColor = true;
            // 
            // getDeltaDeactivate
            // 
            this.getDeltaDeactivate.Location = new System.Drawing.Point(64, 171);
            this.getDeltaDeactivate.Name = "getDeltaDeactivate";
            this.getDeltaDeactivate.Size = new System.Drawing.Size(99, 23);
            this.getDeltaDeactivate.TabIndex = 10;
            this.getDeltaDeactivate.Text = "Deactivate All";
            this.getDeltaDeactivate.UseVisualStyleBackColor = true;
            this.getDeltaDeactivate.Click += new System.EventHandler(this.getDeltaDeactivate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Delta Override (1 second / Tickrate)";
            // 
            // deltaTick
            // 
            this.deltaTick.Location = new System.Drawing.Point(353, 174);
            this.deltaTick.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.deltaTick.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.deltaTick.Name = "deltaTick";
            this.deltaTick.Size = new System.Drawing.Size(41, 20);
            this.deltaTick.TabIndex = 8;
            this.deltaTick.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.deltaTick.ValueChanged += new System.EventHandler(this.deltaTick_ValueChanged);
            // 
            // getDeltaMultiPage
            // 
            this.getDeltaMultiPage.Location = new System.Drawing.Point(4, 22);
            this.getDeltaMultiPage.Name = "getDeltaMultiPage";
            this.getDeltaMultiPage.Padding = new System.Windows.Forms.Padding(3);
            this.getDeltaMultiPage.Size = new System.Drawing.Size(400, 200);
            this.getDeltaMultiPage.TabIndex = 3;
            this.getDeltaMultiPage.Text = "Get_Delta_Multi";
            this.getDeltaMultiPage.UseVisualStyleBackColor = true;
            // 
            // isInterpolating
            // 
            this.isInterpolating.AutoSize = true;
            this.isInterpolating.Enabled = false;
            this.isInterpolating.Location = new System.Drawing.Point(12, 117);
            this.isInterpolating.Name = "isInterpolating";
            this.isInterpolating.Size = new System.Drawing.Size(101, 17);
            this.isInterpolating.TabIndex = 7;
            this.isInterpolating.Text = "Is Interpolating?";
            this.isInterpolating.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.descriptorLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 240);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(594, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // descriptorLabel
            // 
            this.descriptorLabel.Name = "descriptorLabel";
            this.descriptorLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // advancedTip
            // 
            this.advancedTip.AutomaticDelay = 100;
            this.advancedTip.AutoPopDelay = 10000;
            this.advancedTip.InitialDelay = 100;
            this.advancedTip.IsBalloon = true;
            this.advancedTip.ReshowDelay = 20;
            this.advancedTip.UseAnimation = false;
            this.advancedTip.UseFading = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 262);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.isInterpolating);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.userObjectAddress);
            this.Controls.Add(this.advancedButton);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.TickRateNum);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "It\'s all so relative";
            ((System.ComponentModel.ISupportInitialize)(this.TickRateNum)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.getTickRatePage.ResumeLayout(false);
            this.getTickRatePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.getRateMaster)).EndInit();
            this.adjustTickIntPage.ResumeLayout(false);
            this.adjustTickIntPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adjustIntMaster)).EndInit();
            this.getDeltaPage.ResumeLayout(false);
            this.getDeltaPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deltaTick)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage getTickRatePage;
        private System.Windows.Forms.TabPage adjustTickIntPage;
        private System.Windows.Forms.CheckBox isInterpolating;
        private System.Windows.Forms.TabPage getDeltaPage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown deltaTick;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel descriptorLabel;
        private System.Windows.Forms.Button getDeltaDeactivate;
        private System.Windows.Forms.Button getRateReset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown getRateMaster;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown adjustIntMaster;
        private System.Windows.Forms.Button adjustIntReset;
        private System.Windows.Forms.ToolTip advancedTip;
        private System.Windows.Forms.TabPage getDeltaMultiPage;
    }
}

