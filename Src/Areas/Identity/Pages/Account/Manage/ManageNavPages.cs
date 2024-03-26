using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DisneyMoviesWatchlist.Src.Areas.Identity.Pages.Account.Manage;
public static class ManageNavPages
{
    public static string Index => "Index";
    public static string ChangePassword => "ChangePassword";
    public static string DownloadPersonalData => "DownloadPersonalData";
    public static string DeletePersonalData => "DeletePersonalData";
    public static string PersonalData => "PersonalData";

    public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);
    public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);
    public static string DownloadPersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DownloadPersonalData);

    public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

    public static string PageNavClass(ViewContext viewContext, string page)
    {
        var activePage = viewContext.ViewData["ActivePage"] as string
            ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
        return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
    }
}
