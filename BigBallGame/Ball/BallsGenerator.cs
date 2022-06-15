using System;
using System.Collections.Generic;
using System.Drawing;

namespace BigBallGame.Ball
{
    public class BallsGenerator
    {
        private readonly Random _random;
        private readonly Simulation.Simulation _simulation;
        
        public BallsGenerator(Simulation.Simulation simulation)
        {
            _random = new Random();
            _simulation = simulation;
        }
        
        public BallsGenerator(int seed, Simulation.Simulation simulation)
        {
            _random = new Random(seed);
            _simulation = simulation;
        }
        
        public BallsGenerator(Random random, Simulation.Simulation simulation)
        {
            _random = random;
            _simulation = simulation;
        }

        public Ball GenerateRegularBall(IReadOnlyList<IBall> balls = null)
        {
            if (balls == null || balls.Count == 0)
            {
                var radius = _random.Next(_simulation.MinBallRadius, _simulation.MaxBallRadius);
                return new RegularBall(
                    radius,
                    new Vector2D(
                        _random.Next(0 + radius, _simulation.Gui.ClientSize.Width - radius),
                        _random.Next(0 + radius, _simulation.Gui.ClientSize.Height - radius)),
                    Color.FromArgb(
                        _random.Next(0, 220),
                        _random.Next(0, 220),
                        _random.Next(0, 220)),
                    new Vector2D(_random.Next(1, radius), _random.Next(1, radius)), 
                    _simulation.Border,
                    _simulation
                );
            }
            
            // TODO: Generate a ball here but make sure it does not collide with any of the other balls!
            var ballRadius = _random.Next(_simulation.MinBallRadius, _simulation.MaxBallRadius);
            return new RegularBall(
                ballRadius,
                new Vector2D(
                    _random.Next(0 + ballRadius, _simulation.Gui.ClientSize.Width - ballRadius),
                    _random.Next(0 + ballRadius, _simulation.Gui.ClientSize.Height - ballRadius)),
                Color.FromArgb(
                    _random.Next(0, 220),
                    _random.Next(0, 220),
                    _random.Next(0, 220)),
                new Vector2D(_random.Next(1, ballRadius), _random.Next(1, ballRadius)), 
                _simulation.Border,
                _simulation
            );
        }
        
        public RepellentBall GenerateRepellentBall(IReadOnlyList<IBall> balls = null)
        {
            if (balls == null || balls.Count == 0)
            {
                var radius = _random.Next(_simulation.MinBallRadius, _simulation.MaxBallRadius);
                return new RepellentBall(
                    radius,
                    new Vector2D(
                        _random.Next(0 + radius, _simulation.Gui.ClientSize.Width - radius),
                        _random.Next(0 + radius, _simulation.Gui.ClientSize.Height - radius)),
                    Color.FromArgb(
                        _random.Next(0, 220),
                        _random.Next(0, 220),
                        _random.Next(0, 220)),
                    new Vector2D(_random.Next(1, radius), _random.Next(1, radius)), 
                    _simulation.Border,
                    _simulation
                );
            }
            
            // TODO: Generate a ball here but make sure it does not collide with any of the other balls!
            var ballRadius = _random.Next(_simulation.MinBallRadius, _simulation.MaxBallRadius);
            return new RepellentBall(
                ballRadius,
                new Vector2D(
                    _random.Next(0 + ballRadius, _simulation.Gui.ClientSize.Width - ballRadius),
                    _random.Next(0 + ballRadius, _simulation.Gui.ClientSize.Height - ballRadius)),
                Color.FromArgb(
                    _random.Next(0, 220),
                    _random.Next(0, 220),
                    _random.Next(0, 220)),
                new Vector2D(_random.Next(1, ballRadius), _random.Next(1, ballRadius)), 
                _simulation.Border,
                _simulation
            );
        }
        
        public MonsterBall GenerateMonsterBall(IReadOnlyList<IBall> balls = null)
        {
            if (balls == null || balls.Count == 0)
            {
                var radius = _random.Next(_simulation.MinBallRadius, _simulation.MaxBallRadius);
                return new MonsterBall(
                    radius,
                    new Vector2D(
                        _random.Next(0 + radius, _simulation.Gui.ClientSize.Width - radius),
                        _random.Next(0 + radius, _simulation.Gui.ClientSize.Height - radius)),
                    _simulation.Border,
                    _simulation
                );
            }
            
            // TODO: Generate a ball here but make sure it does not collide with any of the other balls!
            var ballRadius = _random.Next(_simulation.MinBallRadius, _simulation.MaxBallRadius);
            return new MonsterBall(
                ballRadius,
                new Vector2D(
                    _random.Next(0 + ballRadius, _simulation.Gui.ClientSize.Width - ballRadius),
                    _random.Next(0 + ballRadius, _simulation.Gui.ClientSize.Height - ballRadius)),
                _simulation.Border,
                _simulation
            );
        }
    }
}