
using AngleSharp.Html.Dom;

namespace Task_3.Interfaces
{
    internal interface IParser<T> where T : class
    {
        T Parse (IHtmlDocument document);
    }
}
