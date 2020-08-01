using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Media.SpeechRecognition;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Lansh.View;

namespace Lansh
{

    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            RenderTitleBar();
            RenderStatusBar();
            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // 当导航堆栈尚未还原时，导航到第一页，
                    // 并通过将所需信息作为导航参数传入来配置
                    // 参数
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // 确保当前窗口处于活动状态
                Window.Current.Activate();
            }

            // Install command
            try
            {
                StorageFile storageFile = await Package.Current.InstalledLocation.GetFileAsync(@"Command\LanshVoiceCommand.xml");
                await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(storageFile);
            }
            catch (Exception)
            {
                System.Console.WriteLine(e);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnActivated(IActivatedEventArgs e)
        {
            base.OnActivated(e);
            Type navigationToPageType;
            string key = null;

            if (e.Kind == ActivationKind.VoiceCommand)
            {
                VoiceCommandActivatedEventArgs voiceCommandArgs = e as VoiceCommandActivatedEventArgs;
                SpeechRecognitionResult speechRecognitionResult = voiceCommandArgs.Result;

                string commandName = speechRecognitionResult.RulePath[0];
                string voiceCommandText = speechRecognitionResult.Text;

                switch (commandName)
                {
                    case "searchVideo":
                        //string key = speechRecognitionResult.SemanticInterpretation.Properties["*"].FirstOrDefault();
                        key = "搞笑";
                        navigationToPageType = typeof(MainPage);
                        //rootFrame.Navigate(typeof(MainPage), key);
                        break;
                    default:
                        navigationToPageType = typeof(MainPage);
                        break;
                }
            }
            else if (e.Kind == ActivationKind.Protocol)
            {
                navigationToPageType = typeof(MainPage);
            }
            else
            {
                navigationToPageType = typeof(MainPage);
            }

            RenderTitleBar();
            RenderStatusBar();
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                //App.NavigationService = new NavigationService(rootFrame);

                rootFrame.NavigationFailed += OnNavigationFailed;

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            // Since we're expecting to always show a details page, navigate even if 
            // a content frame is in place (unlike OnLaunched).
            // Navigate to either the main trip list page, or if a valid voice command
            // was provided, to the details page for that trip.
            rootFrame.Navigate(navigationToPageType, key);

            // Ensure the current window is active
            Window.Current.Activate();

        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }

        public void RenderTitleBar()
        {
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = Color.FromArgb(255, 154, 0, 137); //purple
            titleBar.ForegroundColor = Colors.White;
            titleBar.InactiveBackgroundColor = Color.FromArgb(127, 154, 0, 137);
            titleBar.InactiveForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = Color.FromArgb(255, 154, 0, 137);
            titleBar.ButtonForegroundColor = Colors.White;
            titleBar.ButtonInactiveBackgroundColor = Color.FromArgb(127, 154, 0, 137);
            titleBar.ButtonInactiveForegroundColor = Colors.White;
            titleBar.ButtonHoverBackgroundColor = Color.FromArgb(255, 232, 17, 35); //red
            titleBar.ButtonHoverForegroundColor = Colors.White;
            titleBar.ButtonPressedBackgroundColor = Color.FromArgb(255, 241, 112, 122);
            titleBar.ButtonPressedForegroundColor = Colors.Black;
        }

        public async void RenderStatusBar()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                StatusBar statusbar = StatusBar.GetForCurrentView();
                statusbar.BackgroundColor = Color.FromArgb(255, 154, 0, 137);
                statusbar.ForegroundColor = Colors.White;
                statusbar.BackgroundOpacity = 1;
                await statusbar.ShowAsync();
            }
        }

    }
}
