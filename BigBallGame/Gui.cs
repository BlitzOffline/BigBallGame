using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace BigBallGame
{
    public partial class Gui : Form
    {
        private Simulation.Simulation _simulation;
        
        public Gui()
        {
            this.InitializeComponent();
        }

        // We use the paint event over CreateGraphics() to avoid flickering.
        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (this._simulation == null) return;
            var distance = 0f;

            foreach (var ball in this._simulation.GetBalls())
            {
                ball.Draw(e.Graphics);
                distance += ball.Center.GetDistance(ball.Center.Add(ball.Velocity));
            }
            
            if (this._simulation.Debug) e.Graphics.DrawString(
                "Distance to be traveled next tick: " + distance,
                SystemFonts.DefaultFont,
                Brushes.Black,
                130,
                0);
        }

        private void OnSimulationStartButtonClick(object sender, EventArgs e)
        {
            var successful = int.TryParse(this.textBox1.Text, out var regularBallsAmount);

            if (!successful || regularBallsAmount < 1)
            {
                MessageBox.Show("Regular balls amount must be a positive number.");
                return;
            }
            
            successful = int.TryParse(this.textBox2.Text, out var repellentBallsAmount);
            
            if (!successful || repellentBallsAmount < 1)
            {
                MessageBox.Show("Repellent balls amount must be a positive number.");
                return;
            }
            
            successful = int.TryParse(this.textBox3.Text, out var monsterBallsAmount);
            
            if (!successful || monsterBallsAmount < 1)
            {
                MessageBox.Show("Monster balls amount must be a positive number.");
                return;
            }
            
            successful = int.TryParse(this.textBox4.Text, out var minBallRadius);
            
            if (!successful || minBallRadius < 1)
            {
                MessageBox.Show("Minimum ball radius must be a positive number.");
                return;
            }
            
            successful = int.TryParse(this.textBox5.Text, out var maxBallRadius);

            if (!successful || maxBallRadius <= minBallRadius)
            {
                MessageBox.Show("Maximum ball radius must be greater than minimum ball radius.");
                return;
            }
            
            successful = int.TryParse(this.textBox6.Text, out var tickTime);

            if (!successful || tickTime < 20)
            {
                MessageBox.Show("Tick Time must be a number equal to or greater than 20!");
                return;
            }

            this._simulation = new Simulation.Simulation(this)
            {
                AutomateTicking = this.automateTickingCheckBox.Checked,
                Debug = this.debugModeCheckBox.Checked,
                
                TickTime = tickTime,
                
                RegularBallsAmount = regularBallsAmount,
                RepellentBallsAmount = repellentBallsAmount,
                MonsterBallsAmount = monsterBallsAmount,
                
                MinBallRadius = minBallRadius,
                MaxBallRadius = maxBallRadius + 1
            };

            this.simulationStartButton.Hide();
            this.debugModeCheckBox.Hide();
            this.automateTickingCheckBox.Hide();
            this.settingsGroupBox.Hide();
            this.label7.Hide();

            if (this._simulation.Debug) AllocConsole();
            if (this._simulation.AutomateTicking)
            {
                this.simulationTickButtom.Hide();
                
                // We start the simulation on a separate thread to avoid blocking the GUI. We don't do this for the
                // manual ticking mode as we need to be able to control the simulation from the main thread.
                var thread = new Thread(this._simulation.StartSimulation);
                thread.Start();
                return;
            }
            
            this._simulation.Render();
        }
        
        private void OnSimulationTickButtonClick(object sender, EventArgs e)
        {
            this._simulation?.TickSimulation();
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();
        
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr h, int attr, int[] attrValue, int attrSize);
        
        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(this.Handle, 19, new[] {1}, 4) != 0)
                DwmSetWindowAttribute(this.Handle, 20, new[] {1}, 4);
        }
    }
}