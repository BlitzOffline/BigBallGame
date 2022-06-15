using System.Drawing;
using BigBallGame.Ball;

namespace BigBallGame.Drawing
{
    public static class GraphicsHelper
    {
        public static void DrawBall(this Graphics g, IBall ball)
        {
            g.DrawCircle(new Pen(ball.Color), ball.Center.ToPointF(), ball.Radius);
        }
        
        public static void DrawCircle(this Graphics g, Pen pen, PointF center, float radius)
        {
            g.DrawCircle(pen, center.X, center.Y , radius);
        }
        
        public static void DrawCircle(this Graphics g, Pen pen, float x, float y, float radius)
        {
            g.DrawEllipse(pen, x - radius, y - radius, radius * 2, radius * 2);
            g.FillEllipse(pen.Brush, x - radius, y - radius, radius * 2, radius * 2);
        }
    }
}