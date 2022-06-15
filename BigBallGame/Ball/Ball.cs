using System;
using System.Drawing;
using BigBallGame.Drawing;
using BigBallGame.Simulation;

namespace BigBallGame.Ball
{
    public class Ball : IBall
    {
        private static readonly Pen BlackPen = new(Color.Black, 1);
        
        private readonly Border _border;
        private readonly Simulation.Simulation _simulation;

        public float Radius { get; set; }
        public Vector2D Center { get; set; }
        public Vector2D Velocity { get; set; }
        public Color Color { get; set; }
        
        // Previous Center and Velocity are saved for debugging purposes
        public Vector2D PreviousCenter { get; set; }
        public Vector2D PreviousVelocity { get; set; }

        protected Ball(int radius, Vector2D center, Color color, Vector2D velocity, Border border, Simulation.Simulation simulation)
        {
            Radius = radius;
            Center = center;
            Color = color;
            Velocity = velocity;
            
            PreviousCenter = Center;
            PreviousVelocity = Velocity;
            
            _border = border;
            _simulation = simulation;
        }
        
        public virtual void Draw(Graphics g)
        {
            g.DrawBall(this);
            if (!_simulation.Debug) return;
            
            DrawLastTrajectory(g);
            DrawNextTrajectory(g);
        }

        public virtual void Move()
        {
            PreviousCenter = Center;
            PreviousVelocity = Velocity;
            
            Center = Center.Add(Velocity);

            if (Center.X - Radius < _border.MinX)
            {
                Velocity.X = -Velocity.X;
                Center = new Vector2D(_border.MinX + Math.Abs(Center.X - _border.MinX) + Radius, Center.Y);
            }

            if (Center.X + Radius > _border.MaxX)
            {
                Velocity.X = -Velocity.X;
                Center = new Vector2D(_border.MaxX - Math.Abs(Center.X - _border.MaxX) - Radius, Center.Y);
            }
            
            if (Center.Y - Radius < _border.MinY)
            {
                Velocity.Y = -Velocity.Y;
                Center = new Vector2D(Center.X, _border.MinY + Math.Abs(Center.Y - _border.MinY) + Radius);
            }
            
            if (Center.Y + Radius > _border.MaxY)
            {
                Velocity.Y = -Velocity.Y;
                Center = new Vector2D(Center.X, _border.MaxY - Math.Abs(Center.Y - _border.MaxY) - Radius);
            }
        }

        public virtual bool CollideWith(IBall other)
        {
            if (!CollidesWith(other))
            {
                return false;
            }

            switch (other)
            {
                case RegularBall when other.Radius >= this.Radius:
                {
                    var totalRadius = other.Radius + this.Radius;
                    var otherPercent = other.Radius / 100 * totalRadius;

                    var otherRatio = otherPercent / 100;
                    var thisRatio = 1.0f - otherRatio;

                    other.Color = Color.FromArgb(
                        (this.Color.A + other.Color.A) / 2,
                        (int) Math.Floor((otherRatio * other.Color.R + thisRatio * this.Color.R) % 256),
                        (int) Math.Floor((otherRatio * other.Color.G + thisRatio * this.Color.G) % 256),
                        (int) Math.Floor((otherRatio * other.Color.B + thisRatio * this.Color.B) % 256)
                    );

                    other.Radius = (float) Math.Sqrt((Math.PI * this.Radius * this.Radius + 
                                                      Math.PI * other.Radius * other.Radius) / Math.PI);
                    return true;
                }
                case MonsterBall:
                    other.Radius = (float) Math.Sqrt((Math.PI * this.Radius * this.Radius + 
                                                       Math.PI * other.Radius * other.Radius) / Math.PI);
                    return true;
                case RepellentBall:
                    return false;
            }

            return false;
        }
        
        public virtual bool CollidesWith(IBall other)
        {
            var distanceSquared = (this.Center.X - other.Center.X) * (this.Center.X - other.Center.X) +
                                  (this.Center.Y - other.Center.Y) * (this.Center.Y - other.Center.Y);

            return Math.Abs(distanceSquared) <= (this.Radius + other.Radius) * (this.Radius + other.Radius);
        }

        protected void HandleRepellentBallCollision(IBall other)
        {
            if (this is not RepellentBall && other is not RepellentBall) return;
            
            var delta = this.Center.Subtract(other.Center);
            var radiusSum = this.Radius + other.Radius;
            var distanceSquared = delta.Dot(delta);
        
            // The balls are not coliding
            if (distanceSquared > radiusSum * radiusSum) return;
        
            var d = delta.Length;
        
            Vector2D mtd;
            if (d != 0.0f)
            {
                mtd = delta.Multiply((Radius + other.Radius-d)/d);
            }
            else 
            {
                d = other.Radius + Radius - 1.0f;
                delta = new Vector2D(other.Radius + Radius, 0.0f);
        
                mtd = delta.Multiply((Radius + other.Radius-d)/d);
            }
                    
            Center = Center.Add(mtd.Multiply(0.5f));
            other.Center = other.Center.Subtract(mtd.Multiply(0.5f));
        
            // impact speed
            var v = this.Velocity.Subtract(other.Velocity);
            var vn = v.Dot(mtd.Normalize());
                    
            if (vn > 0.0f) return;
        
            this.Velocity = this.Velocity.Add(mtd.Multiply(-vn));
            other.Velocity = other.Velocity.Subtract(mtd.Multiply(-vn));
        }


        private void DrawLastTrajectory(Graphics g)
        {
            var color = Color.FromArgb(this.Color.A, 255 - this.Color.R, 255 - this.Color.G, 255 - this.Color.B);

            // DISPLAY PREVIOUS POSITION
            g.DrawLine(new Pen(color, 1), this.PreviousCenter.ToPointF(), this.PreviousCenter.Add(this.PreviousVelocity).ToPointF());
            g.DrawLine(new Pen(color, 1), this.PreviousCenter.Add(this.PreviousVelocity).ToPointF(), Center.ToPointF());
        }
        
        private void DrawNextTrajectory(Graphics g)
        {
            // PREDICT NEXT POSITION
            var nextCenter = PredictNextPosition();
            g.DrawLine(BlackPen, this.Center.ToPointF(), this.Center.Add(this.Velocity).ToPointF());
            g.DrawLine(BlackPen, this.Center.Add(this.Velocity).ToPointF(), nextCenter.ToPointF());
        }
        
        private Vector2D PredictNextPosition()
        {
            var newCenter = this.Center.Add(this.Velocity);
            
            if (newCenter.X - Radius < _border.MinX)
            {
                newCenter = new Vector2D(_border.MinX + Math.Abs(newCenter.X - _border.MinX) + Radius, newCenter.Y);
            }

            if (newCenter.X + Radius > _border.MaxX)
            {
                newCenter = new Vector2D(_border.MaxX - Math.Abs(newCenter.X - _border.MaxX) - Radius, newCenter.Y);
            }

            if (newCenter.Y - Radius < _border.MinY)
            {
                newCenter = new Vector2D(newCenter.X, _border.MinY + Math.Abs(newCenter.Y - _border.MinY) + Radius);
            }

            if (newCenter.Y + Radius > _border.MaxY)
            {
                newCenter = new Vector2D(newCenter.X, _border.MaxY - Math.Abs(newCenter.Y - _border.MaxY) - Radius);
            }

            return newCenter;
        }
    }
}