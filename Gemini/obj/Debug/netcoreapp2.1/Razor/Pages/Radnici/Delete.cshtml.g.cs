#pragma checksum "C:\Users\Uroš\Desktop\Gemini\Gemini\Pages\Radnici\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a0aa52bb592c4d563a392b8972c7230edc602863"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Gemini.Pages.Radnici.Pages_Radnici_Delete), @"mvc.1.0.razor-page", @"/Pages/Radnici/Delete.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Radnici/Delete.cshtml", typeof(Gemini.Pages.Radnici.Pages_Radnici_Delete), null)]
namespace Gemini.Pages.Radnici
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Uroš\Desktop\Gemini\Gemini\Pages\_ViewImports.cshtml"
using Gemini;

#line default
#line hidden
#line 3 "C:\Users\Uroš\Desktop\Gemini\Gemini\Pages\Radnici\Delete.cshtml"
using System.Data.SqlClient;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a0aa52bb592c4d563a392b8972c7230edc602863", @"/Pages/Radnici/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3c9eaa6190b295e17025b77bc3d4317b7f46162c", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Radnici_Delete : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(79, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 5 "C:\Users\Uroš\Desktop\Gemini\Gemini\Pages\Radnici\Delete.cshtml"
   
    try
    {
        String id = Request.Query["id"];
        String connectionString = "Data Source=DESKTOP-AV5CJI4;Initial Catalog=gemini;Integrated Security=True";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            String sql = "DELETE FROM clients WHERE id=@id";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

    }
    catch (Exception e)
    {
    }

    Response.Redirect("/Radnici/Index");

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Gemini.Pages.Radnici.DeleteModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Gemini.Pages.Radnici.DeleteModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Gemini.Pages.Radnici.DeleteModel>)PageContext?.ViewData;
        public Gemini.Pages.Radnici.DeleteModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
