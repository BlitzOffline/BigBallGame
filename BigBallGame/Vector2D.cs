using System;
using System.Drawing;

namespace BigBallGame;
public class Vector2D {

    public float X { get; set; }
    public float Y { get; set; }
    public float Length => (float) Math.Sqrt(this.X * this.X + this.Y * this.Y);

    public Vector2D()
    {
        this.X = 0;
        this.Y = 0;
    }

    public Vector2D(float x, float y)
    {
        this.X = x;
        this.Y = y;
    }

    public void Set(float x, float y)
    {
        this.X = x;
        this.Y = y;
    }

    public float Dot(Vector2D other)
    {
        return this.X * other.X + this.Y * other.Y;;
    }
    
    public float GetDistance(Vector2D other)
    {
        return (float) Math.Sqrt((other.X - this.X) * (other.X - this.X) + (other.Y - this.Y) * (other.Y - this.Y));
    }

    public Vector2D Add(Vector2D other)
    {
        return new Vector2D
        {
            X = this.X + other.X,
            Y = this.Y + other.Y
        };
    }

    public Vector2D Subtract(Vector2D other)
    {
        return new Vector2D
        {
            X = this.X - other.X,
            Y = this.Y - other.Y
        };
    }

    public Vector2D Multiply(float scaleFactor)
    {
        return new Vector2D
        {
            X = this.X * scaleFactor,
            Y = this.Y * scaleFactor
        };
    }

    public Vector2D Normalize()
    {
        var len = Length;
        if (len != 0.0f)
        {
            this.X = this.X / len;
            this.Y = this.Y / len;
        }
        else
        {
            this.X = 0.0f;
            this.Y = 0.0f;
        }

        return this;
    }

    public PointF ToPointF()
    {
        return new PointF(this.X, this.Y);
    }

    public override string ToString()
    {
        return "Vector2D[X=" + this.X + ", Y=" + this.Y + "]";
    }
}