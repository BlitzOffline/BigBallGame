using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BigBallGame.Ball;

namespace BigBallGame.Simulation;

public class Simulation
{
    public bool Debug = false;
    public bool AutomateTicking = true;
    
    // Sets every how many milliseconds the simulation should try and update
    public int TickTime = 20;
    
    public int RegularBallsAmount = 10;
    public int RepellentBallsAmount = 5;
    public int MonsterBallsAmount = 1;

    public int MinBallRadius = 15;
    public int MaxBallRadius = 35;
    
    public readonly Gui Gui;
    public readonly Border Border;
    
    private readonly List<IBall> _balls = new();
    private readonly BallsGenerator _ballsGenerator;
    
    public Simulation(Gui gui)
    {
        Gui = gui;
        Border.MinX = 0;
        Border.MinY = 0;
        Border.MaxX = Gui.ClientSize.Width;
        Border.MaxY = Gui.ClientSize.Height;
        
        _ballsGenerator = new BallsGenerator(this);
        GenerateBalls();
    }
    
    public void StartSimulation()
    { 
        Start();
    }

    public void TickSimulation()
    {
        if (!_balls.OfType<RegularBall>().Any() && !_balls.OfType<RepellentBall>().Any()) return;
        
        Render();
        Tick();
    }
    
    private void Start()
    {
        SendDebugMessage("Started simulation...");
        Render();
        SendDebugMessage("Rendered balls...");
        var lastFrameTime = Environment.TickCount;
        while (Tick())
        {
            var currentFrameTime = Environment.TickCount;
            var passedTime = currentFrameTime - lastFrameTime;
            SendDebugMessage("Passed time: " + passedTime);
            var remaining = TickTime - passedTime;
            lastFrameTime = currentFrameTime;
            if (remaining > 0) Thread.Sleep(remaining);
        }
    }

    private bool Tick()
    {
        SendDebugMessage("Ticking...");
        for (var i = _balls.Count - 1; i >= 0; i--)
        {
            _balls[i].Move();
        }
        SendDebugMessage("Moved balls...");

        foreach (var ball in _balls.Where(HandleCollisions).ToList())
        {
            _balls.Remove(ball);
        }

        SendDebugMessage("Handled collisions...");

        Render();
        SendDebugMessage("Rerendered balls...");
        
        return AutomateTicking && _balls.OfType<RegularBall>().Any();
    }
    
    private bool HandleCollisions(IBall tickedBall)
    {
        var shouldRemove = false;
        for (var i = _balls.Count - 1; i >= 0; i--)
        {
            var ball = _balls[i];
            
            if (ball.Equals(tickedBall)) continue;
            if (!tickedBall.CollidesWith(ball)) continue;
            
            shouldRemove = tickedBall.CollideWith(ball);
        }
        
        return shouldRemove;
    }

    public void Render()
    {
        // We refresh the form to make sure OnPaint is called.
        Gui.Invalidate();
    }

    private void GenerateBalls()
    {
        while (_balls.Count < RegularBallsAmount)
        {
            _balls.Add(_ballsGenerator.GenerateRegularBall());
        }

        while (_balls.Count < RegularBallsAmount + RepellentBallsAmount)
        {
            _balls.Add(_ballsGenerator.GenerateRepellentBall());
        }
            
            
        while (_balls.Count < RegularBallsAmount + RepellentBallsAmount + MonsterBallsAmount)
        {
            _balls.Add(_ballsGenerator.GenerateMonsterBall());
        }
    }

    public IEnumerable<IBall> GetBalls()
    {
        return _balls.ToList();
    }

    public void SendDebugMessage(string message)
    {
        if (Debug) Console.WriteLine(message);
    }
}