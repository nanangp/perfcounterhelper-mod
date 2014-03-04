using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PerformanceCounterHelper
{
    /// <summary>
    /// Interface for implementations that will hold all information and actions available regarding a performance counter.
    /// </summary>
    /// <typeparam name="T">enum type holding performance counter details</typeparam>
    public interface CounterHelper<T> : IDisposable
    {
        /// <summary>
        /// Decrement value of the counter
        /// </summary>
        /// <param name="counterName">name of the counter to be decremented</param>
        /// <returns>returns FAILURE  in case there was an error otherwise the final value of the counter</returns>
        /// <exception cref="System.ObjectDisposedException" />
        long Decrement(T counterName);

        /// <summary>
        /// Decrement value of the counter by "value"
        /// </summary>
        /// <param name="value">value to decrement</param>
        /// <param name="counterName">name of the counter to be decremented</param>
        /// <returns>returns FAILURE  in case there was an error otherwise the final value of the counter</returns>
        /// <exception cref="System.ObjectDisposedException" />
        long DecrementBy(T counterName, long value);

        /// <summary>
        /// Increment value of the counter
        /// </summary>
        /// <param name="counterName">name of the counter to be decremented</param>
        /// <returns>returns FAILURE  in case there was an error otherwise the final value of the counter </returns>
        /// <exception cref="System.ObjectDisposedException" />
        long Increment(T counterName);

        /// <summary>
        /// Increment value of the counter by "value"
        /// </summary>
        /// <param name="value">value to increment</param>
        /// <param name="counterName">name of the counter to be decremented</param>
        /// <returns>retorna -1 si hubo un error, o devuelve el valor final</returns>
        /// <exception cref="System.ObjectDisposedException" />
        long IncrementBy(T counterName, long value);

        /// <summary>
        /// Get a sample of the counter 
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <returns>returns CounterSample.Empty in case there was an error, otherwise it returns the not calculated sample</returns>
        /// <exception cref="System.ObjectDisposedException" />
        CounterSample NextSample(T counterName);

        /// <summary>
        /// Get the value of a counter
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <returns>returns FAILURE si hubo un error,in case there was an error, otherwise it returns the not calculated value</returns>
        /// <exception cref="System.ObjectDisposedException" />
        float NextValue(T counterName);

        /// <summary>
        /// Get the value of a counter
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <param name="value">value to put on performance counter</param>
        /// <returns>returns FAILURE si hubo un error,in case there was an error, otherwise it returns the not calculated value</returns>
        /// <exception cref="System.ObjectDisposedException" />
        long RawValue(T counterName, long value);

        /// <summary>
        /// Get the PerformanceCounter Instance associated with the countername.
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <returns>returns an instance of PerformanceCounter in case there is such object associated with the countername given. Otherwise, null</returns>
        PerformanceCounter GetInstance(T counterName);

        /// <summary>
        /// Reset to default value the instance counter
        /// </summary>
        /// <param name="counterName">the counter name</param>
        void Reset(T counterName);


        /// <summary>
        /// Decrement value of the base counter
        /// </summary>
        /// <param name="counterName">name of the counter to has his base counter decremented</param>
        /// <returns>returns FAILURE  in case there was an error otherwise the final value of the counter</returns>
        /// <exception cref="System.ObjectDisposedException" />
        long DecrementBase(T counterName);

        /// <summary>
        /// Increment value of the base counter
        /// </summary>
        /// <param name="counterName">name of the counter to has its base counter decremented</param>
        /// <returns>returns FAILURE  in case there was an error otherwise the final value of the counter </returns>
        /// <exception cref="System.ObjectDisposedException" />
        long IncrementBase(T counterName);

        /// <summary>
        /// Increment value of the base counter by "value"
        /// </summary>
        /// <param name="value">value to increment</param>
        /// <param name="counterName">name of the counter to has its base counter decremented</param>
        /// <returns>returns -1 in case there was an error, otherwise it returns the final value</returns>
        /// <exception cref="System.ObjectDisposedException" />
        long IncrementBaseBy(T counterName, long value);

        /// <summary>
        /// Get a sample of the base counter 
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <returns>returns CounterSample.Empty in case there was an error, otherwise it returns the not calculated sample</returns>
        /// <exception cref="System.ObjectDisposedException" />
        CounterSample NextBaseSample(T counterName);

        /// <summary>
        /// Get the value of a base counter
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <returns>returns FAILURE si hubo un error,in case there was an error, otherwise it returns the not calculated value</returns>
        /// <exception cref="System.ObjectDisposedException" />
        float NextBaseValue(T counterName);

        /// <summary>
        /// Get the value of a base counter
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <returns>returns FAILURE si hubo un error,in case there was an error, otherwise it returns the not calculated value</returns>
        /// <exception cref="System.ObjectDisposedException" />
        /// <param name="value">value to be set</param>
        long BaseRawValue(T counterName, long value);

        /// <summary>
        /// Get the PerformanceCounter Base Instance associated with the countername.
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <returns>returns an instance of PerformanceCounter in case there is such object associated with the countername given as a base counter. Otherwise, null</returns>
        PerformanceCounter GetBaseInstance(T counterName);

        /// <summary>
        /// Removes instances (if MultiInstance) and dispose the counters.
        /// </summary>
        void RemoveInstancesAndDispose();
    }
}
