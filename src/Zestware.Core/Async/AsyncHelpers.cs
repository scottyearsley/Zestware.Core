using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Zestware.Async;

// Code 'borrowed' from:
// http://stackoverflow.com/questions/5095183/how-would-i-run-an-async-taskt-method-synchronously

[DebuggerStepThrough]
public static class AsyncHelpers
{
    /// <summary>
    /// Executes an async Task{T} method which has a void return value synchronously
    /// </summary>
    /// <param name="task">Task{T} method to execute</param>
    [DebuggerHidden]
    [DebuggerStepThrough]
    public static void RunSync(Func<Task> task)
    {
        var oldContext = SynchronizationContext.Current;
        var synch = new ExclusiveSynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(synch);
        // ReSharper disable once AsyncVoidLambda
        synch.Post(async _ =>
        {
            try
            {
                await task();
            }
            catch (Exception e)
            {
                synch.InnerException = e;
                throw;
            }
            finally
            {
                synch.EndMessageLoop();
            }
        }, null);
        synch.BeginMessageLoop();

        SynchronizationContext.SetSynchronizationContext(oldContext);
    }

    /// <summary>
    /// Executes an async Task{T} method which has a T return type synchronously
    /// </summary>
    /// <typeparam name="T">Return Type</typeparam>
    /// <param name="task">Task{T} method to execute</param>
    /// <returns></returns>
    [DebuggerHidden]
    [DebuggerStepThrough]
    public static T? RunSync<T>(Func<Task<T>> task)
    {
        var oldContext = SynchronizationContext.Current;
        var synch = new ExclusiveSynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(synch);
        T? ret = default(T);
        // ReSharper disable once AsyncVoidLambda
        synch.Post(async _ =>
        {
            try
            {
                ret = await task();
            }
            catch (Exception e)
            {
                synch.InnerException = e;
                throw;
            }
            finally
            {
                synch.EndMessageLoop();
            }
        }, null);
        synch.BeginMessageLoop();
        SynchronizationContext.SetSynchronizationContext(oldContext);
        return ret;
    }

    private class ExclusiveSynchronizationContext : SynchronizationContext
    {
        private bool _done;
        private readonly AutoResetEvent? _workItemsWaiting = new(false);
        private readonly Queue<Tuple<SendOrPostCallback, object>> _items = new();
        
        public Exception? InnerException { get; set; }

        public override void Send(SendOrPostCallback d, object? state)
        {
            throw new NotSupportedException("We cannot send to our same thread");
        }

        public override void Post(SendOrPostCallback d, object? state)
        {
            lock (_items)
            {
                if (state != null)
                {
                    _items.Enqueue(Tuple.Create(d, state));
                }
            }
            _workItemsWaiting?.Set();
        }

        public void EndMessageLoop()
        {
            Post(_ => _done = true, null!);
        }

        public void BeginMessageLoop()
        {
            while (!_done)
            {
                Tuple<SendOrPostCallback, object> task = null!;
                lock (_items)
                {
                    if (_items.Count > 0)
                    {
                        task = _items.Dequeue();
                    }
                }
                if (task != null)
                {
                    task.Item1(task.Item2);
                    if (InnerException != null) // the method threw an exeption
                    {
                        throw new AggregateException("AsyncHelpers.Run method threw an exception.", InnerException);
                    }
                }
                else
                {
                    _workItemsWaiting?.WaitOne();
                }
            }
        }

        public override SynchronizationContext CreateCopy()
        {
            return this;
        }
    }
}