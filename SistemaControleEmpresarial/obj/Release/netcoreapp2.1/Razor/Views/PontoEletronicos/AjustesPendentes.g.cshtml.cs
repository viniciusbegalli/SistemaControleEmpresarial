#pragma checksum "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "53cdc461bdfe786bd0542e1f78491aa71d84c27b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PontoEletronicos_AjustesPendentes), @"mvc.1.0.view", @"/Views/PontoEletronicos/AjustesPendentes.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/PontoEletronicos/AjustesPendentes.cshtml", typeof(AspNetCore.Views_PontoEletronicos_AjustesPendentes))]
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
#line 1 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\_ViewImports.cshtml"
using SistemaControleEmpresarial;

#line default
#line hidden
#line 2 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\_ViewImports.cshtml"
using SistemaControleEmpresarial.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"53cdc461bdfe786bd0542e1f78491aa71d84c27b", @"/Views/PontoEletronicos/AjustesPendentes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"306577867de7e655ee7413ae8d13a1b060287378", @"/Views/_ViewImports.cshtml")]
    public class Views_PontoEletronicos_AjustesPendentes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SistemaControleEmpresarial.Models.AjustePontoEletronico>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AjustesPonto", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "PontoEletronicos", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-warning"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ExportarExcelAjustesPendentes", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DetalhesAjustePendente", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(77, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
  
    ViewData["Title"] = "AjustesPonto";

#line default
#line hidden
            BeginContext(128, 38, false);
#line 6 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
Write(Html.Raw(TempData["msgRegistroPonto"]));

#line default
#line hidden
            EndContext();
            BeginContext(166, 45, true);
            WriteLiteral("\r\n\r\n<h2>Ajustes Ponto Pendentes</h2>\r\n<div>\r\n");
            EndContext();
#line 11 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
         if (User.IsInRole("Supervisor"))
        {

#line default
#line hidden
            BeginContext(273, 12, true);
            WriteLiteral("            ");
            EndContext();
            BeginContext(285, 147, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c2db1825c71a4800ade17cf4ba5a53f6", async() => {
                BeginContext(372, 56, true);
                WriteLiteral("<i class=\"glyphicon glyphicon-step-backward\"></i> Voltar");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(432, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 14 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
        }

#line default
#line hidden
            BeginContext(452, 6, true);
            WriteLiteral("    \r\n");
            EndContext();
#line 17 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
      
        if (Model != null && Model.Count() > 0)
        {

#line default
#line hidden
            BeginContext(526, 8, true);
            WriteLiteral("        ");
            EndContext();
            BeginContext(534, 125, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7ae803f7a88240778e5b8f3583924c55", async() => {
                BeginContext(604, 51, true);
                WriteLiteral("<i class=\"glyphicon glyphicon-export\"></i> Exportar");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(659, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 21 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
        }
    

#line default
#line hidden
            BeginContext(679, 20, true);
            WriteLiteral("</div>\r\n\r\n<hr />\r\n\r\n");
            EndContext();
#line 27 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
  
    if (Model == null || (Model != null && Model.Count() == 0))
    {

#line default
#line hidden
            BeginContext(775, 119, true);
            WriteLiteral("    <div>\r\n        <p>\r\n            Nenhuma solicita&ccedil;&atilde;o de ajuste encontrada.\r\n        </p>\r\n    </div>\r\n");
            EndContext();
#line 35 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
    }
    else
    {

#line default
#line hidden
            BeginContext(918, 185, true);
            WriteLiteral("    <table class=\"table\">\r\n        <thead>\r\n            <tr>\r\n                <th>\r\n                    Usu&aacute;rio\r\n                </th>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(1104, 46, false);
#line 45 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
               Write(Html.DisplayNameFor(model => model.DataAjuste));

#line default
#line hidden
            EndContext();
            BeginContext(1150, 67, true);
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(1218, 55, false);
#line 48 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
               Write(Html.DisplayNameFor(model => model.HoraPrimeiraEntrada));

#line default
#line hidden
            EndContext();
            BeginContext(1273, 146, true);
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    Sa&iacute;da\r\n                </th>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(1420, 54, false);
#line 54 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
               Write(Html.DisplayNameFor(model => model.HoraSegundaEntrada));

#line default
#line hidden
            EndContext();
            BeginContext(1474, 274, true);
            WriteLiteral(@"
                </th>
                <th>
                    Sa&iacute;da
                </th>
                <th>
                    Situa&ccedil;&atilde;o
                </th>
                <th></th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 66 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
             foreach (var item in Model)
                {

#line default
#line hidden
            BeginContext(1809, 60, true);
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1870, 64, false);
#line 70 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
               Write(Html.DisplayFor(modelItem => item.ApplicationUser.CodigoExterno));

#line default
#line hidden
            EndContext();
            BeginContext(1934, 3, true);
            WriteLiteral(" - ");
            EndContext();
            BeginContext(1938, 62, false);
#line 70 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
                                                                                   Write(Html.DisplayFor(modelItem => item.ApplicationUser.NomeUsuario));

#line default
#line hidden
            EndContext();
            BeginContext(2000, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2068, 45, false);
#line 73 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
               Write(Html.DisplayFor(modelItem => item.DataAjuste));

#line default
#line hidden
            EndContext();
            BeginContext(2113, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2181, 54, false);
#line 76 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
               Write(Html.DisplayFor(modelItem => item.HoraPrimeiraEntrada));

#line default
#line hidden
            EndContext();
            BeginContext(2235, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2303, 52, false);
#line 79 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
               Write(Html.DisplayFor(modelItem => item.HoraPrimeiraSaida));

#line default
#line hidden
            EndContext();
            BeginContext(2355, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2423, 53, false);
#line 82 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
               Write(Html.DisplayFor(modelItem => item.HoraSegundaEntrada));

#line default
#line hidden
            EndContext();
            BeginContext(2476, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2544, 51, false);
#line 85 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
               Write(Html.DisplayFor(modelItem => item.HoraSegundaSaida));

#line default
#line hidden
            EndContext();
            BeginContext(2595, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2663, 49, false);
#line 88 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
               Write(Html.DisplayFor(modelItem => item.SituacaoAjuste));

#line default
#line hidden
            EndContext();
            BeginContext(2712, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2779, 154, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1912278ccb9e4d5c8329678ce5047cb9", async() => {
                BeginContext(2876, 53, true);
                WriteLiteral("<i class=\"glyphicon glyphicon-list-alt\"></i> Detalhes");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 91 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
                                                             WriteLiteral(item.CodigoAjuste);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2933, 44, true);
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
            EndContext();
#line 94 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
            }

#line default
#line hidden
            BeginContext(2992, 32, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
            EndContext();
#line 97 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\AjustesPendentes.cshtml"
    }

#line default
#line hidden
            BeginContext(3034, 55, true);
            WriteLiteral("<div id=\"modal\" class=\"modal fade\" role=\"dialog\" />\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(3107, 268, true);
                WriteLiteral(@"
    <script>
        $(function () {
            $(""#btnAjuste"").click(function () {
                $(""#modal"").load(""SolicitarAjustePonto"", function () {
                    $(""#modal"").modal();
                })
            });
        })
    </script>
");
                EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SistemaControleEmpresarial.Models.AjustePontoEletronico>> Html { get; private set; }
    }
}
#pragma warning restore 1591
