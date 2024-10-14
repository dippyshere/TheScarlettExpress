#region

using UnityEditor;

#endregion

namespace FullscreenEditor.Linux
{
    static class NativeFullscreenHooks
    {
        [InitializeOnLoadMethod]
        static void Init()
        {
            if (!FullscreenUtility.IsLinux)
            {
                return;
            }

            FullscreenCallbacks.afterFullscreenOpen += fs =>
            {
                if (wmctrl.IsInstalled && !FullscreenPreferences.DoNotUseWmctrl.Value)
                {
                    wmctrl.SetNativeFullscreen(true, fs.m_dst.Container);
                }
            };
            FullscreenCallbacks.beforeFullscreenClose += fs =>
            {
                if (wmctrl.IsInstalled && !FullscreenPreferences.DoNotUseWmctrl.Value)
                {
                    wmctrl.SetNativeFullscreen(false, fs.m_dst.Container);
                }
            };
        }
    }
}