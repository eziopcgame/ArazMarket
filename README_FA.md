# آراز مارکت برای اندروید — C# / .NET MAUI

این پروژه یک پوستهٔ اندرویدی C# با .NET MAUI است. فایل HTML اصلی بدون تغییر در `ArazMarket/Resources/Raw/index.html` قرار گرفته تا ظاهر و منطق فعلی تا حد امکان عیناً حفظ شود. رابط در WebView اجرا می‌شود؛ بنابراین منطق فعلی برنامه هنوز JavaScript است و این نسخه بازنویسی کامل منطق به C# نیست.

## ساخت APK رایگان با GitHub Actions

1. یک مخزن جدید در GitHub بساز.
2. محتویات این پوشه را در ریشهٔ مخزن بارگذاری کن.
3. وارد زبانهٔ **Actions** شو و گردش‌کار **Build Araz Market APK** را انتخاب کن.
4. روی **Run workflow** بزن و منتظر پایان موفق build بمان.
5. در صفحهٔ اجرای workflow، بخش **Artifacts**، فایل `ArazMarket-APK` را دانلود و از ZIP خارج کن. فایل APK داخل آن است.

## ساخت روی ویندوز

.NET 9 SDK و workload اندروید MAUI لازم است. سپس اجرا کن:

```powershell
dotnet workload install maui-android
dotnet publish ArazMarket/ArazMarket.csproj -f net9.0-android -c Release -p:AndroidPackageFormat=apk
```

## نکته‌های مهم

- فایل HTML اصلی تغییر داده نشده است.
- اطلاعات محصولات با `localStorage` خود WebView ذخیره می‌شود؛ حذف داده‌های برنامه می‌تواند آن را پاک کند.
- این پروژه در این محیط با Android SDK کامپایل و روی گوشی آزمایش نشده؛ APK نهایی باید پس از build آزمایش شود.
- رمز ورود موجود در HTML (`araz`) در کد سمت کاربر قرار دارد و امنیت واقعی ایجاد نمی‌کند.
- این پروژه فعلاً C# برای پوستهٔ اندروید است؛ منطق برنامه برای حفظ ظاهر و رفتار فعلی همچنان در HTML/JavaScript اجرا می‌شود.
