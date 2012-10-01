using System;
using System.Collections.Generic;
using System.Text;
using PerformanceCounterHelper.Impl;

namespace PerformanceCounterHelper.Factory
{
    /// <summary>
    /// Factory class to create an implementation of CounterHelper interface
    /// </summary>
    public static class CounterHelperFactory
    {
        /// <summary>
        /// Create a CounterHelper instance to work for instantiated performance counter
        /// </summary>
        /// <typeparam name="T">the enum type holding counter information</typeparam>
        /// <param name="instanceName">name for this instance</param>
        /// <param name="categoryInfo">information about the category for this counter</param>
        /// <param name="enumCounterAttributes">attributes for the enum category</param>
        /// <returns>a created instance of CounterHelper interface to work with the counter</returns>
        public static CounterHelper<T> Create<T>(string instanceName, PerformanceCounterCategoryAttribute categoryInfo, Dictionary<T, PerformanceCounterAttribute> enumCounterAttributes)
        {
            return new CounterHelperImpl<T>(instanceName, categoryInfo, enumCounterAttributes);
        }

        /// <summary>
        /// Create a CounterHelper instance to work for non-instantiated performance counter
        /// </summary>
        /// <typeparam name="T">the enum type holding counter information</typeparam>
        /// <param name="categoryInfo">information about the category for this counter</param>
        /// <param name="enumCounterAttributes">attributes for the enum category</param>
        /// <returns>a created instance of CounterHelper interface to work with the counter</returns>
        public static CounterHelper<T> Create<T>(PerformanceCounterCategoryAttribute categoryInfo, Dictionary<T, PerformanceCounterAttribute> enumCounterAttributes)
        {
            return new CounterHelperImpl<T>(categoryInfo, enumCounterAttributes);
        }
    }
}
