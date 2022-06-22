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
        private Vector2D PreviousCenter { get; set; }
        private Vector2D PreviousVelocity { get; set; }

        protected Ball(
            int radius,
            Vector2D center,
            Color color,
            Vector2D velocity,
            Border border,
            Simulation.Simulation simulation)
        {
            this.Radius = radius;
            this.Center = center;
            this.Color = color;
            this.Velocity = velocity;
            
            this.PreviousCenter = this.Center;
            this.PreviousVelocity = this.Velocity;
            
            this._border = border;
            this._simulation = simulation;
        }
        
        public virtual void Draw(Graphics graphics)
        {
            graphics.DrawCircle(Pens.DimGray, this.Center.Add(new Vector2D(5, 5)).ToPointF(), this.Radius);
            graphics.DrawBall(this);
            if (!this._simulation.Debug || !this._simulation.ShowDirections) return;
            
            this.DrawLastTrajectory(graphics);
            this.DrawNextTrajectory(graphics);
        }

        public virtual void Move()
        {
            this.PreviousCenter = this.Center;
            this.PreviousVelocity = this.Velocity;
            
            this.Center = this.Center.Add(this.Velocity);

            if (this.Center.X - this.Radius < this._border.MinX)
            {
                this.Velocity.X = -this.Velocity.X;
                this.Center = new Vector2D(
                    this._border.MinX + Math.Abs(this.Center.X - this._border.MinX) + this.Radius,
                    this.Center.Y);
            }

            if (this.Center.X + this.Radius > this._border.MaxX)
            {
                this.Velocity.X = -this.Velocity.X;
                this.Center = new Vector2D(
                    this._border.MaxX - Math.Abs(this.Center.X - this._border.MaxX) - this.Radius,
                    this.Center.Y);
            }
            
            if (this.Center.Y - this.Radius < this._border.MinY)
            {
                this.Velocity.Y = -this.Velocity.Y;
                this.Center = new Vector2D(
                    this.Center.X,
                    this._border.MinY + Math.Abs(this.Center.Y - this._border.MinY) + this.Radius);
            }
            
            if (this.Center.Y + this.Radius > this._border.MaxY)
            {
                this.Velocity.Y = -this.Velocity.Y;
                this.Center = new Vector2D(
                    this.Center.X,
                    this._border.MaxY - Math.Abs(this.Center.Y - this._border.MaxY) - this.Radius);
            }
        }

        public virtual bool CollideWith(IBall other)
        {
            if (!this.CollidesWith(other))
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
                mtd = delta.Multiply((this.Radius + other.Radius-d)/d);
            }
            else 
            {
                d = other.Radius + this.Radius - 1.0f;
                delta = new Vector2D(other.Radius + this.Radius, 0.0f);
        
                mtd = delta.Multiply((this.Radius + other.Radius-d)/d);
            }
                    
            this.Center = this.Center.Add(mtd.Multiply(0.5f));
            other.Center = other.Center.Subtract(mtd.Multiply(0.5f));
        
            // impact speed
            var v = this.Velocity.Subtract(other.Velocity);
            mtd = mtd.Normalize();
            var vn = v.Dot(mtd);
                    
            if (vn > 0.0f) return;
        
            this.Velocity = this.Velocity.Add(mtd.Multiply(-vn));
            other.Velocity = other.Velocity.Subtract(mtd.Multiply(-vn));
        }


        private void DrawLastTrajectory(Graphics graphics)
        {
            var color = Color.FromArgb(
                this.Color.A,
                255 - this.Color.R,
                255 - this.Color.G,
                255 - this.Color.B);

            // DISPLAY PREVIOUS POSITION
            graphics.DrawLine(
                new Pen(color, 1),
                this.PreviousCenter.ToPointF(),
                this.PreviousCenter.Add(this.PreviousVelocity).ToPointF());
            
            graphics.DrawLine(
                new Pen(color, 1),
                this.PreviousCenter.Add(this.PreviousVelocity).ToPointF(),
                this.Center.ToPointF());
        }
        
        private void DrawNextTrajectory(Graphics graphics)
        {
            // PREDICT NEXT POSITION
            var nextCenter = this.PredictNextPosition();
            graphics.DrawLine(BlackPen, this.Center.ToPointF(), this.Center.Add(this.Velocity).ToPointF());
            graphics.DrawLine(BlackPen, this.Center.Add(this.Velocity).ToPointF(), nextCenter.ToPointF());
        }
        
        private Vector2D PredictNextPosition()
        {
            var newCenter = this.Center.Add(this.Velocity);
            
            if (newCenter.X - this.Radius < this._border.MinX)
            {
                newCenter = new Vector2D(
                    this._border.MinX + Math.Abs(newCenter.X - this._border.MinX) + this.Radius,
                    newCenter.Y);
            }

            if (newCenter.X + this.Radius > this._border.MaxX)
            {
                newCenter = new Vector2D(
                    this._border.MaxX - Math.Abs(newCenter.X - this._border.MaxX) - this.Radius,
                    newCenter.Y);
            }

            if (newCenter.Y - this.Radius < this._border.MinY)
            {
                newCenter = new Vector2D(
                    newCenter.X,
                    this._border.MinY + Math.Abs(newCenter.Y - this._border.MinY) + this.Radius);
            }

            if (newCenter.Y + this.Radius > this._border.MaxY)
            {
                newCenter = new Vector2D(
                    newCenter.X,
                    this._border.MaxY - Math.Abs(newCenter.Y - this._border.MaxY) - this.Radius);
            }

            return newCenter;
        }
    }
}