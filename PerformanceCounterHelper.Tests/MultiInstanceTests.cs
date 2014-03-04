using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using NUnit.Framework;

namespace PerformanceCounterHelper.Tests
{
    /// <summary>
    /// NOTE: Needless to say, these unit tests need to be run 
    /// with Visual Studio (or the test runner) having the 
    /// right privileges to create/destroy perf counters.
    /// </summary>
    [TestFixture]
    public class MultiInstanceTests
    {
        protected const string CategoryName = "***TEST Category Name";
        protected const string AvgCounterName = "***TEST Average Counter";
        protected const string NumCounterName = "***TEST Number Counter";

        [PerformanceCounterCategory(CategoryName, PerformanceCounterCategoryType.MultiInstance, "")]
        protected enum MultiInstanceCategory
        {
            [PerformanceCounter(AvgCounterName, "", PerformanceCounterType.AverageCount64)]
            AverageCounter,

            [PerformanceCounter(NumCounterName, "", PerformanceCounterType.NumberOfItems32)]
            NumberCounter,
        }

        [SetUp]
        public void SetUp()
        {
            PerformanceHelper.Install(typeof(MultiInstanceCategory));
        }
        
        [TearDown]
        public void TearDown()
        {
            PerformanceHelper.Uninstall(typeof(MultiInstanceCategory));            
        }

        [Test]
        [TestCase(5)]
        public void CanCreate_MultiInstanceCounters(int numOfCounters)
        {
            // Arrange & Act
            var helpers = new List<CounterHelper<MultiInstanceCategory>>();
            for (int i = 0; i < numOfCounters; i++)
            {
                var instanceName = GetInstanceName(i);
                helpers.Add(PerformanceHelper.CreateCounterHelper<MultiInstanceCategory>(instanceName));
            }

            // Assert
            for (int i = 0; i < numOfCounters; i++)
            {
                Assert.IsTrue(PerformanceCounterCategory.CounterExists(AvgCounterName, CategoryName));
                Assert.IsTrue(PerformanceCounterCategory.CounterExists(NumCounterName, CategoryName));
                Assert.IsTrue(PerformanceCounterCategory.InstanceExists(GetInstanceName(i), CategoryName));

                var category = PerformanceCounterCategory.GetCategories().First(cat => cat.CategoryName == CategoryName);
                var instanceNames = category.GetInstanceNames().OrderBy(name => name).ToArray();
                Assert.AreEqual(GetInstanceName(i), instanceNames[i]);
            }
        }

        [Test]
        [TestCase(5)]
        public void RemoveInstancesAndDispose_MultiInstanceCounters_Works(int numOfCounters)
        {
            // Arrange
            var helpers = new List<CounterHelper<MultiInstanceCategory>>();
            for (int i = 0; i < numOfCounters; i++)
            {
                var instanceName = GetInstanceName(i);
                helpers.Add(PerformanceHelper.CreateCounterHelper<MultiInstanceCategory>(instanceName));
            }

            //Act
            helpers.ForEach(helper => helper.RemoveInstancesAndDispose());

            // Assert
            for (int i = 0; i < numOfCounters; i++)
            {
                // The counters still exist
                Assert.IsTrue(PerformanceCounterCategory.CounterExists(AvgCounterName, CategoryName));
                Assert.IsTrue(PerformanceCounterCategory.CounterExists(NumCounterName, CategoryName));

                // The instances should NOT
                Assert.IsFalse(
                    PerformanceCounterCategory.InstanceExists(GetInstanceName(i), CategoryName), 
                    string.Format("{0} should not exist anymore", GetInstanceName(i)));

                var category = PerformanceCounterCategory.GetCategories().First(cat => cat.CategoryName == CategoryName);
                var instanceNames = category.GetInstanceNames().OrderBy(name => name).ToArray();
                Assert.AreEqual(0, instanceNames.Count(), "There should be no instances left"); 
            }
        }

        [Test]
        [TestCase(5)]
        [TestCase(1000)]
        [TestCase(5000)]
        public void RemoveInstancesAndDispose_MultiInstanceCounters_FreesMemory(int numOfCounters)
        {
            Assert.DoesNotThrow(
                () => CreateAndCleanup(numOfCounters, h => h.RemoveInstancesAndDispose()),
                "Should not run out of memory (or throw any exception) when creating many instances after others have been Removed.");
        }

        [Test]
        [TestCase(5000)]
        public void OnlyDispose_MultiInstanceCounters_ThrowsException_OutOfMemory(int numOfCounters)
        {
            Assert.Throws<InvalidOperationException>(
                () => CreateAndCleanup(numOfCounters, h => h.Dispose()),
                "Expecting InvalidOperationException (Custom counters file view is out of memory).");
        }

        private void CreateAndCleanup(int countersToCreate, Action<CounterHelper<MultiInstanceCategory>> cleanupMethod)
        {
            for (int i = 0; i < countersToCreate; i++)
            {
                // Arrange
                var instanceName = GetInstanceName(i);
                var helper = PerformanceHelper.CreateCounterHelper<MultiInstanceCategory>(instanceName);
                
                //Act
                cleanupMethod(helper);
            }
        }

        private static string GetInstanceName(int i)
        {
            return string.Format("instance{0}", i);
        }
    }
}
