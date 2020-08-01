using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Lansh.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Feedback : Page
    {
        public Feedback()
        {
            this.InitializeComponent();
            Loaded += async (s, e) =>
            {
                var mailto = new Uri($"mailto:1019452620@qq.com?subject=Feedback&body=听说你对我的应用不满意，来聊一聊！");
                await Launcher.LaunchUriAsync(mailto);
            };
        }
    }
}
