#pragma checksum "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Feriados\ExcluirJornada.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "470d3d9697d1529fdaa1041118e0335cf60d5726"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Feriados_ExcluirJornada), @"mvc.1.0.view", @"/Views/Feriados/ExcluirJornada.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Feriados/ExcluirJornada.cshtml", typeof(AspNetCore.Views_Feriados_ExcluirJornada))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"470d3d9697d1529fdaa1041118e0335cf60d5726", @"/Views/Feriados/ExcluirJornada.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"306577867de7e655ee7413ae8d13a1b060287378", @"/Views/_ViewImports.cshtml")]
    public class Views_Feriados_ExcluirJornada : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SistemaControleEmpresarial.Models.SolicitacaoJornada>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ListaJornadas", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Feriados", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-warning"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ExcluirJornada", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(61, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Feriados\ExcluirJornada.cshtml"
  
    ViewData["Title"] = "ExcluirJornada";

#line default
#line hidden
            BeginContext(113, 240, true);
            WriteLiteral("\r\n<h2>Excluir Jornada</h2>\r\n\r\n<h3>Tem certeza que deseja excluir esta jornada ?</h3>\r\n<div>\r\n    <hr />\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            Descri&ccedil;&atilde;o do Feriado\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(354, 56, false);
#line 17 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Feriados\ExcluirJornada.cshtml"
       Write(Html.DisplayFor(model => model.Feriado.DescricaoFeriado));

#line default
#line hidden
            EndContext();
            BeginContext(410, 100, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            Usu&aacute;rio\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(511, 61, false);
#line 23 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Feriados\ExcluirJornada.cshtml"
       Write(Html.DisplayFor(model => model.ApplicationUser.CodigoExterno));

#line default
#line hidden
            EndContext();
            BeginContext(572, 3, true);
            WriteLiteral(" - ");
            EndContext();
            BeginContext(576, 59, false);
#line 23 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Feriados\ExcluirJornada.cshtml"
                                                                        Write(Html.DisplayFor(model => model.ApplicationUser.NomeUsuario));

#line default
#line hidden
            EndContext();
            BeginContext(635, 99, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            Justificativa\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(735, 45, false);
#line 29 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Feriados\ExcluirJornada.cshtml"
       Write(Html.DisplayFor(model => model.Justificativa));

#line default
#line hidden
            EndContext();
            BeginContext(780, 34, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n\r\n    ");
            EndContext();
            BeginContext(814, 396, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ddde80128f7b49288bf87717413869e3", async() => {
                BeginContext(848, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(858, 58, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "26800156f0124b4e8e1a319d0efc2066", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 34 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Feriados\ExcluirJornada.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.CodigoSolicitacaoJornada);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(916, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(926, 140, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "61a1c62bf3e343bd802b2d2a4dde23bb", async() => {
                    BeginContext(1006, 56, true);
                    WriteLiteral("<i class=\"glyphicon glyphicon-step-backward\"></i> Voltar");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1066, 137, true);
                WriteLiteral("\r\n        <button type=\"submit\" class=\"btn btn-danger\"><i class=\"glyphicon glyphicon-trash\"></i> Confirmar Exclus&atilde;o</button>\r\n    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1210, 8, true);
            WriteLiteral("\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SistemaControleEmpresarial.Models.SolicitacaoJornada> Html { get; private set; }
    }
}
#pragma warning restore 1591
