using System;
#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif
using UnityEngine;
public static class WebGLCheckBrowser
{
    #if UNITY_WEBGL
    [DllImport("__Internal")]
    static extern bool IsMobilePlatform();

    public static bool IsMobileBrowser()
    {
#if UNITY_EDITOR
        return false; // value to return in Play Mode (in the editor)
#elif UNITY_WEBGL
        return IsMobilePlatform(); // value based on the current browser
#else
        return false;
#endif
    }
#endif
}
