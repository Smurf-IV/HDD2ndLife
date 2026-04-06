#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="Program.cs" company="Smurf-IV">
// 
//  Copyright (C) 2020 - 2026 Simon Coghlan (Aka Smurf-IV)
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//   any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program. If not, see http://www.gnu.org/licenses/.
//  </copyright>
//  <summary>
//  Url: https://github.com/Smurf-IV/HDD2ndLife
//  Email: https://github.com/Smurf-IV
//  </summary>
// --------------------------------------------------------------------------------------------------------------------
#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

using Krypton.Toolkit;

using Exceptionless;

using NLog;

namespace HDD2ndLife;

static class Program
{
    private static readonly Logger s_log = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        try
        {
            ExceptionlessClient.Default.Register();
            // Include the username if available (E.G., Environment.UserName or IIdentity.Name)
            ExceptionlessClient.Default.Configuration.IncludeUserName = false;
            // Include the MachineName in MachineInfo.
            ExceptionlessClient.Default.Configuration.IncludeMachineName = true;
            // Include Ip Addresses in MachineInfo and RequestInfo.
            ExceptionlessClient.Default.Configuration.IncludeIpAddress = false;
            // Include Cookies, please note that DataExclusions are applied to all Cookie keys when enabled.
            ExceptionlessClient.Default.Configuration.IncludeCookies = false;
            // Include Form/POST Data, please note that DataExclusions are only applied to Form data keys when enabled.
            ExceptionlessClient.Default.Configuration.IncludePostData = false;
            // Include Query String information, please note that DataExclusions are applied to all Query String keys when enabled.
            ExceptionlessClient.Default.Configuration.IncludeQueryString = false;

            ExceptionlessClient.Default.Configuration.SetVersion(Assembly.GetExecutingAssembly().GetName().Version);

            AppDomain.CurrentDomain.UnhandledException += (sender, e) => LogUnhandledException(e.ExceptionObject);
        }
        catch (Exception ex)
        {
            try
            {
                s_log.Fatal(ex, @"Failed to attach unhandled exception handler...");
            }
            catch
            {
                // ignored
            }
        }
        try
        {
            s_log.Info("=====================================================================");
            s_log.Info("File Re-opened: Ver :" + Assembly.GetExecutingAssembly().GetName().Version);
            CheckAndRunSingleApp();
        }
        catch (Exception ex)
        {
            s_log.Fatal(ex, "Exception has not been caught by the rest of the application!");
            KryptonMessageBox.Show(ex.Message, "Uncaught Exception - Exiting !");
        }
        finally
        {
            s_log.Info("File Closing");
            s_log.Info("=====================================================================");
        }
    }

    private static void CheckAndRunSingleApp()
    {
        var mutexName = $"{Path.GetFileName(Application.ExecutablePath)} [{Environment.UserName}]";

        // ReSharper disable once UnusedVariable
        using var appUserMutex = new Mutex(true, mutexName, out var grantedOwnership);
        if (grantedOwnership)
        {
            // As timing will be used for "Slow Read / write detection" then "try" to make this as response and above Windows installer as possible
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        else
        {
            KryptonMessageBox.Show($@"{mutexName} is already running", mutexName, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Stop);
            s_log.Error($@"{mutexName} is already running");
        }
    }

    private static void LogUnhandledException(object exceptionObject)
    {
        try
        {
            s_log.Fatal("Unhandled exception.\r\n{0}", exceptionObject);
            var cs = Assembly.GetExecutingAssembly().GetName().Name;
            try
            {
                if (!EventLog.SourceExists(cs))
                {
                    EventLog.CreateEventSource(cs, @"Application");
                }
            }
            catch (Exception sex)
            {
                s_log.Warn(sex);
                cs = @"Application";    // https://stackoverflow.com/questions/25725151/write-to-windows-application-event-log-without-registering-an-event-source
            }
            EventLog.WriteEntry(cs, exceptionObject.ToString(), EventLogEntryType.Error);
            if (exceptionObject is Exception ex)
            {
                s_log.Fatal(ex, "Exception details");
                EventLog.WriteEntry(cs, ex.ToString(), EventLogEntryType.Error);
            }
            else
            {
                s_log.Fatal("Unexpected exception.");
            }
        }
        catch
        {
            // ignored
        }
    }
}