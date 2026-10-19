using Android.App;
using Android.Webkit;
using System;

namespace ArazMarket.Platforms.Android
{
    public class MyDownloadListener : Java.Lang.Object, IDownloadListener
    {
        public void OnDownloadStart(string? url, string? userAgent, string? contentDisposition, string? mimetype, long contentLength)
        {
            try
            {
                if (string.IsNullOrEmpty(url) || url.StartsWith("blob:")) return;

                var request = new DownloadManager.Request(global::Android.Net.Uri.Parse(url));
                if (!string.IsNullOrEmpty(mimetype)) request.SetMimeType(mimetype);
                request.SetNotificationVisibility(DownloadVisibility.VisibleNotifyCompleted);
                request.SetDestinationInExternalPublicDir(global::Android.OS.Environment.DirectoryDownloads, 
                    global::Android.Net.Uri.Parse(url).LastPathSegment ?? "download");

                var dm = (DownloadManager)global::Android.App.Application.Context.GetSystemService(global::Android.Content.Context.DownloadService);
                dm.Enqueue(request);
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("Download Error: " + ex.Message); }
        }
    }
}
