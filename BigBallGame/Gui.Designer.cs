using System.Drawing;
using System.Windows.Forms;

namespace BigBallGame
{
    partial class Gui
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
            this.simulationStartButton = new System.Windows.Forms.Button();
            this.simulationTickButtom = new System.Windows.Forms.Button();
            this.debugModeCheckBox = new System.Windows.Forms.CheckBox();
            this.automateTickingCheckBox = new System.Windows.Forms.CheckBox();
            this.moreSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.showDirectionsCheckBox = new System.Windows.Forms.CheckBox();
            this.enableConsoleCheckBox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.moreSettingsGroupBox.SuspendLayout();
            this.settingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // simulationStartButton
            // 
            this.simulationStartButton.Location = new System.Drawing.Point(127, 12);
            this.simulationStartButton.Name = "simulationStartButton";
            this.simulationStartButton.Size = new System.Drawing.Size(109, 51);
            this.simulationStartButton.TabIndex = 0;
            this.simulationStartButton.Text = "Start Simulation";
            this.simulationStartButton.UseVisualStyleBackColor = true;
            this.simulationStartButton.Click += new System.EventHandler(this.OnSimulationStartButtonClick);
            // 
            // simulationTickButtom
            // 
            this.simulationTickButtom.Location = new System.Drawing.Point(12, 12);
            this.simulationTickButtom.Name = "simulationTickButtom";
            this.simulationTickButtom.Size = new System.Drawing.Size(109, 51);
            this.simulationTickButtom.TabIndex = 1;
            this.simulationTickButtom.TabStop = false;
            this.simulationTickButtom.Text = "Tick Simulation";
            this.simulationTickButtom.UseVisualStyleBackColor = true;
            this.simulationTickButtom.Click += new System.EventHandler(this.OnSimulationTickButtonClick);
            // 
            // debugModeCheckBox
            // 
            this.debugModeCheckBox.Location = new System.Drawing.Point(6, 21);
            this.debugModeCheckBox.Name = "debugModeCheckBox";
            this.debugModeCheckBox.Size = new System.Drawing.Size(108, 51);
            this.debugModeCheckBox.TabIndex = 2;
            this.debugModeCheckBox.Text = "Debug Mode";
            this.debugModeCheckBox.UseVisualStyleBackColor = true;
            this.debugModeCheckBox.Click += new System.EventHandler(this.OnDebugModeCheckBoxClick);
            // 
            // automateTickingCheckBox
            // 
            this.automateTickingCheckBox.Checked = true;
            this.automateTickingCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.automateTickingCheckBox.Location = new System.Drawing.Point(6, 78);
            this.automateTickingCheckBox.Name = "automateTickingCheckBox";
            this.automateTickingCheckBox.Size = new System.Drawing.Size(108, 51);
            this.automateTickingCheckBox.TabIndex = 3;
            this.automateTickingCheckBox.Text = "Automate Ticking";
            this.automateTickingCheckBox.UseVisualStyleBackColor = true;
            // 
            // moreSettingsGroupBox
            // 
            this.moreSettingsGroupBox.Controls.Add(this.label6);
            this.moreSettingsGroupBox.Controls.Add(this.textBox6);
            this.moreSettingsGroupBox.Controls.Add(this.label5);
            this.moreSettingsGroupBox.Controls.Add(this.textBox5);
            this.moreSettingsGroupBox.Controls.Add(this.label4);
            this.moreSettingsGroupBox.Controls.Add(this.textBox4);
            this.moreSettingsGroupBox.Controls.Add(this.label3);
            this.moreSettingsGroupBox.Controls.Add(this.textBox3);
            this.moreSettingsGroupBox.Controls.Add(this.label2);
            this.moreSettingsGroupBox.Controls.Add(this.textBox2);
            this.moreSettingsGroupBox.Controls.Add(this.label1);
            this.moreSettingsGroupBox.Controls.Add(this.textBox1);
            this.moreSettingsGroupBox.Location = new System.Drawing.Point(12, 215);
            this.moreSettingsGroupBox.Name = "moreSettingsGroupBox";
            this.moreSettingsGroupBox.Size = new System.Drawing.Size(353, 174);
            this.moreSettingsGroupBox.TabIndex = 4;
            this.moreSettingsGroupBox.TabStop = false;
            this.moreSettingsGroupBox.Text = "More Settings";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(217, 25);
            this.label6.TabIndex = 10;
            this.label6.Text = "Tick Length in ms (min 20):";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(239, 143);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(108, 22);
            this.textBox6.TabIndex = 17;
            this.textBox6.Text = "20";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(217, 25);
            this.label5.TabIndex = 9;
            this.label5.Text = "Maximum Ball Radius:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(239, 118);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(108, 22);
            this.textBox5.TabIndex = 16;
            this.textBox5.Text = "35";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(217, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Minimum Ball Radius:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(239, 93);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(108, 22);
            this.textBox4.TabIndex = 15;
            this.textBox4.Text = "15";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(217, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Monster Balls Amount:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(239, 68);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(108, 22);
            this.textBox3.TabIndex = 14;
            this.textBox3.Text = "1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Repellent Balls Amount:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(239, 43);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(108, 22);
            this.textBox2.TabIndex = 13;
            this.textBox2.Text = "5";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Regular Balls Amount:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(239, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(108, 22);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = "10";
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Controls.Add(this.showDirectionsCheckBox);
            this.settingsGroupBox.Controls.Add(this.enableConsoleCheckBox);
            this.settingsGroupBox.Controls.Add(this.debugModeCheckBox);
            this.settingsGroupBox.Controls.Add(this.automateTickingCheckBox);
            this.settingsGroupBox.Location = new System.Drawing.Point(12, 69);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Size = new System.Drawing.Size(353, 140);
            this.settingsGroupBox.TabIndex = 5;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "Settings";
            // 
            // showDirectionsCheckBox
            // 
            this.showDirectionsCheckBox.Location = new System.Drawing.Point(120, 78);
            this.showDirectionsCheckBox.Name = "showDirectionsCheckBox";
            this.showDirectionsCheckBox.Size = new System.Drawing.Size(108, 51);
            this.showDirectionsCheckBox.TabIndex = 5;
            this.showDirectionsCheckBox.Text = "Show Directions\r\n";
            this.showDirectionsCheckBox.UseVisualStyleBackColor = true;
            this.showDirectionsCheckBox.Click += new System.EventHandler(this.OnShowDirectionCheckBoxClick);
            // 
            // enableConsoleCheckBox
            // 
            this.enableConsoleCheckBox.Location = new System.Drawing.Point(120, 21);
            this.enableConsoleCheckBox.Name = "enableConsoleCheckBox";
            this.enableConsoleCheckBox.Size = new System.Drawing.Size(108, 51);
            this.enableConsoleCheckBox.TabIndex = 4;
            this.enableConsoleCheckBox.Text = "Enable Console";
            this.enableConsoleCheckBox.UseVisualStyleBackColor = true;
            this.enableConsoleCheckBox.Click += new System.EventHandler(this.OnEnableConsoleCheckBoxClick);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(1633, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(275, 50);
            this.label7.TabIndex = 6;
            this.label7.Text = "FPS: 0\r\nDistance Traveled Last Frame: 0";
            this.label7.Visible = false;
            // 
            // Gui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.settingsGroupBox);
            this.Controls.Add(this.moreSettingsGroupBox);
            this.Controls.Add(this.simulationTickButtom);
            this.Controls.Add(this.simulationStartButton);
            this.DoubleBuffered = true;
            this.Name = "Gui";
            this.Text = "Big Ball Game";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.moreSettingsGroupBox.ResumeLayout(false);
            this.moreSettingsGroupBox.PerformLayout();
            this.settingsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button simulationStartButton;
        private System.Windows.Forms.Button simulationTickButtom;
        
        private System.Windows.Forms.CheckBox debugModeCheckBox;
        private System.Windows.Forms.CheckBox automateTickingCheckBox;
        private System.Windows.Forms.CheckBox enableConsoleCheckBox;
        private System.Windows.Forms.CheckBox showDirectionsCheckBox;

        private System.Windows.Forms.GroupBox moreSettingsGroupBox;
        private System.Windows.Forms.GroupBox settingsGroupBox;

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;

        #endregion
    }
}