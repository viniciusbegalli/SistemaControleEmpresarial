#pragma checksum "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1ff693987defb433933c1c7f9e89399a4743b30a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PontoEletronicos_MeuPontoEletronico), @"mvc.1.0.view", @"/Views/PontoEletronicos/MeuPontoEletronico.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/PontoEletronicos/MeuPontoEletronico.cshtml", typeof(AspNetCore.Views_PontoEletronicos_MeuPontoEletronico))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1ff693987defb433933c1c7f9e89399a4743b30a", @"/Views/PontoEletronicos/MeuPontoEletronico.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"306577867de7e655ee7413ae8d13a1b060287378", @"/Views/_ViewImports.cshtml")]
    public class Views_PontoEletronicos_MeuPontoEletronico : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SistemaControleEmpresarial.Models.PontoEletronico>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ExportarExcel", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RegistrarPonto", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(71, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
  
    ViewData["Title"] = "MeuPontoEletronico";

#line default
#line hidden
            BeginContext(127, 26, true);
            WriteLiteral("\r\n<h2>Meus Pontos</h2>\r\n\r\n");
            EndContext();
            BeginContext(153, 503, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3b6750aaaee241b9bfce128133fc9e1d", async() => {
                BeginContext(187, 122, true);
                WriteLiteral("\r\n    <button type=\"submit\" class=\"btn btn-primary\"><i class=\"glyphicon glyphicon-hand-up\"></i> Registrar Ponto</button>\r\n");
                EndContext();
#line 11 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
      
        if (Model != null && Model.Count() > 0)
        {

#line default
#line hidden
                BeginContext(377, 12, true);
                WriteLiteral("            ");
                EndContext();
                BeginContext(389, 109, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2e602dc9678a4e1ead192e84cf2fbbda", async() => {
                    BeginContext(443, 51, true);
                    WriteLiteral("<i class=\"glyphicon glyphicon-export\"></i> Exportar");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(498, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 15 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
        }
    

#line default
#line hidden
                BeginContext(518, 131, true);
                WriteLiteral("    <button type=\"button\" id=\"btnAjuste\" class=\"btn btn-info\"><i class=\"glyphicon glyphicon-erase\"></i> Solicitar Ajuste</button>\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(656, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            BeginContext(661, 38, false);
#line 20 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
Write(Html.Raw(TempData["msgRegistroPonto"]));

#line default
#line hidden
            EndContext();
            BeginContext(699, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 22 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
  
    if (Model == null || (Model != null && Model.Count() == 0))
    {

#line default
#line hidden
            BeginContext(779, 137, true);
            WriteLiteral("        <div>\r\n            <p>\r\n                Voc&ecirc; n&atilde;o possui nenhum ponto registrado.\r\n            </p>\r\n        </div>\r\n");
            EndContext();
#line 30 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
    }
    else
    {

#line default
#line hidden
            BeginContext(940, 124, true);
            WriteLiteral("        <table class=\"table\">\r\n            <thead>\r\n                <tr>\r\n                    <th>\r\n                        ");
            EndContext();
            BeginContext(1065, 40, false);
#line 37 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
                   Write(Html.DisplayNameFor(model => model.Data));

#line default
#line hidden
            EndContext();
            BeginContext(1105, 79, true);
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
            EndContext();
            BeginContext(1185, 59, false);
#line 40 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
                   Write(Html.DisplayNameFor(model => model.DataHoraPrimeiraEntrada));

#line default
#line hidden
            EndContext();
            BeginContext(1244, 79, true);
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
            EndContext();
            BeginContext(1324, 57, false);
#line 43 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
                   Write(Html.DisplayNameFor(model => model.DataHoraPrimeiraSaida));

#line default
#line hidden
            EndContext();
            BeginContext(1381, 79, true);
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
            EndContext();
            BeginContext(1461, 58, false);
#line 46 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
                   Write(Html.DisplayNameFor(model => model.DataHoraSegundaEntrada));

#line default
#line hidden
            EndContext();
            BeginContext(1519, 79, true);
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
            EndContext();
            BeginContext(1599, 56, false);
#line 49 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
                   Write(Html.DisplayNameFor(model => model.DataHoraSegundaSaida));

#line default
#line hidden
            EndContext();
            BeginContext(1655, 126, true);
            WriteLiteral("\r\n                    </th>\r\n                    <th></th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n");
            EndContext();
#line 55 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
            BeginContext(1846, 84, true);
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(1931, 39, false);
#line 59 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Data));

#line default
#line hidden
            EndContext();
            BeginContext(1970, 91, true);
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(2062, 54, false);
#line 62 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
                       Write(Html.DisplayFor(modelItem => item.HoraPrimeiraEntrada));

#line default
#line hidden
            EndContext();
            BeginContext(2116, 91, true);
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(2208, 52, false);
#line 65 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
                       Write(Html.DisplayFor(modelItem => item.HoraPrimeiraSaida));

#line default
#line hidden
            EndContext();
            BeginContext(2260, 91, true);
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(2352, 53, false);
#line 68 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
                       Write(Html.DisplayFor(modelItem => item.HoraSegundaEntrada));

#line default
#line hidden
            EndContext();
            BeginContext(2405, 91, true);
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(2497, 51, false);
#line 71 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
                       Write(Html.DisplayFor(modelItem => item.HoraSegundaSaida));

#line default
#line hidden
            EndContext();
            BeginContext(2548, 60, true);
            WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n");
            EndContext();
#line 74 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
                }

#line default
#line hidden
            BeginContext(2627, 40, true);
            WriteLiteral("            </tbody>\r\n        </table>\r\n");
            EndContext();
#line 77 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\PontoEletronicos\MeuPontoEletronico.cshtml"
    }

#line default
#line hidden
            BeginContext(2677, 55, true);
            WriteLiteral("<div id=\"modal\" class=\"modal fade\" role=\"dialog\" />\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(2750, 294, true);
                WriteLiteral(@"
    <script>
        $(function () {
            $(""#btnAjuste"").click(function () {
                $(""#modal"").load(""../AjustePontoEletronicos/SolicitarAjustePonto"", function () {
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SistemaControleEmpresarial.Models.PontoEletronico>> Html { get; private set; }
    }
}
#pragma warning restore 1591