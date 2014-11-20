using System;

namespace WebKitHtmlToPdf.Util
{
    internal delegate TResult Func<TResult>();
    internal delegate TResult Func<T, TResult>(T t);
    internal delegate void Action();
}
