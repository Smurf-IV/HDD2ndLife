using System.Diagnostics;
using System.Numerics;
using System.Threading;

using Array = System.Array;

// ReSharper disable UnusedMember.Global

namespace AverageBuddy;

/// <summary>
/// Template class to help calculate the average value of a history of values. 
/// This can only be used with types that have a 'zero' and that have the += and / operators overloaded.
/// Example: Used to smooth frame rate calculations.
/// </summary>
/// <remarks>
/// Borrowed from https://github.com/dmanning23/AverageBuddy/tree/master
/// and then modified to be Net Core worthy ;-)
/// </remarks>
/// <typeparam name="T"></typeparam>
public sealed class Averager<T> where T :struct, /*IDivisionOperators<T, int, T>,*/ IAdditionOperators<T, T, T>
{
    #region Fields

    private int NextIndex { get; set; }

    private readonly Lock _lock = new();

    #endregion //Fields

    #region Members

    /// <summary>
    /// this holds the history
    /// </summary>
    private T[] _history;

    /// <summary>
    /// The max sample size we want
    /// </summary>
    private int _maxSize;

    /// <summary>
    /// Set the size of the `Averager` by seconds instead of sample size
    /// Assumes you are updating once a frame at 60fps
    /// </summary>
    public float MaxSeconds
    {
        set => _maxSize = ToFrames(value);
    }

    #endregion //Members

    #region Methods

    //to instantiate a Smoother pass it the number of samples you want
    //to use in the smoothing, and an example of a 'zero' type
    public Averager(int sampleSize, T zero)
    {
        //start the index at -1 so we can tell if this is the first entry
        NextIndex = -1;

        _maxSize = sampleSize;

        lock (_lock)
        {
            _history = new T[_maxSize];
            Array.Fill(_history, zero);
        }
    }

    /// <summary>
    /// average something over a period of time
    /// </summary>
    /// <param name="sampleSeconds"></param>
    /// <param name="zeroValue"></param>
    public Averager(float sampleSeconds, T zeroValue) :
        this(ToFrames(sampleSeconds), zeroValue)
    {
    }

    /// <summary>
    /// When you really want the `Averager` to return a certain value (say starting it up)
    /// </summary>
    /// <param name="currentValue">the value for the `Averager` to return</param>
    public void Set(T currentValue)
    {
        lock (_lock)
        {
            Array.Fill(_history, currentValue);
        }
    }

    /// <summary>
    /// each time you want to get a new average, feed it the most recent value
    /// and this method will return an average over the last SampleSize updates
    /// </summary>
    /// <param name="mostRecentValue"></param>
    /// <returns></returns>
    public T Update(T mostRecentValue)
    {
        Add(mostRecentValue);

        return Average();
    }

    /// <summary>
    /// Add a new item to the list
    /// </summary>
    /// <param name="mostRecentValue"></param>
    public void Add(T mostRecentValue)
    {
        //add the new value to the correct index
        lock (_lock)
        {
            //increment first 
            NextIndex++;
            if (NextIndex >= _maxSize)
            {
                NextIndex = 0;
            }

            Debug.Assert(NextIndex < _history.Length);
            _history[NextIndex] = mostRecentValue;
        }
    }

    /// <summary>
    /// Calculate the average from the current list of stuff
    /// </summary>
    /// <returns></returns>
    public T Average()
    {
        lock (_lock)
        {
            // This is the first item, use this instead of zero.
            T sum = _history[0];
            for (int i = 1; i < _maxSize; i++)
            {
                sum += _history[i];
            }

            Set((dynamic)sum / _maxSize);
            return _history[0];
        }
    }

    private static int ToFrames(float seconds)
    {
        seconds *= 60.0f;
        return (int)(seconds + 0.5f);
    }

    #endregion //Methods
}