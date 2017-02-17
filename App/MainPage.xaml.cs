using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        const string TASK_NAME = "MyTask";
        const string ENTRY_POINT = "BackgroundTask.MyTask";

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void UnRegister(object sender, RoutedEventArgs e)
        {
            var task = BackgroundTaskRegistration.AllTasks.Values.FirstOrDefault(t => t.Name == TASK_NAME);
            if (task != null)
            {
                task.Unregister(true);
                await new Windows.UI.Popups.MessageDialog("卸载成功！").ShowAsync();
            }
        }
        private async void Register(object sender, RoutedEventArgs e)
        {
            BackgroundAccessStatus res = await BackgroundExecutionManager.RequestAccessAsync();
            //if (res == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity || res == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity)
            if (res == BackgroundAccessStatus.AllowedSubjectToSystemPolicy)
            {
                var task = BackgroundTaskRegistration.AllTasks.Values.FirstOrDefault(t => t.Name == TASK_NAME);
                if (task == null)
                {
                    BackgroundTaskBuilder btb = new BackgroundTaskBuilder();
                    btb.Name = TASK_NAME;
                    btb.TaskEntryPoint = ENTRY_POINT;
                    btb.SetTrigger(new TimeTrigger(40, false));
                    btb.AddCondition(new SystemCondition(SystemConditionType.UserPresent));
                    btb.Register();
                    await new Windows.UI.Popups.MessageDialog("注册成功！").ShowAsync();
                }
            }
        }
    }
}
