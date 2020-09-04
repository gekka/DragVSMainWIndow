namespace Gekka.VisualStudio.Extention.DragVSMainWindow
{
    using Microsoft.VisualStudio.Shell;
    using System;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;
    using Task = System.Threading.Tasks.Task;

    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = false)]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.EmptySolution, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.SolutionHasSingleProject, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.SolutionHasMultipleProjects, PackageAutoLoadFlags.BackgroundLoad)]
    [System.Runtime.InteropServices.Guid(VSMainWindowDragPackage.PackageGuidString)]    
    public sealed class VSMainWindowDragPackage : AsyncPackage
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        public const string PackageGuidString = "054ec25b-5be5-4f56-996f-d149aa8ce4cd";

        #region Package Members
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            var tMainWindowTitleBar = Type.GetType("Microsoft.VisualStudio.PlatformUI.MainWindowTitleBar, Microsoft.VisualStudio.Shell.UI.Internal", false);
            if (tMainWindowTitleBar == null)
            {
                return;
            }
            EventManager.RegisterClassHandler(tMainWindowTitleBar, FrameworkElement.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(onMouseRightButtonDown));
        }

        private void onMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var ls = GetAsyncKeyState(System.Windows.Input.KeyInterop.VirtualKeyFromKey(Key.LeftShift));
            var rs = GetAsyncKeyState(System.Windows.Input.KeyInterop.VirtualKeyFromKey(Key.RightShift));
            if (((ls | rs) & 0x8000) == 0)
            {
                return;
            }
            var titleBar = (FrameworkElement)sender;
            var mainWindow = Window.GetWindow(titleBar);
            if (mainWindow.WindowState != WindowState.Normal)
            {
                return;
            }
            e.Handled = true;

            mainWindow.Activate();
            mainWindow.DragMove();
        }
        #endregion
    }
}
