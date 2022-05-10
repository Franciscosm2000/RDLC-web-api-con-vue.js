using AspNetCore.Reporting;
using Sistema.Datos.ContextoSp.Reporte;
using Sistema.Entidades.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Web.Service
{
    public interface IReportService
    {
        byte[] GenerateReportAsync(string path, string reportName, string reportType, DateTime desde , DateTime hasta);
    }
    public class ReportService : IReportService
    {
        public byte[] GenerateReportAsync(string fileDirPath, string reportName, string reportType, DateTime desde , DateTime hasta)
        {
            // string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("ApiServiceReport_1.dll", string.Empty);
            string rdlcFilePath = fileDirPath+"\\Report\\Report.rdlc"; //string.Format("{0}Report\\{1}.rdlc", fileDirPath, reportName);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("utf-8");

            LocalReport report = new LocalReport(rdlcFilePath);

            // prepare data for report

            List<ReportModel> data = new List<ReportModel>();

            ReporteSP repor = new ReporteSP();

            data = repor.queryListar(1,desde,hasta);

            report.AddDataSource("DataSet1",data);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            var result = report.Execute(GetRenderType(reportType), 1, parameters);

            return result.MainStream;
        }
        private RenderType GetRenderType(string reportType)
        {
            var renderType = RenderType.Pdf;

            switch (reportType.ToUpper())
            {
                default:
                case "PDF":
                    renderType = RenderType.Pdf;
                    break;
                case "XLS":
                    renderType = RenderType.Excel;
                    break;
                case "DOC":
                    renderType = RenderType.Word;
                    break;
            }

            return renderType;
        }

    }
}
