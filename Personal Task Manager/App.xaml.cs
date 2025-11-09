using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace Personal_Task_Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string MutexName = "PersonalTaskManager_SingleInstance_Mutex_v1";
        private Mutex? _instanceMutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            bool createdNew;
            try
            {
                _instanceMutex = new Mutex(true, MutexName, out createdNew);
            }
            catch
            {
                // If mutex creation fails for any reason, attempt to bring existing instance to front
                BringExistingInstanceToFront();
                Shutdown();
                return;
            }

            if (!createdNew)
            {
                BringExistingInstanceToFront();
                Shutdown();
                return;
            }

            base.OnStartup(e);

            // Create and show main window manually (removed StartupUri from App.xaml)
            var mainWindow = new MainWindow();
            MainWindow = mainWindow;
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            try
            {
                _instanceMutex?.ReleaseMutex();
                _instanceMutex?.Dispose();
                _instanceMutex = null;
            }
            catch
            {
                // ignore
            }
        }

        private void BringExistingInstanceToFront()
        {
            try
            {
                var current = Process.GetCurrentProcess();
                var processes = Process.GetProcessesByName(current.ProcessName);

                foreach (var p in processes)
                {
                    if (p.Id == current.Id)
                        continue;

                    IntPtr hWnd = p.MainWindowHandle;
                    if (hWnd == IntPtr.Zero)
                        continue;

                    if (IsIconic(hWnd))
                        ShowWindowAsync(hWnd, SW_RESTORE);

                    SetForegroundWindow(hWnd);
                    return;
                }

                // Fallback: if no MainWindowHandle found, try to find top-level window by process id
                foreach (var p in processes)
                {
                    if (p.Id == current.Id)
                        continue;

                    IntPtr found = FindMainWindowForProcess(p.Id);
                    if (found != IntPtr.Zero)
                    {
                        if (IsIconic(found))
                            ShowWindowAsync(found, SW_RESTORE);

                        SetForegroundWindow(found);
                        return;
                    }
                }
            }
            catch
            {
                // ignore failures - just exit
            }
        }

        private IntPtr FindMainWindowForProcess(int pid)
        {
            IntPtr result = IntPtr.Zero;
            bool Callback(IntPtr hWnd, IntPtr lParam)
            {
                if (!IsWindowVisible(hWnd))
                    return true;

                GetWindowThreadProcessId(hWnd, out uint windowPid);
                if (windowPid == (uint)pid)
                {
                    result = hWnd;
                    return false; // stop enumeration
                }
                return true;
            }

            EnumWindows((wnd, param) => Callback(wnd, param), IntPtr.Zero);
            return result;
        }

        #region Win32 interop
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private const int SW_RESTORE = 9;
        #endregion
    }
}
