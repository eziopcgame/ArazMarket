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
                var base64 = base64Data.Substring(base64Data.IndexOf(",") + 1);
                var bytes = Convert.FromBase64String(base64);
                var downloads = global::Android.OS.Environment.GetExternalStoragePublicDirectory(global::Android.OS.Environment.DirectoryDownloads).AbsolutePath;
                var listDir = Path.Combine(downloads, "araz market", "list");
                if (!Directory.Exists(listDir)) Directory.CreateDirectory(listDir);
                File.WriteAllBytes(Path.Combine(listDir, fileName), bytes);
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("SaveFile Error: " + ex.Message); }
        }

        [JavascriptInterface]
        [Export("SaveReceipt")]
        public void SaveReceipt(string receiptText)
        {
            try
            {
                var downloads = global::Android.OS.Environment.GetExternalStoragePublicDirectory(global::Android.OS.Environment.DirectoryDownloads).AbsolutePath;
                var residDir = Path.Combine(downloads, "araz market", "resid");
                if (!Directory.Exists(residDir)) Directory.CreateDirectory(residDir);
                var fileName = $"receipt_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                File.WriteAllText(Path.Combine(residDir, fileName), receiptText);
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("SaveReceipt Error: " + ex.Message); }
        }
    }
}
