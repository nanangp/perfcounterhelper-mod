using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PerformanceCounterHelper
{
    /// <summary>
    /// Attribute to be set to the category containing a set of performance counters
    /// It contains information about the category that would be used to configure how to managed this category.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public sealed class PerformanceCounterCategoryAttribute : Attribute
    {
        private string _name;
        private string _info;
        private PerformanceCounterCategoryType _instanceType;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">category name to be shown</param>
        /// <param name="instanceType">category Type (single or multiIntance)</param>
        /// <param name="info">Information to be shown for this category</param>
        /// <seealso cref="PerformanceCounterCategoryType"/>
        public PerformanceCounterCategoryAttribute(string name, PerformanceCounterCategoryType instanceType, string info)
            : base()
        {
            this._name = name;
            this._info = info;
            this._instanceType = instanceType;
        }

        /// <summary>
        /// Get or Set the name of the counter
        /// </summary>
        public string Name { get { return this._name; } }
        /// <summary>
        /// Get or Set information about this category
        /// </summary>
        public string Info { get { return this._info; } }

        /// <summary>
        /// Get category instance type
        /// </summary>
        public PerformanceCounterCategoryType InstanceType { get { return this._instanceType; } }
    }
}
