using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Profile;

namespace Lansh.Common.Util
{
    public class DeviceFamily
    {


        public static bool IsDesktop()
        {
            string device = AnalyticsInfo.VersionInfo.DeviceFamily;
            if ("Windows.Desktop" == device)
                return true;
            else
                return false;
        }

        public static bool IsMobile()
        {
            string device = AnalyticsInfo.VersionInfo.DeviceFamily;
            if ("Windows.Mobile" == device)
                return true;
            else
                return false;
        }

    }
}
