using System.Drawing;
using BigBallGame.Vector;

namespace BigBallGame.Ball;

public interface IBall
{
    float Radius { get; set; }
    Vector2D Center { get; set; }
    Vector2D Velocity { get; set; }
    Color Color { get; set; }

    void Draw(Graphics graphics);
    void DrawShadow(Graphics graphics);
    void Move();

    /**
         * Returns true if the ball should be removed as it colided into another ball.
         */
    bool CollideWith(IBall other);
    bool CollidesWith(IBall other);
}