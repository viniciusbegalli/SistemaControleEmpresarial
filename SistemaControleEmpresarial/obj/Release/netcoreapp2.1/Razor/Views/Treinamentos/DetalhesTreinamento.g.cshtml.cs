#pragma checksum "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "68f3bb2e17b17557cb56a5e925ffc6c92f663041"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Treinamentos_DetalhesTreinamento), @"mvc.1.0.view", @"/Views/Treinamentos/DetalhesTreinamento.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Treinamentos/DetalhesTreinamento.cshtml", typeof(AspNetCore.Views_Treinamentos_DetalhesTreinamento))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"68f3bb2e17b17557cb56a5e925ffc6c92f663041", @"/Views/Treinamentos/DetalhesTreinamento.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"306577867de7e655ee7413ae8d13a1b060287378", @"/Views/_ViewImports.cshtml")]
    public class Views_Treinamentos_DetalhesTreinamento : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SistemaControleEmpresarial.Models.Treinamento>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ListaTreinamentos", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Treinamentos", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-warning"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ParticiparTreinamento", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ExportarInscritosExcel", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(54, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
            BeginContext(99, 126, true);
            WriteLiteral("\r\n<h2>Detalhes</h2>\r\n\r\n<div>\r\n    <h4>Treinamento</h4>\r\n    <hr />\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(226, 42, false);
#line 14 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayNameFor(model => model.Titulo));

#line default
#line hidden
            EndContext();
            BeginContext(268, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(312, 38, false);
#line 17 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayFor(model => model.Titulo));

#line default
#line hidden
            EndContext();
            BeginContext(350, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(394, 45, false);
#line 20 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayNameFor(model => model.Descricao));

#line default
#line hidden
            EndContext();
            BeginContext(439, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(483, 41, false);
#line 23 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayFor(model => model.Descricao));

#line default
#line hidden
            EndContext();
            BeginContext(524, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(568, 57, false);
#line 26 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayNameFor(model => model.DataInicioTreinamento));

#line default
#line hidden
            EndContext();
            BeginContext(625, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(669, 53, false);
#line 29 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayFor(model => model.DataInicioTreinamento));

#line default
#line hidden
            EndContext();
            BeginContext(722, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(766, 54, false);
#line 32 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayNameFor(model => model.DataFimTreinamento));

#line default
#line hidden
            EndContext();
            BeginContext(820, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(864, 50, false);
#line 35 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayFor(model => model.DataFimTreinamento));

#line default
#line hidden
            EndContext();
            BeginContext(914, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(958, 57, false);
#line 38 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayNameFor(model => model.HoraInicioTreinamento));

#line default
#line hidden
            EndContext();
            BeginContext(1015, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1059, 53, false);
#line 41 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayFor(model => model.HoraInicioTreinamento));

#line default
#line hidden
            EndContext();
            BeginContext(1112, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1156, 54, false);
#line 44 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayNameFor(model => model.HoraFimTreinamento));

#line default
#line hidden
            EndContext();
            BeginContext(1210, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1254, 50, false);
#line 47 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayFor(model => model.HoraFimTreinamento));

#line default
#line hidden
            EndContext();
            BeginContext(1304, 93, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            Criador\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1398, 61, false);
#line 53 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayFor(model => model.ApplicationUser.CodigoExterno));

#line default
#line hidden
            EndContext();
            BeginContext(1459, 3, true);
            WriteLiteral(" - ");
            EndContext();
            BeginContext(1463, 59, false);
#line 53 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
                                                                        Write(Html.DisplayFor(model => model.ApplicationUser.NomeUsuario));

#line default
#line hidden
            EndContext();
            BeginContext(1522, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1566, 47, false);
#line 56 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayNameFor(model => model.DataCriacao));

#line default
#line hidden
            EndContext();
            BeginContext(1613, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1657, 43, false);
#line 59 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayFor(model => model.DataCriacao));

#line default
#line hidden
            EndContext();
            BeginContext(1700, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1744, 55, false);
#line 62 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayNameFor(model => model.SituacaoTreinamento));

#line default
#line hidden
            EndContext();
            BeginContext(1799, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1843, 51, false);
#line 65 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
       Write(Html.DisplayFor(model => model.SituacaoTreinamento));

#line default
#line hidden
            EndContext();
            BeginContext(1894, 78, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n    <hr />\r\n    <h3>INSTRUTORES</h3>\r\n    <hr />\r\n");
            EndContext();
#line 71 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
     if (Model.InstrutoresTreinamento != null && Model.InstrutoresTreinamento.Count > 0)
    {
        foreach (var item in Model.InstrutoresTreinamento)
        {

#line default
#line hidden
            BeginContext(2140, 158, true);
            WriteLiteral("            <dl class=\"dl-horizontal\">\r\n                <dt>\r\n                    Instrutor\r\n                </dt>\r\n                <dd>\r\n                    ");
            EndContext();
            BeginContext(2299, 48, false);
#line 80 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
               Write(Html.DisplayFor(modelItem => item.CodigoExterno));

#line default
#line hidden
            EndContext();
            BeginContext(2347, 3, true);
            WriteLiteral(" - ");
            EndContext();
            BeginContext(2351, 48, false);
#line 80 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
                                                                   Write(Html.DisplayFor(modelItem => item.NomeInstrutor));

#line default
#line hidden
            EndContext();
            BeginContext(2399, 44, true);
            WriteLiteral("\r\n                </dd>\r\n            </dl>\r\n");
            EndContext();
#line 83 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
        }
    }

#line default
#line hidden
            BeginContext(2461, 21, true);
            WriteLiteral("</div>\r\n\r\n<div>\r\n    ");
            EndContext();
            BeginContext(2482, 148, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "35cec7bf1b4544659124c4ee7e6a34b4", async() => {
                BeginContext(2570, 56, true);
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
            BeginContext(2630, 6, true);
            WriteLiteral("\r\n    ");
            EndContext();
            BeginContext(2636, 161, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0431145fd09a45699c41a2cfac21006c", async() => {
                BeginContext(2738, 55, true);
                WriteLiteral("<i class=\"glyphicon glyphicon-education\"></i>Participar");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 89 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
                                            WriteLiteral(Model.CodigoTreinamento);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2797, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 90 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
     if (User.IsInRole("Gerente") || User.IsInRole("Administrador"))
    {

#line default
#line hidden
            BeginContext(2876, 8, true);
            WriteLiteral("        ");
            EndContext();
            BeginContext(2884, 168, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f4d7b15420f748d7a87e11d4bc8f1e4f", async() => {
                BeginContext(2987, 61, true);
                WriteLiteral("<i class=\"glyphicon glyphicon-export\"></i> Exportar Inscritos");
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
#line 92 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
                                                 WriteLiteral(Model.CodigoTreinamento);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3052, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 93 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\DetalhesTreinamento.cshtml"
    }

#line default
#line hidden
            BeginContext(3061, 8, true);
            WriteLiteral("</div>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SistemaControleEmpresarial.Models.Treinamento> Html { get; private set; }
    }
}
#pragma warning restore 1591