using System.Drawing;
using BigBallGame.Simulation;
using BigBallGame.Vector;

namespace BigBallGame.Ball;

public class RepellentBall : Ball
{
    public RepellentBall(
        int radius,
        Vector2D center,
        Color color,
        Vector2D velocity,
        Border border,
        Simulation.Simulation simulation) : base(radius, center, color, velocity, border, simulation)
    {
    }

    public override void Draw(Graphics graphics)
    {
        base.Draw(graphics);

        var color = Color.FromArgb(
            this.Color.A,
            255 - this.Color.R,
            this.Color.G, 
            255 - this.Color.B);
            
        graphics.DrawEllipse(
            new Pen(color, 5),
            this.Center.X - this.Radius,
            this.Center.Y - this.Radius,
            this.Radius * 2,
            this.Radius * 2);
    }
        
    public override bool CollideWith(IBall other)
    {
        switch (other)
        {
            case RegularBall regularBall:
                // Swap Colors
                (this.Color, other.Color) = (other.Color, this.Color);
                    
                this.HandleRepellentBallCollision(regularBall);
                return false;
            case MonsterBall monsterBall:
                this.HandleMonsterBallCollision(monsterBall);
                this.Radius /= 2;
                return this.Radius < 1f;
            case RepellentBall repellentBall:
                this.HandleRepellentBallCollision(repellentBall);
                return false;
        }
            
        return false;
    }

    protected void HandleMonsterBallCollision(MonsterBall other)
    {
        var delta = this.Center.Subtract(other.Center);
        var radiusSum = this.Radius + other.Radius;
        var distanceSquared = delta.Dot(delta);

        // The balls are not coliding
        if (distanceSquared > radiusSum * radiusSum) return;

        var d = delta.Length;

        Vector2D mtd;
        if (d != 0.0f)
        {
            mtd = delta.Multiply(this.Radius/d);
        }
        else 
        {
            d = other.Radius + this.Radius - 1.0f;
            delta = new Vector2D(other.Radius + this.Radius, 0.0f);

            mtd = delta.Multiply(this.Radius/d);
        }
                    
        this.Center = this.Center.Add(mtd);
        this.Velocity = this.Velocity.Add(mtd);
    }
}