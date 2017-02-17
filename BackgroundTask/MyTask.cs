using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace BackgroundTask
{
    public sealed class MyTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            XmlDocument doc = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);
            //TODO
            ToastNotification toast = new ToastNotification(doc);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
            deferral.Complete();
        }
    }
}
