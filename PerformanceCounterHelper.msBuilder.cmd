@echo off
%windir%\microsoft.net\framework\v3.5\msbuild.exe PerformanceCounterHelper.xml /t:Clean,Build,Publish
rem %windir%\microsoft.net\framework\v3.5\msbuild.exe PerformanceCounterHelper.xml /t:Clean,Build,Publish
pause
