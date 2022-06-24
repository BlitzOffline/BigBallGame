using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BigBallGame.Ball;
using BigBallGame.Collection;

namespace BigBallGame.Simulation;

public class Simulation
{
    private readonly CircularQueue<float> _fps = new(60);
    public float GetAverageFps()
    {
        return _fps.Sum() / _fps.Count;
    }

    public readonly bool Debug;
    public readonly bool ShowDirections;
    public readonly bool AutomateTicking;
    
    // Sets every how many milliseconds the simulation should try and update
    private readonly int _tickTime;

    private readonly int _regularBallsAmount;
    private readonly int _repellentBallsAmount;
    private readonly int _monsterBallsAmount;

    public readonly int MinBallRadius;
    public readonly int MaxBallRadius;
    
    public readonly Gui Gui;
    public readonly Border Border;
    
    private readonly List<IBall> _balls = new();
    private readonly BallGenerator _ballGenerator;
    
    public Simulation(
        Gui gui,
        bool debug,
        bool showDirections,
        bool automateTicking,
        int tickTime,
        int regularBallsAmount,
        int repellentBallsAmount,
        int monsterBallsAmount,
        int minBallRadius,
        int maxBallRadius)
    {
        this.Gui = gui;
        
        this.Debug = debug;
        this.ShowDirections = showDirections;
        this.AutomateTicking = automateTicking;
        
        this._tickTime = tickTime;

        this._regularBallsAmount = regularBallsAmount;
        this._repellentBallsAmount = repellentBallsAmount;
        this._monsterBallsAmount = monsterBallsAmount;
        
        this.MinBallRadius = minBallRadius;
        this.MaxBallRadius = maxBallRadius;
        
        this.Gui = gui;
        this.Border.MinX = 0;
        this.Border.MinY = 0;
        this.Border.MaxX = this.Gui.ClientSize.Width;
        this.Border.MaxY = this.Gui.ClientSize.Height;

        this._ballGenerator = new BallGenerator(this);
        this.GenerateBalls();
    }
    
    public void StartSimulation()
    {
        this.Start();
    }

    public void TickSimulation()
    {
        if (!this._balls.OfType<RegularBall>().Any() && !this._balls.OfType<RepellentBall>().Any()) return;
        
        this.Render();
        this.Tick();
    }
    
    private void Start()
    {
        this.SendDebugMessage("Started simulation...");
        this.Render();
        this.SendDebugMessage("Rendered balls...");
        
        var lastFrameTime = Environment.TickCount;
        while (this.Tick())
        {
            var currentFrameTime = Environment.TickCount;
            var passedTime = currentFrameTime - lastFrameTime;
            this.SendDebugMessage("Passed time: " + passedTime);
            var remaining = this._tickTime - passedTime;
            lastFrameTime = currentFrameTime;
            this._fps.Enqueue(remaining >= 0 ? 1000f/ this._tickTime : 1000f / (this._tickTime + -remaining));
            if (remaining > 0) Thread.Sleep(remaining);
        }
    }

    private bool Tick()
    {
        this.SendDebugMessage("Ticking...");
        for (var i = this._balls.Count - 1; i >= 0; i--)
        {
            this._balls[i].Move();
        }
        this.SendDebugMessage("Moved balls...");

        foreach (var ball in this._balls.Where(this.HandleCollisions).ToList())
        {
            this._balls.Remove(ball);
        }

        this.SendDebugMessage("Handled collisions...");

        this.Render();
        this.SendDebugMessage("Rerendered balls...");
        
        return this._balls.OfType<RegularBall>().Any() ||
               (Debug && (this._balls.OfType<RepellentBall>().Any() || this._balls.OfType<RegularBall>().Any()));
    }
    
    private bool HandleCollisions(IBall tickedBall)
    {
        var shouldRemove = false;
        for (var i = this._balls.Count - 1; i >= 0; i--)
        {
            var ball = this._balls[i];
            
            if (ball.Equals(tickedBall)) continue;
            if (!tickedBall.CollidesWith(ball)) continue;
            
            shouldRemove = tickedBall.CollideWith(ball);
        }
        
        return shouldRemove;
    }

    public void Render()
    {
        // We refresh the form to make sure OnPaint is called.
        this.Gui.Invalidate();
    }

    private void GenerateBalls()
    {
        while (this._balls.Count < this._regularBallsAmount)
        {
            this._balls.Add(this._ballGenerator.GenerateRegularBall());
        }

        while (this._balls.Count < this._regularBallsAmount + this._repellentBallsAmount)
        {
            this._balls.Add(this._ballGenerator.GenerateRepellentBall());
        }
            
            
        while (this._balls.Count < this._regularBallsAmount + this._repellentBallsAmount + this._monsterBallsAmount)
        {
            this._balls.Add(this._ballGenerator.GenerateMonsterBall());
        }
    }

    public IEnumerable<IBall> GetBalls()
    {
        // Use a copy of the list to prevent concurrent modification
        return this._balls.ToList();
    }

    public void SendDebugMessage(string message = "")
    {
        if (this.Debug) Console.WriteLine(message);
    }
}