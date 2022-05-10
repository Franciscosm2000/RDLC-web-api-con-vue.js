using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Contexto;
using Sistema.Entidades.Rentas;
using Sistema.Web.Models.Rentas.Renta;

namespace Sistema.Web.Controllers.Rentas
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public RentasController(DbContextSistema context)
        {
            _context = context;
        }
        // POST: api/Crear
        //[Authorize(Policy = "Orden_Es")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultados_ = verificar_vehiculo(model.detalle);
            if (resultados_.Count() > 0)
            {
                string error="";

                foreach (var item in resultados_)
                {
                    error += " "+item.Item1;

                }
                return BadRequest(error);
            }

            Renta cat_ = new Renta
            {
                id_cliente = model.id_cliente,
                id_empleado = model.id_empleado,
                fecha = DateTime.Now,
                estado = true
            };

            try
            {
                _context.renta.Add(cat_);
                await _context.SaveChangesAsync();

                var id = cat_.id_renta;

                foreach (var det in model.detalle)
                {
                    
                    Detalle_Renta detalle = new Detalle_Renta
                    {
                        id_renta = id,
                        id_vehiculo = det.id_vehiculo,
                        fecha_ini = det.fecha_ini,
                        fecha_fin = det.fecha_fin,
                        costo_dia = det.monto_dia
                    };

                    _context.detalle_renta.Add(detalle);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }

        // PUT: api/Actualizar
        //[Authorize(Policy = "Orden_Mo")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.id_renta <= 0)
            {
                return BadRequest("El id no fue encontrado.");
            }

            var cat = await _context.renta.FirstOrDefaultAsync(p => p.id_renta == model.id_renta);


            if (cat == null)
            {
                return NotFound("Registro no encontrado.");
            }

            cat.id_cliente = model.id_cliente;
            cat.id_empleado = model.id_empleado;

            try
            {
                await _context.SaveChangesAsync();

                var Det = await _context.detalle_renta.Where(d => d.id_renta == model.id_renta).ToListAsync(); // todos los registros detalle

                var model_guardado = model.detalle.Where(d => d.id_detalle_renta != 0);

                var diferencia = (from x in Det
                                  select new
                                  {
                                      x.id_detalle_renta
                                  }).Except(from x in model_guardado
                                            select new
                                            {
                                                x.id_detalle_renta
                                            }).ToList(); //estos registros no existen dentro de la nueva peticion

                //existen elementos a elminar
                if (diferencia.Count > 0)
                {

                    foreach (var item in diferencia)
                    {
                        var aux_det = await _context.detalle_renta.
                            Where(p => p.id_detalle_renta == item.id_detalle_renta).ToListAsync();

                        _context.detalle_renta.RemoveRange(aux_det);

                        await _context.SaveChangesAsync();
                    }
                }
                //update y add elementos detalle
                foreach (var e in model.detalle)
                {
                    var det = await _context.detalle_renta
                        .FirstOrDefaultAsync(p => p.id_detalle_renta == e.id_detalle_renta); //encontramos el registro

                    if (det == null)
                    { // quiere decir que no existe 
                        List<DetalleModel> d = new List<DetalleModel>();
                        d.Add(
                                 new DetalleModel {
                                 id_detalle_renta = e.id_detalle_renta,
                                 id_renta = e.id_renta,
                                 id_vehiculo = e.id_vehiculo,
                                 fecha_ini = e.fecha_ini,
                                 fecha_fin = e.fecha_fin,
                                 monto_dia = e.monto_dia
                                 }
                            );


                        var resultados_ = verificar_vehiculo(d);

                        if (resultados_.Count() > 0)
                        {
                            string error = "";

                            foreach (var item in resultados_)
                            {
                                error += " " + item.Item1;

                            }
                            return BadRequest(error);
                        }
                        //escribimos el detalle                        

                        var id = model.id_renta;

                        Detalle_Renta detalle = new Detalle_Renta
                        {
                            id_renta = id,
                            id_vehiculo = e.id_vehiculo,
                            fecha_ini = e.fecha_ini,
                            fecha_fin = e.fecha_fin,
                            costo_dia = e.monto_dia

                        };

                        _context.detalle_renta.Add(detalle);
                    }
                    else
                    {

                        det.id_vehiculo = e.id_vehiculo;
                        det.fecha_ini = e.fecha_ini;
                        det.fecha_fin = e.fecha_fin;
                        det.costo_dia = e.monto_dia;

                    }

                    await _context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                // Guardar Excepción
                return BadRequest(ex + " " + model.detalle[0].id_renta );
            }

            return Ok();
        }

        // GET: api/ListarClientes
        //[Authorize(Policy = "Orden_Le")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ListarViewModel>> Listar()
        {
            var tipo = await _context.renta
                .Include(d=> d.cliente)
                .Include(d=> d.empleado)
               .OrderByDescending(c => c.id_renta)
               .ToListAsync();

            return tipo.Select(p => new ListarViewModel
            {
                id_renta = p.id_renta,
                id_cliente = p.id_cliente,
                cliente = p.cliente.primer_nombre + " " + p.cliente.segundo_nombre + " " 
                + p.cliente.primer_apellido + " " + p.cliente.segundo_apellido,
                id_empleado = p.id_empleado,
                empleado = p.empleado.primer_nombre + " " + p.empleado.segundo_nombre + " "
                + p.empleado.primer_apellido + " " + p.empleado.segundo_apellido,
                fecha = p.fecha,
                estado = p.estado

            });
        }

        // PUT: api/Desactivar/1/2
        //[Authorize(Policy = "Cargo_desac")]
        [HttpPut("[action]/{id}/{usuario}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id, string usuario)
        {

            if (id <= 0 || id.Equals(""))
            {
                return BadRequest("Id no encontrado.");
            }

            var cat = await _context.renta.FirstOrDefaultAsync(p => p.id_renta == id);

            if (cat == null)
            {
                return NotFound("Registro no encontrado.");
            }

            cat.estado = false;

            try
            {
                await _context.SaveChangesAsync();

                //bitacora.queryinsert(new Bitacora
                //{
                //    accion = "Inactivar",
                //    controlador = "Categoria",
                //    usuario = "admin",
                //    descripcion = "Se Inactivo el registro con el id " + cat.id_categoria

                //});
            }
            catch (Exception ex)
            {
                //bitacora.queryinsert(new Bitacora
                //{
                //    accion = "Inactivar",
                //    controlador = "Categoria",
                //    usuario = "admin",
                //    descripcion = "Ocurrio un error al inactivar el registro con id" + cat.id_categoria + " " + ex.Message

                //});
                // Guardar Excepción
                return BadRequest(ex);
            }

            return Ok();
        }

        // PUT: api/Activar/1/1
        //[Authorize(Policy = "Cargo_ac")]
        [HttpPut("[action]/{id}/{usuario}")]
        public async Task<IActionResult> Activar([FromRoute] int id, string usuario)
        {

            if (id <= 0 || id.Equals(""))
            {
                return BadRequest();
            }

            var cat = await _context.renta.FirstOrDefaultAsync(p => p.id_renta == id);

            if (cat == null)
            {
                return NotFound("Id no encontrado.");
            }

            cat.estado = true;

            //bitacora.queryinsert(new Bitacora
            //{
            //    accion = "Inactivar",
            //    controlador = "Categoria",
            //    usuario = "admin",
            //    descripcion = "Se activo el registro con el id " + cat.id_categoria

            //});


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //bitacora.queryinsert(new Bitacora
                //{
                //    accion = "Inactivar",
                //    controlador = "Categoria",
                //    usuario = "admin",
                //    descripcion = "Ocurrio un error al activar el registro con id" + cat.id_categoria + " " + ex.Message

                //});
                // Guardar Excepción
                return BadRequest(ex);
            }

            return Ok();
        }

        public List<Tuple<string, bool>> verificar_vehiculo(List<DetalleModel> vehiculo) {
           
            var result_b = false;
            var restul_d = "";

            var resultados = new List<Tuple<string, bool>>();

            foreach (var item_ in vehiculo)
            {
                var vehi = _context.vehiculo.FirstOrDefault(d => d.id_vehiculo == item_.id_vehiculo);

                var rentas = _context.detalle_renta.Where(d => d.id_vehiculo == vehi.id_vehiculo)
                    .ToList();

                var data_encontrado = from item in rentas
                                      where (item.fecha_ini <= item_.fecha_fin && item.fecha_fin >= item_.fecha_ini)
                                      select new
                                      {
                                          id = item.id_renta
                                      };

                if (data_encontrado.Count() > 0)
                {
                    result_b = true;
                    var placa = _context.vehiculo.FirstOrDefault(d=> d.id_vehiculo == item_.id_vehiculo);
                    restul_d = "Vehiculo con num de placa ocupado : "+ placa.placa;

                    resultados.Add(
                        new Tuple<string, bool>(restul_d, result_b)
                   ) ;
                }

            }

            return resultados;
        }
    }
}
