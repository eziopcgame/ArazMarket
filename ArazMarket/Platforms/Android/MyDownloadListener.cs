using Android.App;
using Android.Webkit;
using System;
using System.IO;

namespace ArazMarket.Platforms.Android
{
    public class MyDownloadListener : Java.Lang.Object, IDownloadListener
    {
        public void OnDownloadStart(string url, string userAgent, string contentDisposition, string mimetype, long contentLength)
        {
            try
            {
                // اگر فایل از نوع blob باشد، باید آن را به روش دیگری ذخیره کرد
                if (url.StartsWith("blob:"))
                {
                    // راه‌حل: از JS برای تبدیل blob به Base64 و فراخوانی C# استفاده کنید
                    // (در ادامه توضیح داده شده)
                    return;
                }

                // برای لینک‌های معمولی (HTTP/HTTPS)
                var request = new DownloadManager.Request(global::Android.Net.Uri.Parse(url));
                request.SetMimeType(mimetype);
                request.SetNotificationVisibility(DownloadVisibility.VisibleNotifyCompleted);
                request.SetDestinationInExternalPublicDir(global::Android.OS.Environment.DirectoryDownloads, 
                    global::Android.Net.Uri.Parse(url).LastPathSegment ?? "download");

                var dm = (DownloadManager)global::Android.App.Application.Context.GetSystemService(global::Android.Content.Context.DownloadService);
                dm.Enqueue(request);
            }
            catch (Exception ex)
            {
                // مدیریت خطا
            }
        }
    }
}