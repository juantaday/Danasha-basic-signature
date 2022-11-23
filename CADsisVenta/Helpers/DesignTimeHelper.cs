using System.ComponentModel;
using System.Diagnostics;

namespace CADsisVenta.Helpers
{
    public class DesignTimeHelper
    {
        public static bool IsInDesignMode
        {
            get
            {
                bool isInDesignMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime || Debugger.IsAttached == true;
                if (!isInDesignMode)
                {
                    using (var process = Process.GetCurrentProcess())
                    {
                        return process.ProcessName.ToLowerInvariant().Contains("devenv");
                    }
                }
                return isInDesignMode;
            }
        }
    }
}
