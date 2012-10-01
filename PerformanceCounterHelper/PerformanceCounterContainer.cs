using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PerformanceCounterHelper
{
    /// <summary>
    /// Container of a relevant Performance Counter. It includes context information about that counter such as base performance counter and if it needs to increase/decrease automatically the base when the relevant one is increased / decreased.
    /// </summary>
    internal class PerformanceCounterContainer : IDisposable
    {
        private PerformanceCounter _performanceCounterInstance;
        private bool _baseAutoIncreased;
        private PerformanceCounter _performanceCounterBaseInstance;

        /// <summary>
        /// Get the instance of the relevant performanceCounter.
        /// </summary>
        public PerformanceCounter PerformanceCounterInstance 
        { 
            get 
            { 
                if (_disposed)
                    throw new ObjectDisposedException("PerformanceCounterContainer");

                return this._performanceCounterInstance; 
            } 
        }
        /// <summary>
        /// Get the instance of the base performanceCounter associated with the relevant counter in case there is need of one.
        /// <remarks>This value can be null.</remarks>
        /// </summary>
        public PerformanceCounter PerformanceCounterBaseInstance
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException("PerformanceCounterContainer");

                return this._performanceCounterBaseInstance;
            }
        }

        /// <summary>
        /// Get if the  Base should be autoincreased. This value is used internally and checked only when the relevant counter is modified.
        /// </summary>
        public bool IsBaseAutoIncreased 
        { 
            get 
            {
                if (_disposed)
                    throw new ObjectDisposedException("PerformanceCounterContainer");

                return this._baseAutoIncreased; 
            } 
        }
        
        /// <summary>
        /// Creates a container with only the relevant performance counter. This constructor sets the base instance in null and the autoincrease value to false
        /// If the performance counter you are passing as argument needs a base, you should consider using the other constructor.
        /// </summary>
        /// <param name="performanceCounterInstance">instance of performance counter</param>
        public PerformanceCounterContainer(PerformanceCounter performanceCounterInstance) : this(performanceCounterInstance, null, false) { }
        /// <summary>
        /// Creates a container with the relevant performance counter and the base one associated, setting also if the base counter should be increased / decreased when the relevant one is modified.
        /// If the autoincreased value is set to true, then when increasing or decreasing the relevant counter, the base is increased / decreased by 1. In case the autoincrease is set to false, the  user
        /// will need to manually update the base accordingly.
        /// </summary>
        /// <param name="performanceCounterInstance">instance of performance counter</param>
        /// <param name="performanceCounterBaseInstance">instance of performance counter being the base of the performanceCounterInstance</param>
        /// <param name="autoIncrease">true, to autoincrease the base, false if you prefer doing it manually.</param>
        public PerformanceCounterContainer(PerformanceCounter performanceCounterInstance, PerformanceCounter performanceCounterBaseInstance, bool autoIncrease) {

            this._performanceCounterInstance = performanceCounterInstance;
            this._performanceCounterBaseInstance = performanceCounterBaseInstance;
            this._baseAutoIncreased = autoIncrease;
        }

        #region IDisposable Members
        private bool _disposed; //false
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._disposed = true;
                this._performanceCounterInstance.RawValue = PerformanceHelper.getInitialValue(this._performanceCounterInstance.CounterType);
                this._performanceCounterInstance.Dispose();
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
        ~PerformanceCounterContainer()
        {
            this.Dispose(false);
        }
        #endregion
    }
}
