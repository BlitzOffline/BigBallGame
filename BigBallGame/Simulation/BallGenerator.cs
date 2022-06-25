using System;
using System.Collections.Generic;
using System.Drawing;
using BigBallGame.Ball;
using BigBallGame.Vector;

namespace BigBallGame.Simulation
{
    public class BallGenerator
    {
        private readonly Random _random;
        private readonly Simulation _simulation;
        
        public BallGenerator(Simulation simulation)
        {
            this._random = new Random();
            this._simulation = simulation;
        }
        
        public BallGenerator(int seed, Simulation simulation)
        {
            this._random = new Random(seed);
            this._simulation = simulation;
        }
        
        public BallGenerator(Random random, Simulation simulation)
        {
            this._random = random;
            this._simulation = simulation;
        }

        public Ball.Ball GenerateRegularBall(IReadOnlyList<IBall> balls = null)
        {
            if (balls == null || balls.Count == 0)
            {
                var radius = this._random.Next(this._simulation.MinBallRadius, this._simulation.MaxBallRadius);
                return new RegularBall(
                    radius,
                    new Vector2D(
                        this._random.Next(0 + radius, this._simulation.Gui.ClientSize.Width - radius),
                        this._random.Next(0 + radius, this._simulation.Gui.ClientSize.Height - radius)),
                    Color.FromArgb(
                        this._random.Next(0, 220),
                        this._random.Next(0, 220),
                        this._random.Next(0, 220)),
                    new Vector2D(this._random.Next(-radius + 1, radius), this._random.Next(-radius + 1, radius)), 
                    this._simulation.Border,
                    this._simulation
                );
            }
            
            // TODO: Generate a ball here but make sure it does not collide with any of the other balls!
            var ballRadius = this._random.Next(this._simulation.MinBallRadius, this._simulation.MaxBallRadius);
            return new RegularBall(
                ballRadius,
                new Vector2D(
                    this._random.Next(0 + ballRadius, this._simulation.Gui.ClientSize.Width - ballRadius),
                    this._random.Next(0 + ballRadius, this._simulation.Gui.ClientSize.Height - ballRadius)),
                Color.FromArgb(
                    this._random.Next(0, 220),
                    this._random.Next(0, 220),
                    this._random.Next(0, 220)),
                new Vector2D(this._random.Next(-ballRadius + 1, ballRadius), this._random.Next(-ballRadius + 1, ballRadius)), 
                this._simulation.Border,
                this._simulation
            );
        }
        
        public RepellentBall GenerateRepellentBall(IReadOnlyList<IBall> balls = null)
        {
            if (balls == null || balls.Count == 0)
            {
                var radius = this._random.Next(this._simulation.MinBallRadius, this._simulation.MaxBallRadius);
                return new RepellentBall(
                    radius,
                    new Vector2D(
                        this._random.Next(0 + radius, this._simulation.Gui.ClientSize.Width - radius),
                        this._random.Next(0 + radius, this._simulation.Gui.ClientSize.Height - radius)),
                    Color.FromArgb(
                        this._random.Next(0, 220),
                        this._random.Next(0, 220),
                        this._random.Next(0, 220)),
                    new Vector2D(this._random.Next(-radius + 1, radius), this._random.Next(-radius + 1, radius)), 
                    this._simulation.Border,
                    this._simulation
                );
            }
            
            // TODO: Generate a ball here but make sure it does not collide with any of the other balls!
            var ballRadius = this._random.Next(this._simulation.MinBallRadius, this._simulation.MaxBallRadius);
            return new RepellentBall(
                ballRadius,
                new Vector2D(
                    this._random.Next(0 + ballRadius, this._simulation.Gui.ClientSize.Width - ballRadius),
                    this._random.Next(0 + ballRadius, this._simulation.Gui.ClientSize.Height - ballRadius)),
                Color.FromArgb(
                    this._random.Next(0, 220),
                    this._random.Next(0, 220),
                    this._random.Next(0, 220)),
                new Vector2D(this._random.Next(-ballRadius + 1, ballRadius), this._random.Next(-ballRadius + 1, ballRadius)), 
                this._simulation.Border,
                this._simulation
            );
        }
        
        public MonsterBall GenerateMonsterBall(IReadOnlyList<IBall> balls = null)
        {
            if (balls == null || balls.Count == 0)
            {
                var radius = this._random.Next(this._simulation.MinBallRadius, this._simulation.MaxBallRadius);
                return new MonsterBall(
                    radius,
                    new Vector2D(
                        this._random.Next(0 + radius, this._simulation.Gui.ClientSize.Width - radius),
                        this._random.Next(0 + radius, this._simulation.Gui.ClientSize.Height - radius)),
                    this._simulation.Border,
                    this._simulation
                );
            }
            
            // TODO: Generate a ball here but make sure it does not collide with any of the other balls!
            var ballRadius = this._random.Next(this._simulation.MinBallRadius, this._simulation.MaxBallRadius);
            return new MonsterBall(
                ballRadius,
                new Vector2D(
                    this._random.Next(0 + ballRadius, this._simulation.Gui.ClientSize.Width - ballRadius),
                    this._random.Next(0 + ballRadius, this._simulation.Gui.ClientSize.Height - ballRadius)),
                this._simulation.Border,
                this._simulation
            );
        }
    }
}