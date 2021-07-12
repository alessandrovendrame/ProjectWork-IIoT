#pragma checksum "C:\Users\Fluox\Documents\Project-Work\sln\RoomTech.CloudManager\RoomTech.CloudManager.Web\Pages\Dashboard\Dashboard.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e96ca77476f5fb826e7e5a091ca0a73a682cd3d1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(RoomTech.CloudManager.Web.Pages.Dashboard.Pages_Dashboard_Dashboard), @"mvc.1.0.razor-page", @"/Pages/Dashboard/Dashboard.cshtml")]
namespace RoomTech.CloudManager.Web.Pages.Dashboard
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Fluox\Documents\Project-Work\sln\RoomTech.CloudManager\RoomTech.CloudManager.Web\Pages\_ViewImports.cshtml"
using RoomTech.CloudManager.Web;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemMetadataAttribute("RouteTemplate", "/Dashboard")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e96ca77476f5fb826e7e5a091ca0a73a682cd3d1", @"/Pages/Dashboard/Dashboard.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f6dbf0a864e4db2e6fee30e6f50938296bd93045", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Dashboard_Dashboard : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Fluox\Documents\Project-Work\sln\RoomTech.CloudManager\RoomTech.CloudManager.Web\Pages\Dashboard\Dashboard.cshtml"
  
    ViewData["Title"] = "Dashboard";


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 8 "C:\Users\Fluox\Documents\Project-Work\sln\RoomTech.CloudManager\RoomTech.CloudManager.Web\Pages\Dashboard\Dashboard.cshtml"
 if (Model.TodayLessons.Count == 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h2 class=\"mt-4\" style=\"text-align: center\">Today there aren\'t any lessons.</h2>\r\n");
#nullable restore
#line 11 "C:\Users\Fluox\Documents\Project-Work\sln\RoomTech.CloudManager\RoomTech.CloudManager.Web\Pages\Dashboard\Dashboard.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h2 class=\"mt-4\" style=\"text-align: center\">Today\'s lessons</h2>\r\n");
#nullable restore
#line 15 "C:\Users\Fluox\Documents\Project-Work\sln\RoomTech.CloudManager\RoomTech.CloudManager.Web\Pages\Dashboard\Dashboard.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"card border-0\" style=\"align-items: center; background-color: transparent;\">\r\n    <div class=\"card-body\">\r\n");
#nullable restore
#line 19 "C:\Users\Fluox\Documents\Project-Work\sln\RoomTech.CloudManager\RoomTech.CloudManager.Web\Pages\Dashboard\Dashboard.cshtml"
         foreach (var l in Model.TodayLessons)
        {


#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"card border-primary mt-3 mb-3 mr-3\" style=\"width: 275px; float: left;\">\r\n                <div class=\"card-header\" style=\"text-align: center\"><b>Classroom: ");
#nullable restore
#line 23 "C:\Users\Fluox\Documents\Project-Work\sln\RoomTech.CloudManager\RoomTech.CloudManager.Web\Pages\Dashboard\Dashboard.cshtml"
                                                                             Write(l.Classroom);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></div>\r\n                <div class=\"card-body\">\r\n                    <p style=\"color: black\" class=\"card-text ml-2\">Date: <b>");
#nullable restore
#line 25 "C:\Users\Fluox\Documents\Project-Work\sln\RoomTech.CloudManager\RoomTech.CloudManager.Web\Pages\Dashboard\Dashboard.cshtml"
                                                                       Write(l.Date.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></p>\r\n                    <p style=\"color: black\" class=\"card-text ml-2\">Start: <b>");
#nullable restore
#line 26 "C:\Users\Fluox\Documents\Project-Work\sln\RoomTech.CloudManager\RoomTech.CloudManager.Web\Pages\Dashboard\Dashboard.cshtml"
                                                                        Write(l.StartTime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></p>\r\n                    <p style=\"color: black\" class=\"card-text ml-2\">Subject: <b>");
#nullable restore
#line 27 "C:\Users\Fluox\Documents\Project-Work\sln\RoomTech.CloudManager\RoomTech.CloudManager.Web\Pages\Dashboard\Dashboard.cshtml"
                                                                          Write(l.Subject);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></p>\r\n                    <p style=\"color: black\" class=\"card-text ml-2\">Teacher: <b>");
#nullable restore
#line 28 "C:\Users\Fluox\Documents\Project-Work\sln\RoomTech.CloudManager\RoomTech.CloudManager.Web\Pages\Dashboard\Dashboard.cshtml"
                                                                          Write(l.Teacher);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></p>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 31 "C:\Users\Fluox\Documents\Project-Work\sln\RoomTech.CloudManager\RoomTech.CloudManager.Web\Pages\Dashboard\Dashboard.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DashboardModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<DashboardModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<DashboardModel>)PageContext?.ViewData;
        public DashboardModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591