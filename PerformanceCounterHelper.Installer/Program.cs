using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using PerformanceCounterHelper;

namespace PerformanceCounterHelper.Installer
{
    internal enum Operation
    {
        install = 1,
        uninstall
    }

    class Program
    {
        private static string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        static void Main(string[] args)
        {
            Operation operation = Operation.install;
            string assemblyFileName = null;
            Console.WriteLine(Properties.Resources.Console_Title, GetAssemblyVersion());
            if ((args.Length < 1) || (args.Length > 2))
            {
                Console.WriteLine(Properties.Resources.Console_Help);
                Environment.Exit(-1);
            }
            else
            {
                if (string.Compare(args[0], "/u", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    operation = Operation.uninstall;
                    assemblyFileName = args[1];
                }
                else
                {
                    assemblyFileName = args[0];
                }
            }
            Assembly assemblyLoaded = null;
            try
            {
                assemblyLoaded = Assembly.LoadFile(assemblyFileName);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(Properties.Resources.Console_FileDoesNotExistErrorMessage);
            }
            catch (ArgumentException)
            {
                Console.WriteLine(Properties.Resources.Console_FileFullPathIsRequiredErrorMessage);
            }
            catch (System.IO.FileLoadException)
            {
                Console.WriteLine(Properties.Resources.Console_FileNotLoadedErrorMessage);
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine(Properties.Resources.Console_FileNotFoundErrorMessage);
            }
            catch (BadImageFormatException)
            {
                Console.WriteLine(Properties.Resources.Console_AssemblyNotCompatibleErrorMessage);
            }
            if (assemblyLoaded == null)
                Environment.Exit(-1);

            Type[] performanceCounterTypes = PerformanceHelper.GetPerformanceCounterFromAssembly(assemblyLoaded);

            Console.WriteLine(String.Empty);
            Console.WriteLine(Properties.Resources.Console_FoundTypesTitle);
            if (performanceCounterTypes.Length == 0)
                Console.WriteLine(Properties.Resources.Console_NoneTypesFound);
            else
            {
                bool succeded = true;
                foreach (Type performanceCounterType in performanceCounterTypes)
                {
                    Console.WriteLine(Properties.Resources.Console_PerformanceCountersCategoryFoundInformation, performanceCounterType.Namespace, performanceCounterType.Namespace);
                }

                Console.WriteLine(String.Empty);
                foreach (Type performanceCounterType in performanceCounterTypes)
                {

                    try
                    {
                        if (operation == Operation.install)
                        {
                            Console.Write(string.Format(Properties.Resources.Console_InstallingTypeMessage, Properties.Resources.Console_PerformanceCountersCategoryFoundInformation), performanceCounterType.FullName, performanceCounterType.FullName);
                            PerformanceHelper.Install(performanceCounterType);
                        }
                        else
                        {
                            Console.Write(string.Format(Properties.Resources.Console_UninstallingTypeMessage, Properties.Resources.Console_PerformanceCountersCategoryFoundInformation), performanceCounterType.FullName, performanceCounterType.FullName);
                            PerformanceHelper.Uninstall(performanceCounterType);
                        }
                    }
                    catch (Exception oEx)
                    {
                        Console.WriteLine(string.Format(Properties.Resources.Console_Error, oEx.Message));
                        succeded = false;
                        break;
                    }

                    Console.WriteLine(Properties.Resources.Console_OK);
                }

                if (!succeded)
                    Environment.Exit(-1);

                Environment.Exit(0);
            }

        }
    }
}
