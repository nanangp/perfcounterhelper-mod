using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using PerformanceCounterHelper.Factory;

namespace PerformanceCounterHelper
{
    /// <summary>
    /// Helper to create and managed performance counters to monitor applications.
    /// It recieves an enum type that needs to have PerformanceCounterCategoryAttribute attribute and PerformanceCounterAttribute on each item contained with information about the category and types of counters.
    /// </summary>
    /// <seealso cref="PerformanceCounterCategoryAttribute"/>
    /// <seealso cref="PerformanceCounterAttribute"/>
    /// <exception cref="System.NotSupportedException" />
    public static class PerformanceHelper
    {
        /// <summary>
        /// Get all enums declared on that assembly that defines PerformanceCounterCategoryAttribute and PerformanceCounterAttribute
        /// </summary>
        /// <param name="assembly">Assembly that holds performance Counters definition</param>
        /// <returns>returns an Array of Enumerators that holds Performance Counters information</returns>
        public static Type[] GetPerformanceCounterFromAssembly(Assembly assembly)
        {
            List<Type> performanceCounterEnums = new List<Type>();
            Type[] types = null;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                // ignores this error, cause I'm only loading this assembly and not the ones referenced by it.
                Trace.WriteLine(ex.Message);
                types = ex.Types;
            }

