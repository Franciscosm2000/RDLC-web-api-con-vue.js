using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Contexto;
using Sistema.Entidades.Rentas;
using Sistema.Web.Models.Rentas.Detalle;
using Sistema.Web.Service;

namespace Sistema.Web.Controllers.Rentas
{
    [Route("api/[controller]")]
    [ApiController]
    public class Detalle_RentasController : ControllerBase
    {
        private readonly DbContextSistema _context;
        private IReportService _reportService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public Detalle_RentasController(DbContextSistema context, IHostingEnvironment host, IReportService reportService)
        {
            _context = context;
            _hostingEnvironment = host;
            _reportService = reportService;
        }
        // GET: api/ListarClientes
        //[Authorize(Policy = "Orden_Le")]
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<DetalleViewModel>> Listar([FromRoute] int id)
        {
            var detall = await _context.detalle_renta
                .Include(c => c.vehiculo)
                .Where(d => d.id_renta == id)
                .ToListAsync();

            return detall.Select(p => new DetalleViewModel
            {
                id_detalle_renta = p.id_detalle_renta,
                id_renta = p.id_renta,
                id_vehiculo = p.id_vehiculo,
                vehiculo = p.vehiculo.placa,
                fecha_ini = p.fecha_ini,
                fecha_fin = p.fecha_fin,
                monto_dia = p.costo_dia,
                num_dia = (p.fecha_fin.Day - p.fecha_ini.Day)+1
            });


        }

        [HttpGet("[action]/{reportType}/{desde}/{hasta}")]
        //[Route("Export_Data1")]
        public ActionResult Generar_Reporte([FromRoute] string reportType, DateTime desde , DateTime hasta)
        {
            string reportName = "report";
            //string reportType = "XLS";
            string path = _hostingEnvironment.ContentRootPath.ToString();
            var reportNameWithLang = reportName;
            var reportFileByteString = _reportService.GenerateReportAsync(path, reportNameWithLang, reportType,desde,hasta);
            return File(reportFileByteString, MediaTypeNames.Application.Octet, getReportName(reportNameWithLang, reportType));
        }

        private string getReportName(string reportName, string reportType)
        {
            var outputFileName = reportName + ".pdf";

            switch (reportType.ToUpper())
            {
                default:
                case "PDF":
                    outputFileName = reportName + ".pdf";
                    break;
                case "XLS":
                    outputFileName = reportName + ".xls";
                    break;
                case "WORD":
                    outputFileName = reportName + ".doc";
                    break;
            }

            return outputFileName;
        }


    }
}
