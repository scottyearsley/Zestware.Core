using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Zestware
{
    /// <summary>
    /// Provides support for asynchronous lazy initialization. This type is fully threadsafe.
    /// </summary>
    /// <typeparam name="T">The type of object that is being asynchronously initialized.</typeparam>
    public sealed class LazyAsync<T>
    {
        /// <summary>
        /// The underlying lazy task.
        /// </summary>
        private readonly Lazy<Task<T>> _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyAsync&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="factory">The delegate that is invoked on a background thread to produce the value when it is needed.</param>
        public LazyAsync(Func<T> factory)
        {
            _instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyAsync&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="factory">The asynchronous delegate that is invoked on a background thread to produce the value when it is needed.</param>
        public LazyAsync(Func<Task<T>> factory)
        {
            _instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        /// <summary>
        /// Asynchronous infrastructure support. This method permits instances of <see cref="LazyAsync&lt;T&gt;"/> to be await'ed.
        /// </summary>
        public TaskAwaiter<T> GetAwaiter()
        {
            return _instance.Value.GetAwaiter();
        }

        /// <summary>
        /// Starts the asynchronous initialization, if it has not already started.
        /// </summary>
        public void Start()
        {
            var unused = _instance.Value;
        }
    }
}