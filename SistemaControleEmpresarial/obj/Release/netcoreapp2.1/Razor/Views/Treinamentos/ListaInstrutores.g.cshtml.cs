#pragma checksum "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\ListaInstrutores.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "73f8c873dfd4ccefef6be7f32ea1cda2b28367f3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Treinamentos_ListaInstrutores), @"mvc.1.0.view", @"/Views/Treinamentos/ListaInstrutores.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Treinamentos/ListaInstrutores.cshtml", typeof(AspNetCore.Views_Treinamentos_ListaInstrutores))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"73f8c873dfd4ccefef6be7f32ea1cda2b28367f3", @"/Views/Treinamentos/ListaInstrutores.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"306577867de7e655ee7413ae8d13a1b060287378", @"/Views/_ViewImports.cshtml")]
    public class Views_Treinamentos_ListaInstrutores : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SistemaControleEmpresarial.Models.TreinamentoInstrutor>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(76, 1028, true);
            WriteLiteral(@"

    <div class=""form-group"">
    <h3>INSTRUTOR(ES)</h3>
</div>
<br />
<div class=""form-group"">
    <input type=""text"" id=""codigoInstrutor"" onchange=""CodigoInstrutorChange();"" placeholder=""Código Instrutor"" name=""codigoInstrutor"" class=""form-control"" />
    <input type=""text"" id=""nomeInstrutor"" readonly=""readonly"" placeholder=""Instrutor"" class=""form-control"" />
</div>
<div class=""form-group"">
    <button type=""button"" title=""Clique para abrir o modal de busca por usuários"" data-toggle=""modal"" data-target=""#myModal"" id=""showPop2"" value=""Cancelar"" class=""btn btn-info"" data-dismiss=""modal""><i class=""glyphicon glyphicon-search""></i></button>
    
    <a href=""#"" onclick=""SalvarInstrutor();"" title=""Clique para adicionar o instrutor"" id=""adicionaInstrutor"" class=""btn btn-primary""><i class=""glyphicon glyphicon-plus""></i> Adicionar</a>
</div>
<div class=""form-group"" id=""cbInstrutores"">
    <input type=""hidden"" name=""cblInstrutores"" id=""cblInstrutores"" />
</div>

<input type=""hidden"" id=""idTreiname");
            WriteLiteral("nto\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1104, "\"", 1132, 1);
#line 21 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\ListaInstrutores.cshtml"
WriteAttributeValue("", 1112, ViewBag.Treinamento, 1112, 20, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1133, 262, true);
            WriteLiteral(@" />

<table class=""table"">
    <thead>
        <tr>
            <th></th>
            <th>
                C&oacute;digo
            </th>
            <th>
                Nome do Instrutor
            </th>
        </tr>
    </thead>
    <tbody>
");
            EndContext();
#line 36 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\ListaInstrutores.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
            BeginContext(1444, 71, true);
            WriteLiteral("            <tr>\r\n                <td>\r\n                    <a href=\"#\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 1515, "\"", 1587, 5);
            WriteAttributeValue("", 1525, "RemoveInstrutor(", 1525, 16, true);
#line 40 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\ListaInstrutores.cshtml"
WriteAttributeValue("", 1541, item.CodigoTreinamento, 1541, 23, false);

#line default
#line hidden
            WriteAttributeValue("", 1564, ",", 1564, 1, true);
#line 40 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\ListaInstrutores.cshtml"
WriteAttributeValue(" ", 1565, item.CodigoExterno, 1566, 19, false);

#line default
#line hidden
            WriteAttributeValue("", 1585, ");", 1585, 2, true);
            EndWriteAttribute();
            BeginContext(1588, 197, true);
            WriteLiteral(" title=\"Clique para remover o instrutor\" id=\"removeInstrutor\" class=\"btn btn-danger\"><i class=\"glyphicon glyphicon-minus\"></i></a>\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1786, 58, false);
#line 43 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\ListaInstrutores.cshtml"
               Write(Html.DisplayFor(modelItem => item.Instrutor.CodigoExterno));

#line default
#line hidden
            EndContext();
            BeginContext(1844, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1912, 56, false);
#line 46 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\ListaInstrutores.cshtml"
               Write(Html.DisplayFor(modelItem => item.Instrutor.NomeUsuario));

#line default
#line hidden
            EndContext();
            BeginContext(1968, 44, true);
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
            EndContext();
#line 49 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Treinamentos\ListaInstrutores.cshtml"
        }

#line default
#line hidden
            BeginContext(2023, 22, true);
            WriteLiteral("    </tbody>\r\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SistemaControleEmpresarial.Models.TreinamentoInstrutor>> Html { get; private set; }
    }
}
#pragma warning restore 1591
