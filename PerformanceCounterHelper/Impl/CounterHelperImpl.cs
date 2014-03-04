using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PerformanceCounterHelper.Impl
{
    /// <summary>
    /// Class to wrap managing logic for performance counters
    /// </summary>
    /// <typeparam name="T">Enum Type that defines the performance counter</typeparam>
    public class CounterHelperImpl<T> : CounterHelper<T>
    {
        /// <summary>
        /// Failure constant value
        /// </summary>
        public const int FAILURE = -1;
        /// <summary>
        /// Counters dictionary
        /// </summary>
        private Dictionary<T, PerformanceCounterContainer> _counters;

        private string _instanceName;

        /// <summary>
        /// Get the instance name associated with this counterHelper
        /// </summary>
        public string InstanceName 
        { 
            get 
            { 
                return this._instanceName; 
            } 
        }

        private CounterHelperImpl(int capacity, string instanceName)
        {
            this._counters = new Dictionary<T, PerformanceCounterContainer>(capacity);
            if (string.IsNullOrEmpty(instanceName))
            {
                this._instanceName = Guid.NewGuid().ToString();
            }
            else
            {
                this._instanceName = instanceName;
            }
        }
        /// <summary>
        /// Internal Constructor for named instances (multi-instance counters)
        /// </summary>
        /// <param name="instanceName">name for this instance</param>
        /// <param name="categoryInfo">information about this category</param>
        /// <param name="enumCounterAttributes">enumerator attributes</param>
        /// <exception cref="System.NotSupportedException" />
        internal CounterHelperImpl(string instanceName, PerformanceCounterCategoryAttribute categoryInfo, Dictionary<T, PerformanceCounterAttribute> enumCounterAttributes)
            : this(enumCounterAttributes.Count, instanceName)
        {
            if ( (categoryInfo.InstanceType == PerformanceCounterCategoryType.MultiInstance) 
                && (string.IsNullOrEmpty(instanceName)) )
                throw new NotSupportedException(Properties.Resources.CounterHelper_MultiInstanceNoInstanceNameErrorMessage);

            PerformanceCounter performanceCounter, performanceCounterBase = null;
            Dictionary<T, PerformanceCounterAttribute>.Enumerator enumerator = enumCounterAttributes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (categoryInfo.InstanceType == PerformanceCounterCategoryType.MultiInstance)
                {
                    performanceCounter = new PerformanceCounter(categoryInfo.Name,
                        enumerator.Current.Value.Name,
                        this._instanceName,
                        false);
                }
                else
                {
                    performanceCounter = new PerformanceCounter(categoryInfo.Name,
                        enumerator.Current.Value.Name,
                        false);
                }
                PerformanceCounterType? baseType = PerformanceHelper.GetBaseType(performanceCounter.CounterType);

                if (baseType != null)
                {
                    if (categoryInfo.InstanceType == PerformanceCounterCategoryType.MultiInstance)
                    {
                        performanceCounterBase = new PerformanceCounter(categoryInfo.Name,
                            PerformanceHelper.GetCounterNameForBaseType(enumerator.Current.Value.Name),
                            instanceName,
                            false);
                    }
                    else
                    {
                        performanceCounterBase = new PerformanceCounter(categoryInfo.Name,
                            PerformanceHelper.GetCounterNameForBaseType(enumerator.Current.Value.Name),
                            false);
                    }
                    performanceCounterBase.RawValue = PerformanceHelper.getInitialValue(performanceCounter.CounterType);
                }
                else
                {
                    performanceCounterBase = null;
                }

                PerformanceCounterContainer performanceCounterContainer = new PerformanceCounterContainer(performanceCounter, performanceCounterBase, enumerator.Current.Value.IsBaseAutoIncreased);
                this._counters.Add(enumerator.Current.Key, performanceCounterContainer);
            }
        }

        /// <summary>
        /// Internal Constructor for not named instances 
        /// </summary>
        /// <param name="categoryInfo">information about this category</param>
        /// <param name="enumCounterAttributes">enumerator attributes</param>
        /// <exception cref="System.NotSupportedException" />
        internal CounterHelperImpl(PerformanceCounterCategoryAttribute categoryInfo, Dictionary<T, PerformanceCounterAttribute> enumCounterAttributes)
            : this(null, categoryInfo, enumCounterAttributes) { }

        #region IDisposable Members
        private bool _disposed; //false
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._disposed = true;
                if (this._counters != null && this._counters.Count > 0)
                {
                    PerformanceCounterContainer[] counters = new PerformanceCounterContainer[this._counters.Values.Count];
                    this._counters.Values.CopyTo(counters, 0);
                    this._counters.Clear();

                    foreach (PerformanceCounterContainer performanceCounterContainer in counters)
                    {
                        performanceCounterContainer.Dispose();
                    }
                }
            }
        }
        /// <summary>
        /// Excplicit Call to dispose the object
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~CounterHelperImpl()
        {
            this.Dispose(false);
        }
        #endregion

        #region Public Method
        #region Operations over Relevant Counters
        /// <summary>
        /// Decrement value of the counter
        /// </summary>
        /// <param name="counterName">name of the counter to be decremented</param>
        /// <returns>returns FAILURE  in case there was an error otherwise the final value of the counter</returns>
        /// <exception cref="System.ObjectDisposedException" />
        public long Decrement(T counterName)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            PerformanceCounterContainer counter = null;

            if ((counter = this.GetContainer(counterName)) == null)
                return FAILURE;

            try
            {
                long rtnValue = counter.PerformanceCounterInstance.Decrement();
                if ( (counter.PerformanceCounterBaseInstance != null) && (counter.IsBaseAutoIncreased) )
                    counter.PerformanceCounterBaseInstance.Decrement();

                return rtnValue;
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.PlatformNotSupportedException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return FAILURE;
        }
        /// <summary>
        /// Decrement value of the counter by "value"
        /// </summary>
        /// <param name="value">value to decrement</param>
        /// <param name="counterName">name of the counter to be decremented</param>
        /// <returns>returns FAILURE  in case there was an error otherwise the final value of the counter</returns>
        /// <exception cref="System.ObjectDisposedException" />
        public long DecrementBy(T counterName, long value)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            if (value > 0)
            {
                value *= -1;
            }

            PerformanceCounterContainer counter = null;
            if ((counter = this.GetContainer(counterName)) == null)
                return FAILURE;
            try
            {
                long rtnValue = counter.PerformanceCounterInstance.IncrementBy(value);
                if ((counter.PerformanceCounterBaseInstance != null) && (counter.IsBaseAutoIncreased))
                    counter.PerformanceCounterBaseInstance.Decrement();

                return rtnValue;
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.PlatformNotSupportedException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return FAILURE;
        }
        /// <summary>
        /// Increment value of the counter
        /// </summary>
        /// <param name="counterName">name of the counter to be decremented</param>
        /// <returns>returns FAILURE  in case there was an error otherwise the final value of the counter </returns>
        /// <exception cref="System.ObjectDisposedException" />
        public long Increment(T counterName)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            PerformanceCounterContainer counter = null;
            if ((counter = this.GetContainer(counterName)) == null)
                return FAILURE;

            try
            {
                long rtnValue = counter.PerformanceCounterInstance.Increment();
                if ((counter.PerformanceCounterBaseInstance != null) && (counter.IsBaseAutoIncreased))
                    counter.PerformanceCounterBaseInstance.Increment();
                
                return rtnValue;
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.PlatformNotSupportedException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return FAILURE;

        }
        /// <summary>
        /// Increment value of the counter by "value"
        /// </summary>
        /// <param name="value">value to increment</param>
        /// <param name="counterName">name of the counter to be decremented</param>
        /// <returns>retorna -1 si hubo un error, o devuelve el valor final</returns>
        /// <exception cref="System.ObjectDisposedException" />
        public long IncrementBy(T counterName, long value)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            PerformanceCounterContainer counter = null;
            if ((counter = this.GetContainer(counterName)) == null)
                return FAILURE;
            try
            {
                long rtnValue = counter.PerformanceCounterInstance.IncrementBy(value);
                if ((counter.PerformanceCounterBaseInstance != null) && (counter.IsBaseAutoIncreased))
                    counter.PerformanceCounterBaseInstance.Increment();

                return rtnValue;
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.PlatformNotSupportedException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return FAILURE;
        }
        /// <summary>
        /// Get a sample of the counter 
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <returns>returns CounterSample.Empty in case there was an error, otherwise it returns the not calculated sample</returns>
        /// <exception cref="System.ObjectDisposedException" />
        public CounterSample NextSample(T counterName)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            PerformanceCounterContainer counter = null;
            if ((counter = this.GetContainer(counterName)) == null)
                return CounterSample.Empty;
            try
            {
                return counter.PerformanceCounterInstance.NextSample();
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.PlatformNotSupportedException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (UnauthorizedAccessException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return CounterSample.Empty;
        }
        /// <summary>
        /// Get the value of a counter
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <returns>returns FAILURE si hubo un error,in case there was an error, otherwise it returns the not calculated value</returns>
        /// <exception cref="System.ObjectDisposedException" />
        public float NextValue(T counterName)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            PerformanceCounterContainer counter = null;
            if ((counter = this.GetContainer(counterName)) == null)
                return FAILURE;

            try
            {
                return counter.PerformanceCounterInstance.NextValue();
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.PlatformNotSupportedException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (UnauthorizedAccessException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return FAILURE;
        }
        /// <summary>
        /// Get the value of a counter
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <returns>returns FAILURE si hubo un error,in case there was an error, otherwise it returns the not calculated value</returns>
        /// <exception cref="System.ObjectDisposedException" />
        /// <param name="value">value to be put on performance counter</param>
        public long RawValue(T counterName, long value)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            PerformanceCounterContainer counter = null;
            if ((counter = this.GetContainer(counterName)) == null)
                return FAILURE;

            try
            {
                long rtnValue = counter.PerformanceCounterInstance.RawValue = value;
                if ((counter.PerformanceCounterBaseInstance != null) && (counter.IsBaseAutoIncreased))
                    counter.PerformanceCounterBaseInstance.Increment();
                return rtnValue;
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.PlatformNotSupportedException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (UnauthorizedAccessException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return FAILURE;
        }
        /// <summary>
        /// Get the PerformanceCounter Instance associated with the countername.
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <returns>returns an instance of PerformanceCounter in case there is such object associated with the countername given. Otherwise, null</returns>
        public PerformanceCounter GetInstance(T counterName)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            PerformanceCounterContainer counter = null;

            if ((counter = this.GetContainer(counterName)) == null)
            {
                return null;
            }
            return counter.PerformanceCounterInstance;
        }
        /// <summary>
        /// Reset to default value the instance counter
        /// </summary>
        /// <param name="counterName">the counter name</param>
        public void Reset(T counterName)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            PerformanceCounterContainer counter = null;
            if ((counter = this.GetContainer(counterName)) != null)
            {
                counter.PerformanceCounterInstance.RawValue = PerformanceHelper.getInitialValue(counter.PerformanceCounterInstance.CounterType);
                if (counter.IsBaseAutoIncreased && counter.PerformanceCounterBaseInstance != null) 
                {
                    counter.PerformanceCounterBaseInstance.RawValue = PerformanceHelper.getInitialValue(counter.PerformanceCounterBaseInstance.CounterType);
                }
            }
        }
        #endregion

        #region Operations over Base Counters
        /// <summary>
        /// Decrement value of the base counter
        /// </summary>
        /// <param name="counterName">name of the counter to has his base counter decremented</param>
        /// <returns>returns FAILURE  in case there was an error otherwise the final value of the counter</returns>
        /// <exception cref="System.ObjectDisposedException" />
        public long DecrementBase(T counterName)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            PerformanceCounterContainer counter = null;

            if ((counter = this.GetContainer(counterName)) == null)
                return FAILURE;

            if (counter.PerformanceCounterBaseInstance == null)
                return FAILURE;

            try
            {
                long rtnValue = counter.PerformanceCounterBaseInstance.Decrement();
                return rtnValue;
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.PlatformNotSupportedException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return FAILURE;
        }
        /// <summary>
        /// Increment value of the base counter
        /// </summary>
        /// <param name="counterName">name of the counter to has its base counter decremented</param>
        /// <returns>returns FAILURE  in case there was an error otherwise the final value of the counter </returns>
        /// <exception cref="System.ObjectDisposedException" />
        public long IncrementBase(T counterName)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            PerformanceCounterContainer counter = null;
            if ((counter = this.GetContainer(counterName)) == null)
                return FAILURE;
            if (counter.PerformanceCounterBaseInstance == null)
                return FAILURE;

            try
            {
                long rtnValue = counter.PerformanceCounterBaseInstance.Increment();
                return rtnValue;
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.PlatformNotSupportedException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return FAILURE;

        }
        /// <summary>
        /// Increment value of the base counter by "value"
        /// </summary>
        /// <param name="value">value to increment</param>
        /// <param name="counterName">name of the counter to has its base counter decremented</param>
        /// <returns>returns -1 in case there was an error, otherwise it returns the final value</returns>
        /// <exception cref="System.ObjectDisposedException" />
        public long IncrementBaseBy(T counterName, long value)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            PerformanceCounterContainer counter = null;
            if ((counter = this.GetContainer(counterName)) == null)
                return FAILURE;
            if (counter.PerformanceCounterBaseInstance == null)
                return FAILURE;

            try
            {
                long rtnValue = counter.PerformanceCounterBaseInstance.IncrementBy(value);
                return rtnValue;
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.PlatformNotSupportedException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return FAILURE;
        }
        /// <summary>
        /// Get a sample of the base counter 
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <returns>returns CounterSample.Empty in case there was an error, otherwise it returns the not calculated sample</returns>
        /// <exception cref="System.ObjectDisposedException" />
        public CounterSample NextBaseSample(T counterName)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            PerformanceCounterContainer counter = null;
            if ((counter = this.GetContainer(counterName)) == null)
                return CounterSample.Empty;

            if (counter.PerformanceCounterBaseInstance == null)
                return CounterSample.Empty;

            try
            {
                return counter.PerformanceCounterBaseInstance.NextSample();
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.PlatformNotSupportedException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (UnauthorizedAccessException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return CounterSample.Empty;
        }
        /// <summary>
        /// Get the value of a base counter
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <returns>returns FAILURE si hubo un error,in case there was an error, otherwise it returns the not calculated value</returns>
        /// <exception cref="System.ObjectDisposedException" />
        public float NextBaseValue(T counterName)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            PerformanceCounterContainer counter = null;
            if ((counter = this.GetContainer(counterName)) == null)
                return FAILURE;

            if (counter.PerformanceCounterBaseInstance == null)
                return FAILURE;

            try
            {
                return counter.PerformanceCounterBaseInstance.NextValue();
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.PlatformNotSupportedException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (UnauthorizedAccessException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return FAILURE;
        }
        /// <summary>
        /// Get the value of a base counter
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <param name="value">value to be put on performance counter</param>
        /// <returns>returns FAILURE si hubo un error,in case there was an error, otherwise it returns the not calculated value</returns>
        /// <exception cref="System.ObjectDisposedException" />
        public long BaseRawValue(T counterName, long value)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            PerformanceCounterContainer counter = null;
            if ((counter = this.GetContainer(counterName)) == null)
                return FAILURE;

            if (counter.PerformanceCounterBaseInstance == null)
            {
                return FAILURE;
            }
            try
            {
                long rtnValue = counter.PerformanceCounterBaseInstance.RawValue = value;
                return rtnValue;
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (System.PlatformNotSupportedException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            catch (UnauthorizedAccessException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return FAILURE;
        }
        /// <summary>
        /// Get the PerformanceCounter Base Instance associated with the countername.
        /// 
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <returns>returns an instance of PerformanceCounter in case there is such object associated with the countername given as a base counter. Otherwise, null</returns>
        public PerformanceCounter GetBaseInstance(T counterName)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().Name);

            PerformanceCounterContainer counter = null;

            if ((counter = this.GetContainer(counterName)) == null)
            {
                return null;
            }
            return counter.PerformanceCounterBaseInstance;
        }

        #endregion
        #endregion

        /// <summary>
        /// get the PerformanceCounterContainer associated with the given counterName.
        /// </summary>
        /// <param name="counterName">name of the counter</param>
        /// <returns>PerformanceCounterContainer instance in case there is such. Otherwise null.</returns>
        private PerformanceCounterContainer GetContainer(T counterName)
        {
            PerformanceCounterContainer counter = null;

            if (!this._counters.TryGetValue(counterName, out counter))
            {
                return null;
            }
            return counter;
        }

        public void RemoveInstancesAndDispose()
        {
            foreach (var container in _counters.Values)
            {
                container.PerformanceCounterInstance.RemoveInstance();
                
                if (container.PerformanceCounterBaseInstance != null)
                    container.PerformanceCounterBaseInstance.RemoveInstance();
            }

            Dispose();
        }

    }
}