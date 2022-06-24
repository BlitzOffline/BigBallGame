using System.Collections.Concurrent;

namespace BigBallGame.Collection;

public class CircularQueue<T> : ConcurrentQueue<T>
{
    private readonly object _lockObject = new();
    
    public int Size { get; }
    
    public CircularQueue(int size)
    {
        Size = size;
    }

    public new void Enqueue(T obj)
    {
        base.Enqueue(obj);
        lock (_lockObject)
        {
            while (base.Count > Size)
            {
                base.TryDequeue(out _);
            }
        }
    }
}