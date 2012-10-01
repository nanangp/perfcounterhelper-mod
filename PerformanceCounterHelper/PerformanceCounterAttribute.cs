using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PerformanceCounterHelper
{
    /// <summary>
    /// Attribute used for Performance counter. 
    /// It contains information about the counter that would be used to configure how to manage this variable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class PerformanceCounterAttribute : Attribute
    {
        private string _name;
        private string _info;
        private PerformanceCounterType _counterType;
        private bool _baseAutoIncreased;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the counter</param>
        /// <param name="info">Information about the counter</param>
        /// <param name="counterType">Type of counter</param>
        /// <seealso cref="PerformanceCounterType"/>
        public PerformanceCounterAttribute(string name, string info, PerformanceCounterType counterType)
            : this(name, info, counterType, true) {}
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the counter</param>
        /// <param name="info">Information about the counter</param>
        /// <param name="counterType">Type of counter</param>
        /// <param name="baseAutoIncreased">if true, each time the performance counter increased/decreased its base will be increased/decrease on 1 point. Otherwise all that base management will need to be handed on client code</param>
        /// <seealso cref="PerformanceCounterType"/>
        public PerformanceCounterAttribute(string name, string info, PerformanceCounterType counterType, bool baseAutoIncreased)
            : base()
        {
            this._name = name;
            this._info = info;
            this._counterType = counterType;
            this._baseAutoIncreased = baseAutoIncreased;
        }
        /// <summary>
        /// Specify the formula to be used to calculate the value when the method 'NextValue' is called
        /// </summary>
        /// <param name="performanceCounter">Counter</param>
        /// <returns>returns the CounterType</returns>
        public static implicit operator PerformanceCounterType(PerformanceCounterAttribute performanceCounter)
        {
            return performanceCounter.CounterType;
        }
        /// <summary>
        /// Override method to returns counter name
        /// </summary>
        /// <returns>returns the counter name</returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Get or Set the counter name
        /// </summary>
        public string Name { get { return this._name; } }
        /// <summary>
        /// Get or Set information about the counter
        /// </summary>
        public string Info { get { return this._info; } }
        /// <summary>
        /// Get or Set counterType 
        /// </summary>
        public PerformanceCounterType CounterType { get { return this._counterType; } }

        /// <summary>
        /// Indicates that, if this performance counter needs a base, it should be increased/decreased by 1 when the relevant one is increased/decreased.
        /// </summary>
        public bool IsBaseAutoIncreased { get { return this._baseAutoIncreased; } }
    }
}
