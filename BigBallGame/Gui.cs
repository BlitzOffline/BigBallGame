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
        private bool _debug;
        private bool _automateTicking = true;
        
        private int _tickTime = 20;
    
        private int _regularBallsAmount = 10;
        private int _repellentBallsAmount = 5;
        private int _monsterBallsAmount = 1;

        private int _minBallRadius = 15;
        private int _maxBallRadius = 35;

        public Gui()
        {
            InitializeComponent();
        }

        // We use the paint event over CreateGraphics() to avoid flickering.
        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (_simulation == null) return;
            var distance = 0f;

            foreach (var ball in _simulation.GetBalls())
            {
                ball.Draw(e.Graphics);
                distance += ball.Center.GetDistance(ball.Center.Add(ball.Velocity));
            }
            
            if (_simulation.Debug) e.Graphics.DrawString(
                "Distance to be traveled next tick: " + distance,
                SystemFonts.DefaultFont,
                Brushes.Black,
                    130,
                    0);
        }

        private void OnSimulationStartButtonClick(object sender, EventArgs e)
        {
            _simulation = new Simulation.Simulation(this)
            {
                AutomateTicking = _automateTicking,
                Debug = _debug,
                
                TickTime = _tickTime,
                
                RegularBallsAmount = _regularBallsAmount,
                RepellentBallsAmount = _repellentBallsAmount,
                MonsterBallsAmount = _monsterBallsAmount,
                
                MinBallRadius = _minBallRadius,
                MaxBallRadius = _maxBallRadius + 1
            };

            simulationStartButton.Hide();
            debugModeCheckBox.Hide();
            automateTickingCheckBox.Hide();
            settingsGroupBox.Hide();
            label7.Hide();

            if (_simulation.Debug) AllocConsole();
            if (_simulation.AutomateTicking)
            {
                simulationTickButtom.Hide();
                var thread = new Thread(_simulation.StartSimulation);
                thread.Start();
                return;
            }
            
            _simulation.Render();
        }
        
        private void OnSimulationTickButtonClick(object sender, EventArgs e)
        {
            _simulation?.TickSimulation();
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr h, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        private void OnDebugModeCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            _debug = !_debug;
        }

        private void OnAutomateTickingCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            _automateTicking = !_automateTicking;
        }

        private void OnRegularBallsAmountChange(object sender, EventArgs e)
        {
            var successful = int.TryParse(textBox1.Text, out var value);

            if (successful && value > 0)
            {
                _regularBallsAmount = value;
                label7.Hide();
                return;
            }
            
            label7.Text = "Regular Ball Amount must be a positive number!";
            label7.Show();
            textBox1.Text = _regularBallsAmount.ToString();
        }
        
        private void OnRepellentBallsAmountChange(object sender, EventArgs e)
        {
            var successful = int.TryParse(textBox2.Text, out var value);

            if (successful && value > 0)
            {
                _repellentBallsAmount = value;
                label7.Hide();
                return;
            }
            
            label7.Text = "Repellent Ball Amount must be a positive number!";
            label7.Show();
            textBox2.Text = _repellentBallsAmount.ToString();
        }
        
        private void OnMonsterBallsAmountChange(object sender, EventArgs e)
        {
            var successful = int.TryParse(textBox3.Text, out var value);

            if (successful && value > 0)
            {
                _monsterBallsAmount = value;
                label7.Hide();
                return;
            }
            
            label7.Text = "Monster Ball Amount must be a positive number!";
            label7.Show();
            textBox3.Text = _monsterBallsAmount.ToString();
        }

        private void OnBallMinRadiusChange(object sender, EventArgs e)
        {
            var successful = int.TryParse(textBox4.Text, out var value);

            if (successful && value > 0)
            {
                _minBallRadius = value;
                label7.Hide();
                return;
            }
            
            label7.Text = "Minimum Ball Radius must be a positive number!";
            label7.Show();
            textBox4.Text = _minBallRadius.ToString();
        }
        
        private void OnBallMaxRadiusChange(object sender, EventArgs e)
        {
            var successful = int.TryParse(textBox5.Text, out var value);

            if (successful && value > _minBallRadius)
            {
                _maxBallRadius = value;
                label7.Hide();
                return;
            }
            
            label7.Text = "Maximum Ball Radius must be a positive number, greater than Minimum Ball Radius!";
            label7.Show();
            textBox5.Text = _maxBallRadius.ToString();
        }
        
        private void OnTickTimeChange(object sender, EventArgs e)
        {
            var successful = int.TryParse(textBox6.Text, out var value);

            if (successful && value >= 20)
            {
                _tickTime = value;
                label7.Hide();
                return;
            }
            
            label7.Text = "Tick Time must be a number equals or greater than 20!";
            label7.Show();
            textBox6.Text = _tickTime.ToString();
        }
    }
}