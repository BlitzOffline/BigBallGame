using System.Drawing;
using BigBallGame.Simulation;
using BigBallGame.Vector;

namespace BigBallGame.Ball
{
    public class RegularBall : Ball
    {
        public RegularBall(
            int radius,
            Vector2D center,
            Color color,
            Vector2D velocity,
            Border border,
            Simulation.Simulation simulation) : base(radius, center, color, velocity, border, simulation)
        {
        }
    }
}