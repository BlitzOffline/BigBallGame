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
    private readonly BallGenerator _ballGenerator;
    
    public Simulation(Gui gui)
    {
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
            var remaining = this.TickTime - passedTime;
            lastFrameTime = currentFrameTime;
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
        
        return this.AutomateTicking && this._balls.OfType<RegularBall>().Any();
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
        while (this._balls.Count < this.RegularBallsAmount)
        {
            this._balls.Add(this._ballGenerator.GenerateRegularBall());
        }

        while (this._balls.Count < this.RegularBallsAmount + this.RepellentBallsAmount)
        {
            this._balls.Add(this._ballGenerator.GenerateRepellentBall());
        }
            
            
        while (this._balls.Count < this.RegularBallsAmount + this.RepellentBallsAmount + this.MonsterBallsAmount)
        {
            this._balls.Add(this._ballGenerator.GenerateMonsterBall());
        }
    }

    public IEnumerable<IBall> GetBalls()
    {
        // Use a copy of the list to prevent concurrent modification
        return this._balls.ToList();
    }

    public void SendDebugMessage(string message)
    {
        if (this.Debug) Console.WriteLine(message);
    }
}