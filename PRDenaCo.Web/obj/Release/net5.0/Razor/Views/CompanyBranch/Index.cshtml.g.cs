#pragma checksum "C:\DumiSoft_Web\DumiSoft.Web\Views\CompanyBranch\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "845abe0108011e76409f8ae97f7a08f3892bae77"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CompanyBranch_Index), @"mvc.1.0.view", @"/Views/CompanyBranch/Index.cshtml")]
namespace AspNetCore
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
#line 1 "C:\DumiSoft_Web\DumiSoft.Web\Views\_ViewImports.cshtml"
using DumiSoft.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\DumiSoft_Web\DumiSoft.Web\Views\_ViewImports.cshtml"
using DumiSoft.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"845abe0108011e76409f8ae97f7a08f3892bae77", @"/Views/CompanyBranch/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0109afd5f38b40a0fd4c1be669680d9471e7874d", @"/Views/_ViewImports.cshtml")]
    public class Views_CompanyBranch_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DumiSoft.Web.Models.Companies.CompanyBranchModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\DumiSoft_Web\DumiSoft.Web\Views\CompanyBranch\Index.cshtml"
   Layout = null; 

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\DumiSoft_Web\DumiSoft.Web\Views\CompanyBranch\Index.cshtml"
   ViewData["Title"] = "Index"; 

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"card\">\r\n    <div class=\"card-body\">\r\n        <div id=\"view-allL2\">\r\n            ");
#nullable restore
#line 7 "C:\DumiSoft_Web\DumiSoft.Web\Views\CompanyBranch\Index.cshtml"
       Write(await Html.PartialAsync("_ViewAll", Model));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        </div>
    </div>
</div>
<div class=""form-group"">
    <div class=""row"">
        <div class=""col-md-8"">

        </div>
        <div class=""col-md-2"">

        </div>
        <div class=""col-md-2 text-center"">
            <input type=""button"" value=""Close"" class=""btn btn-primary btn-block"" onclick=""jQueryAjaxCloseDialog()"" />
        </div>
    </div>
</div>
");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 25 "C:\DumiSoft_Web\DumiSoft.Web\Views\CompanyBranch\Index.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DumiSoft.Web.Models.Companies.CompanyBranchModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
