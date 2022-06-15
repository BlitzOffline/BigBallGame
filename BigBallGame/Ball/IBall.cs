using System.Drawing;

namespace BigBallGame.Ball
{
    public interface IBall
    {
        float Radius { get; set; }
        Vector2D Center { get; set; }
        Vector2D Velocity { get; set; }
        Color Color { get; set; }
        
        Vector2D PreviousCenter { get; set; }
        Vector2D PreviousVelocity { get; set; }

        void Draw(Graphics g);
        void Move();

        /**
         * Returns true if the ball should be removed as it colided into another ball.
         */
        bool CollideWith(IBall other);
        bool CollidesWith(IBall other);
    }
}