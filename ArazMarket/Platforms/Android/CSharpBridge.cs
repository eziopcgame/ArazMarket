using Android.Webkit;
using Java.Interop;
using System;
using System.IO;

namespace ArazMarket.Platforms.Android
{
    public class CSharpBridge : Java.Lang.Object
    {
        // ذخیره لیست محصولات (به صورت یک فایل ثابت list.json)
        [JavascriptInterface]
        [Export("SaveFile")]
        public void SaveFile(string fileName, string base64Data)
        {
            try
            {
                var base64 = base64Data.Substring(base64Data.IndexOf(",") + 1);
                var bytes = Convert.FromBase64String(base64);
                
                // ساخت مسیر Download/araz market/list
                var downloads = global::Android.OS.Environment.GetExternalStoragePublicDirectory(global::Android.OS.Environment.DirectoryDownloads).AbsolutePath;
                var listDir = Path.Combine(downloads, "araz market", "list");
                if (!Directory.Exists(listDir)) Directory.CreateDirectory(listDir);
                
                var filePath = Path.Combine(listDir, fileName);
                File.WriteAllBytes(filePath, bytes);
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("Save Error: " + ex.Message); }
        }

        // ذخیره رسید (با تاریخ و ساعت)
        [JavascriptInterface]
        [Export("SaveReceipt")]
        public void SaveReceipt(string receiptText)
        {
            try
            {
                // ساخت مسیر Download/araz market/resid
                var downloads = global::Android.OS.Environment.GetExternalStoragePublicDirectory(global::Android.OS.Environment.DirectoryDownloads).AbsolutePath;
                var residDir = Path.Combine(downloads, "araz market", "resid");
                if (!Directory.Exists(residDir)) Directory.CreateDirectory(residDir);

                var fileName = $"receipt_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                var filePath = Path.Combine(residDir, fileName);
                File.WriteAllText(filePath, receiptText);
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("Save Receipt Error: " + ex.Message); }
        }
    }
}
