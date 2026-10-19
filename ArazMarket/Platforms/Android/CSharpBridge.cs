using Android.Webkit;
using Java.Interop;
using System;
using System.IO;

namespace ArazMarket.Platforms.Android
{
    public class CSharpBridge : Java.Lang.Object
    {
        [JavascriptInterface]
        [Export("SaveFile")]
        public void SaveFile(string fileName, string base64Data)
        {
            try
            {
                // حذف پیشوند data:...;base64,
                var base64 = base64Data.Substring(base64Data.IndexOf(",") + 1);
                var bytes = Convert.FromBase64String(base64);
                
                var downloadsPath = global::Android.OS.Environment.GetExternalStoragePublicDirectory(
                    global::Android.OS.Environment.DirectoryDownloads).AbsolutePath;
                var filePath = Path.Combine(downloadsPath, fileName);
                
                File.WriteAllBytes(filePath, bytes);
            }
            catch (Exception ex)
            {
                // مدیریت خطا
            }
        }
    }
}