            if (types != null)
            {
                foreach (Type possibleCounter in types)
            {
                    if (possibleCounter == null)
                        continue;
                if (!possibleCounter.IsEnum)
                    continue;

                if (GetCategoryAttribute(possibleCounter) == null)
                    continue;

                performanceCounterEnums.Add(possibleCounter);
                }
            }
            else
            {
                return new Type[0];
            }
            return performanceCounterEnums.ToArray();
        }
        /// <summary>
        /// Uninstall a category of performance counters defined in this Enumerator
        /// </summary>
        /// <param name="typeT">enumerator that holds counters and defines PerformanceCounterCategoryAttribute and PerformanceCounterAttribute</param>
        /// <seealso cref="PerformanceCounterCategoryAttribute"/>
        /// <seealso cref="PerformanceCounterAttribute"/>
        /// <exception cref="System.NotSupportedException" />
        public static void Uninstall(Type typeT)
        {
            if (!typeT.IsEnum)
                throw new NotSupportedException(
                    String.Format(
                    System.Globalization.CultureInfo.CurrentUICulture,
                    Properties.Resources.PerformanceHelper_EnumTypeErrorMessage,
                    typeT.Name));

            PerformanceCounterCategoryAttribute categoryInfo = GetCategoryAttribute(typeT);

            if (categoryInfo == null)
                throw new NotSupportedException(
                    String.Format(
                    System.Globalization.CultureInfo.CurrentUICulture,
                    Properties.Resources.PerformanceHelper_EnumTypeNoPerformanceCounterCategoryAttributeErrorMessage,
                    typeT.Name));

            if (PerformanceCounterCategory.Exists(categoryInfo.Name))
                PerformanceCounterCategory.Delete(categoryInfo.Name);

        }
        /// <summary>
        /// Install a category of performance counters using the information on the enumerator 
        /// </summary>
        /// <param name="typeT">enumerator that contains information on the performance counters</param>
        /// <seealso cref="PerformanceCounterCategoryAttribute"/>
        /// <seealso cref="PerformanceCounterAttribute"/>
        public static void Install(Type typeT)
        {
            if (!typeT.IsEnum)
                throw new NotSupportedException(
                    String.Format(
                    System.Globalization.CultureInfo.CurrentUICulture,
                    Properties.Resources.PerformanceHelper_EnumTypeErrorMessage,
                    typeT.Name));

            PerformanceCounterCategoryAttribute categoryInfo = GetCategoryAttribute(typeT);

            if (categoryInfo == null)
                throw new NotSupportedException(
                    String.Format(
                    System.Globalization.CultureInfo.CurrentUICulture,
                    Properties.Resources.PerformanceHelper_EnumTypeNoPerformanceCounterCategoryAttributeErrorMessage,
                    typeT.Name));

            Array enumValues = Enum.GetValues(typeT);
            /* removed cause i tested and now it supports more than 255
            if (enumValues.Length > 255)
                throw new NotSupportedException(
                    String.Format(
                    System.Globalization.CultureInfo.CurrentUICulture,
                    "{0}'s length must be less than or equal to 255.",
                    typeT.Name));
            */

            if (PerformanceCounterCategory.Exists(categoryInfo.Name))
                PerformanceCounterCategory.Delete(categoryInfo.Name);

            CounterCreationDataCollection categoryCounters = new CounterCreationDataCollection();

            PerformanceCounterAttribute attr;
            foreach(object performanceCounter in enumValues)
            {
                attr = GetCounterAttribute(typeT, performanceCounter);
                if (attr != null)
                {
                    if (IsBaseType(attr.CounterType))
                        throw new NotSupportedException(
                            String.Format(Properties.Resources.PerformanceHelper_BaseTypeSupportedInternalErrorMessage,
                            attr.Name));

                    CounterCreationData counterData = new CounterCreationData(attr.Name, attr.Info, attr.CounterType);
                    categoryCounters.Add(counterData);
                    PerformanceCounterType? baseType = GetBaseType(counterData.CounterType);
                    if (baseType != null)
                    {
                        categoryCounters.Add(new CounterCreationData(GetCounterNameForBaseType(attr.Name),
                            string.Format(Properties.Resources.PerformanceHelper_BaseCounterDescription, attr.Name),
                            (PerformanceCounterType) baseType));
                    }
                }
            }
            if (categoryCounters.Count > 0)
            {
                PerformanceCounterCategory.Create(categoryInfo.Name, categoryInfo.Info, categoryInfo.InstanceType, categoryCounters);
            }
        }
        /// <summary>
        /// Create an instance of CounterHelper to manage performance counters defined on T
        /// </summary>
        /// <typeparam name="T">enumerator that holds performance counter information</typeparam>
        /// <seealso cref="PerformanceCounterCategoryAttribute"/>
        /// <seealso cref="PerformanceCounterAttribute"/>
        /// <returns>returns an instance of CounterHelper</returns>
        public static CounterHelper<T> CreateCounterHelper<T>()
        {
            return CreateCounterHelper<T>(null);
        }
        /// <summary>
        /// Create an instance of CounterHelper to manage performance counters defined on T defininig an instance name for multi-instance counters.
        /// </summary>
        /// <typeparam name="T">enumerator that holds performance counter information</typeparam>
        /// <param name="instanceName">instance name to be used on multi-instance counters</param>
        /// <seealso cref="PerformanceCounterCategoryAttribute"/>
        /// <seealso cref="PerformanceCounterAttribute"/>
        /// <returns>returns an instance of CounterHelper</returns>
        public static CounterHelper<T> CreateCounterHelper<T>(string instanceName)
        {
            Type typeT = typeof(T);
            if (!typeT.IsEnum)
                throw new NotSupportedException(
                    String.Format(
                    System.Globalization.CultureInfo.CurrentUICulture,
                    Properties.Resources.PerformanceHelper_EnumTypeErrorMessage,
                    typeT.Name));

            PerformanceCounterCategoryAttribute categoryInfo = GetCategoryAttribute(typeT);

            if (categoryInfo == null)
                throw new NotSupportedException(
                    String.Format(
                    System.Globalization.CultureInfo.CurrentUICulture,
                    Properties.Resources.PerformanceHelper_EnumTypeNoPerformanceCounterCategoryAttributeErrorMessage,
                    typeT.Name));
            

            Array enumValues = Enum.GetValues(typeT);

            Dictionary<T, PerformanceCounterAttribute> enumCounterAttributes = new Dictionary<T, PerformanceCounterAttribute>();
            PerformanceCounterAttribute attr;
            foreach (T performanceCounter in enumValues)
            {
                attr = GetCounterAttribute(typeT, performanceCounter);
                if (attr != null)
                {
                    enumCounterAttributes.Add(performanceCounter, attr);
                }
            }

            if (PerformanceCounterCategory.Exists(categoryInfo.Name))
            {
                CounterHelper<T> counterHelper;
                if (categoryInfo.InstanceType == PerformanceCounterCategoryType.MultiInstance)
                {
                    
                    if (string.IsNullOrEmpty(instanceName))
                        instanceName = string.Format("{0}_{1}",AppDomain.CurrentDomain.FriendlyName, Process.GetCurrentProcess().Id);
                    counterHelper = CounterHelperFactory.Create<T>(instanceName, categoryInfo, enumCounterAttributes);
                }
                else
                {
                    counterHelper = CounterHelperFactory.Create<T>(categoryInfo, enumCounterAttributes);
                }
                return counterHelper;
            }
            return null;
        }
        /// <summary>
        /// Get PerformanceCounterCategoryAttribute attached to an Enumeration
        /// </summary>
        /// <param name="enumType">enumerator</param>
        /// <returns>returns an instance of PerformanceCounterCategoryAttribute in case the attribute is found, otherwise null</returns>
        /// <seealso cref="PerformanceCounterCategoryAttribute"/>
        /// <exception cref="System.NotSupportedException" />
        private static PerformanceCounterCategoryAttribute GetCategoryAttribute(Type enumType)
        {
            if (enumType == null)
                throw new NotSupportedException(Properties.Resources.PerformanceHelper_EnumTypeCannotBeNullErrorMessage);

            PerformanceCounterCategoryAttribute attr = Attribute.GetCustomAttribute(enumType, typeof(PerformanceCounterCategoryAttribute)) as PerformanceCounterCategoryAttribute;
            return attr;
        }
        /// <summary>
        /// Get PerformanceCounterAttribute attached to an item within an Enumeration
        /// </summary>
        /// <param name="enumType">enumerator</param>
        /// <param name="enumValue">value withing the enum</param>
        /// <returns>returns an instance of PerformanceCounterAttribute in case the attribute is found, otherwise null</returns>
        /// <seealso cref="PerformanceCounterAttribute"/>
        /// <exception cref="System.NotSupportedException" />
        private static PerformanceCounterAttribute GetCounterAttribute(Type enumType, object enumValue)
        {
            if (enumType == null)
                throw new NotSupportedException(Properties.Resources.PerformanceHelper_EnumTypeCannotBeNullErrorMessage);
            if (enumValue == null)
                throw new NotSupportedException(Properties.Resources.PerformanceHelper_EnumValueCannotBeNullErrorMessage);

            //TODO:Se podría evitar el ToString del enumValue?
            FieldInfo fieldInfo = enumType.GetField(enumValue.ToString());
            PerformanceCounterAttribute[] attributes = fieldInfo.GetCustomAttributes(typeof(PerformanceCounterAttribute), false) as PerformanceCounterAttribute[];
            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0];
            return null;
        }
        /// <summary>
        /// Get the PerformanceCounterType of the base type associated to this counter
        /// </summary>
        /// <param name="type">PerformanceCounterType of the counter to be added</param>
        /// <returns>returns PerformanceCounterType for the base counter in case the PerformanceCounterType passed as parameter requires a base counter, otherwise null</returns>
        /// <seealso cref="PerformanceCounterType"/>
        internal static PerformanceCounterType? GetBaseType(PerformanceCounterType type)
        {
            PerformanceCounterType? rtnValue = null;

            switch (type)
            {
                case PerformanceCounterType.AverageCount64:
                case PerformanceCounterType.AverageTimer32:
                    rtnValue = PerformanceCounterType.AverageBase;
                    break;
                case PerformanceCounterType.RawFraction:
                    rtnValue = PerformanceCounterType.RawBase;
                    break;
                case PerformanceCounterType.CounterMultiTimer:
                case PerformanceCounterType.CounterMultiTimerInverse:
                case PerformanceCounterType.CounterMultiTimer100Ns:
                case PerformanceCounterType.CounterMultiTimer100NsInverse:
                    rtnValue = PerformanceCounterType.CounterMultiBase;
                    break;
                case PerformanceCounterType.SampleCounter:
                case PerformanceCounterType.SampleFraction:
                    rtnValue = PerformanceCounterType.SampleBase;
                    break;
                default:
                    rtnValue = null;
                    break;
            }

            return rtnValue;
        }
        /// <summary>
        /// Returns the counter name for the counter base given the counter name that needs a base
        /// </summary>
        /// <param name="counterName">counter name of the counter that needs a base</param>
        /// <returns>counter name to be used on the base counter</returns>
        internal static string GetCounterNameForBaseType(string counterName)
        {
            return string.Concat(counterName, Properties.Resources.PerformanceHelper_BaseTypeNameAddon);
        }
        /// <summary>
        /// Returns if a PerformanceCounterType is a base type for another counter type
        /// (AverageBase, CounterMultiBase, RawBase y SampleBase are Base Types)
        /// </summary>
        /// <param name="counterType">PerformanceCounterType</param>
        /// <returns>returns true if the type is a based counter type, otherwise false</returns>
        private static bool IsBaseType(PerformanceCounterType counterType)
        {
            return (counterType == PerformanceCounterType.AverageBase)
                || (counterType == PerformanceCounterType.CounterMultiBase)
                || (counterType == PerformanceCounterType.RawBase)
                || (counterType == PerformanceCounterType.SampleBase);
        }

        internal static long getInitialValue(PerformanceCounterType performanceCounterType)
        {
            switch (performanceCounterType)
            {
                case PerformanceCounterType.AverageTimer32:
                    return Stopwatch.GetTimestamp();
                default:
                    return 0;
            }
        }
    }
}
