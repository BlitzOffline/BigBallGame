using System;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace BigBallGame
{
    public partial class Gui : Form
    {
        private Simulation.Simulation _simulation;
        private Thread _simulationThread;
        
        public Gui()
        {
            this.InitializeComponent();
        }

        // We use the paint event over CreateGraphics() to avoid flickering.
        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (this._simulation == null) return;
            var distance = 0f;

            var balls = this._simulation.GetBalls().ToArray();

            foreach (var ball in balls)
            {
                ball.DrawShadow(e.Graphics);
            }

            foreach (var ball in balls)
            {
                ball.Draw(e.Graphics);
                distance += ball.Center.GetDistance(ball.Center.Add(ball.Velocity));
            }

            this.label7.Text = "FPS: " + (int) this._simulation.GetAverageFps() + 
                               Environment.NewLine +
                               "Units To Be Traveled: " + (int) distance;
        }

        private void OnSimulationStartButtonClick(object sender, EventArgs e)
        {
            var successful = int.TryParse(this.textBox1.Text, out var regularBallsAmount);

            if (!successful || regularBallsAmount < 0 || (regularBallsAmount < 1 && !this.debugModeCheckBox.Checked))
            {
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Regular balls amount must be a positive number unless debug mode is enabled.");
                return;
            }
            
            successful = int.TryParse(this.textBox2.Text, out var repellentBallsAmount);
            
            if (!successful || repellentBallsAmount < 0 || (repellentBallsAmount < 1 && !this.debugModeCheckBox.Checked))
            {
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Repellent balls amount must be a positive number unless debug mode is enabled.");
                return;
            }
            
            successful = int.TryParse(this.textBox3.Text, out var monsterBallsAmount);
            
            if (!successful || monsterBallsAmount < 0 || (monsterBallsAmount < 1 && !this.debugModeCheckBox.Checked))
            {
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Monster balls amount must be a positive number unless debug mode is enabled.");
                return;
            }
            
            successful = int.TryParse(this.textBox4.Text, out var minBallRadius);
            
            if (!successful || minBallRadius < 1)
            {
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Minimum ball radius must be a positive number.");
                return;
            }
            
            successful = int.TryParse(this.textBox5.Text, out var maxBallRadius);

            if (!successful || maxBallRadius <= minBallRadius)
            {
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Maximum ball radius must be greater than minimum ball radius.");
                return;
            }
            
            successful = int.TryParse(this.textBox6.Text, out var tickTime);

            if (!successful || tickTime < 1 || (tickTime < 20 && !this.debugModeCheckBox.Checked))
            {
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Tick Time must be a number equal to or greater than 20 unless debug mode is enabled.");
                return;
            }

            this._simulation = new Simulation.Simulation(
                this,
                
                this.debugModeCheckBox.Checked,
                this.showDirectionsCheckBox.Checked,
                this.automateTickingCheckBox.Checked,
                
                tickTime,
                
                regularBallsAmount,
                repellentBallsAmount,
                monsterBallsAmount,
                
                minBallRadius,
                maxBallRadius + 1);

            this.simulationStartButton.Hide();
            this.debugModeCheckBox.Hide();
            this.automateTickingCheckBox.Hide();
            this.enableConsoleCheckBox.Hide();
            this.settingsGroupBox.Hide();
            this.moreSettingsGroupBox.Hide();

            if (this._simulation.Debug && this.enableConsoleCheckBox.Checked) AllocConsole();
            if (this._simulation.Debug) this.label7.Show();
            if (this._simulation.AutomateTicking)
            {
                this.simulationTickButtom.Hide();
                
                // We start the simulation on a separate thread to avoid blocking the GUI. We don't do this for the
                // manual ticking mode as we need to be able to control the simulation from the main thread.
                _simulationThread = new Thread(this._simulation.StartSimulation);
                _simulationThread.Start();
                return;
            }
            
            this._simulation.Render();
        }
        
        private void OnSimulationTickButtonClick(object sender, EventArgs e)
        {
            if (_simulation == null)
            {
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Simulation has not started yet.");
                return;
            }
            
            this._simulation.TickSimulation();
        }

        private void OnDebugModeCheckBoxClick(object sender, EventArgs e)
        {
            if (this.debugModeCheckBox.Checked)
            {
                this.label7.Show();
                return;
            }
            
            this.label7.Hide();
            this.enableConsoleCheckBox.Checked = false;
            this.showDirectionsCheckBox.Checked = false;
        }
        
        private void OnEnableConsoleCheckBoxClick(object sender, EventArgs e)
        {
            if (this.debugModeCheckBox.Checked) return;
            
            SystemSounds.Exclamation.Play();
            this.enableConsoleCheckBox.Checked = false;
            MessageBox.Show("Console can only be enabled in debug mode.");
        }

        private void OnShowDirectionCheckBoxClick(object sender, EventArgs e)
        {
            if (this.debugModeCheckBox.Checked) return;
            
            SystemSounds.Exclamation.Play();
            this.showDirectionsCheckBox.Checked = false;
            MessageBox.Show("Direction can only be shown in debug mode.");
        }

        private void OnClose(object sender, FormClosedEventArgs e)
        {
            if (_simulationThread == null) return;
            
            _simulationThread.Abort();
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