using System.Drawing;
using BigBallGame.Simulation;
using BigBallGame.Vector;

namespace BigBallGame.Ball;

public class MonsterBall : Ball
{
    public MonsterBall(int radius, Vector2D center, Border border, Simulation.Simulation simulation)
        : base(radius, center, Color.Black, new Vector2D(0, 0), border, simulation)
    {
    }

    public override void Move()
    {
        // Monster balls do not move.
        return;
    }

    public override bool CollideWith(IBall other)
    {
        // Monster balls do not move so they can't collide into other balls.
        return false;
    }
}