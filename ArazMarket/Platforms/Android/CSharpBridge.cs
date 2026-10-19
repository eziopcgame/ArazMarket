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

        [JavascriptInterface]
        [Export("SavePdf")]
        public void SavePdf(string base64Data)
        {
            try
            {
                var bytes = Convert.FromBase64String(base64Data);
                var downloads = global::Android.OS.Environment.GetExternalStoragePublicDirectory(global::Android.OS.Environment.DirectoryDownloads).AbsolutePath;
                var residDir = Path.Combine(downloads, "araz market", "resid");
                if (!Directory.Exists(residDir)) Directory.CreateDirectory(residDir);
                var fileName = $"receipt_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                File.WriteAllBytes(Path.Combine(residDir, fileName), bytes);
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("SavePdf Error: " + ex.Message); }
        }

        [JavascriptInterface]
        [Export("SharePdf")]
        public void SharePdf(string base64Data)
        {
            try
            {
                var bytes = Convert.FromBase64String(base64Data);
                var downloads = global::Android.OS.Environment.GetExternalStoragePublicDirectory(global::Android.OS.Environment.DirectoryDownloads).AbsolutePath;
                var residDir = Path.Combine(downloads, "araz market", "resid");
                if (!Directory.Exists(residDir)) Directory.CreateDirectory(residDir);
                var fileName = $"receipt_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                var filePath = Path.Combine(residDir, fileName);
                File.WriteAllBytes(filePath, bytes);

                // 🔴 مشکل از این خط بود که با global:: حل شد
                var uri = global::AndroidX.Core.Content.FileProvider.GetUriForFile(
                    global::Android.App.Application.Context,
                    global::Android.App.Application.Context.PackageName + ".fileprovider",
                    new Java.IO.File(filePath));

                var intent = new global::Android.Content.Intent(global::Android.Content.Intent.ActionSend);
                intent.SetType("application/pdf");
                intent.PutExtra(global::Android.Content.Intent.ExtraStream, uri);
                intent.AddFlags(global::Android.Content.ActivityFlags.GrantReadUriPermission);
                intent.SetFlags(global::Android.Content.ActivityFlags.NewTask);
                
                var chooser = global::Android.Content.Intent.CreateChooser(intent, "اشتراک‌گذاری رسید");
                chooser.SetFlags(global::Android.Content.ActivityFlags.NewTask);
                global::Android.App.Application.Context.StartActivity(chooser);
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("SharePdf Error: " + ex.Message); }
        }
    }
}
