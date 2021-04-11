#pragma checksum "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Relatorios\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8544e5a448fa1f05580cd90d71006bdc4b5d07b2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Relatorios_Index), @"mvc.1.0.view", @"/Views/Relatorios/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Relatorios/Index.cshtml", typeof(AspNetCore.Views_Relatorios_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8544e5a448fa1f05580cd90d71006bdc4b5d07b2", @"/Views/Relatorios/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"306577867de7e655ee7413ae8d13a1b060287378", @"/Views/_ViewImports.cshtml")]
    public class Views_Relatorios_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 13, true);
            WriteLiteral("<title>\r\n    ");
            EndContext();
            BeginContext(14, 17, false);
#line 2 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Relatorios\Index.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(31, 400, true);
            WriteLiteral(@" - Grafico
</title>
<script type=""text/javascript"" src=""https://www.gstatic.com/charts/loader.js""></script>
<div id=""chart_div"" style=""width: 900px; height: 500px;""></div>
<script type=""text/javascript"">

    google.charts.load('current', {packages: ['corechart', 'bar']});
    google.charts.setOnLoadCallback(CarregaDados);
    function CarregaDados() {
        $.ajax({
            url: '");
            EndContext();
            BeginContext(432, 42, false);
#line 12 "C:\Users\viniciusbegalli\source\repos\SistemaControleEmpresarial\SistemaControleEmpresarial\Views\Relatorios\Index.cshtml"
             Write(Url.Action("FeriadosGrafico","Relatorios"));

#line default
#line hidden
            EndContext();
            BeginContext(474, 1273, true);
            WriteLiteral(@"',
            dataType: ""json"",
            type: ""GET"",
            error: function(xhr, status, error) {
                var err = eval(""("" + xhr.responseText + "")"");
                toastr.error(err.message);
            },
            success: function(data) {
                GraficoFeriados(data);
                return false;
            }
        });
        return false;
    }
    function GraficoFeriados(data) {
        var dataArray = [
            ['AnoMes', 'DiasUteis' , 'DiasFeriado']
        ];
        $.each(data, function(i, item) {
            dataArray.push([item.anoMes, item.diasUteis, item.diasFeriado]);
        });
        var data = google.visualization.arrayToDataTable(dataArray);
        var options = {
            title: 'Feriados AnoMes',
            chartArea: {
                width: '50%'
            },
            colors: ['#b0120a', '#ffab91'],
            hAxis: {
                title: 'AnoMes',
                minValue: 0
            },
      ");
            WriteLiteral("      vAxis: {\r\n                title: \'Feriados\'\r\n            }\r\n        };\r\n        var chart = new google.visualization.LineChart(document.getElementById(\'chart_div\'));\r\n        chart.draw(data, options);\r\n        return false;\r\n    }\r\n</script> ");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591