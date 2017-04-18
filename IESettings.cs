using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace HtmlSkin.Net
{
    public static class IESettings
    {
        private const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
        private const int FEATURE_ZONE_ELEVATION = 1;
        private const int FEATURE_WEBOC_POPUPMANAGEMENT = 5;
        private const int FEATURE_LOCALMACHINE_LOCKDOWN = 8;
        private const int FEATURE_RESTRICT_FILEDOWNLOAD = 12;
        private const int FEATURE_PROTOCOL_LOCKDOWN = 14;
        private const int FEATURE_SECURITYBAND = 9;


        private const int SET_FEATURE_ON_THREAD = 0x00000001;
        private const int SET_FEATURE_ON_PROCESS = 0x00000002;
        private const int SET_FEATURE_IN_REGISTRY = 0x00000004;
        private const int SET_FEATURE_ON_THREAD_LOCALMACHINE = 0x00000008;
        private const int SET_FEATURE_ON_THREAD_INTRANET = 0x00000010;
        private const int SET_FEATURE_ON_THREAD_TRUSTED = 0x00000020;
        private const int SET_FEATURE_ON_THREAD_INTERNET = 0x00000040;
        private const int SET_FEATURE_ON_THREAD_RESTRICTED = 0x00000080;

        [DllImport("urlmon.dll")]
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        public static extern int CoInternetSetFeatureEnabled(
            int FeatureEntry,
            [MarshalAs(UnmanagedType.U4)] int dwFlags,
            bool fEnable);
        public static void DisableSecurityCheck()
        {
            IESettings.CoInternetSetFeatureEnabled(FEATURE_RESTRICT_FILEDOWNLOAD, SET_FEATURE_ON_PROCESS, false);
            IESettings.CoInternetSetFeatureEnabled(FEATURE_ZONE_ELEVATION, SET_FEATURE_ON_PROCESS, false);
            IESettings.CoInternetSetFeatureEnabled(FEATURE_WEBOC_POPUPMANAGEMENT, SET_FEATURE_ON_PROCESS, false);
            IESettings.CoInternetSetFeatureEnabled(FEATURE_LOCALMACHINE_LOCKDOWN, SET_FEATURE_ON_PROCESS, false);
            IESettings.CoInternetSetFeatureEnabled(FEATURE_PROTOCOL_LOCKDOWN, SET_FEATURE_ON_PROCESS, false);
            IESettings.CoInternetSetFeatureEnabled(FEATURE_SECURITYBAND, SET_FEATURE_ON_PROCESS, false);
        }
    }
}
