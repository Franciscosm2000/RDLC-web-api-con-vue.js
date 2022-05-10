using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Contexto;
using Sistema.Entidades.Persona;
using Sistema.Web.Models.Personas.Empleados;

namespace Sistema.Web.Controllers.Personas
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public EmpleadosController(DbContextSistema context)
        {
            _context = context;
        }


        //[Authorize(Policy = "Cargo_cre")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (Exists(model.cedula, 0, 1))
            {
                return BadRequest("El numero de cedula ya existe");
            }


            Empleado cat_ = new Empleado
            {
                cedula = model.cedula,
                primer_nombre = model.primer_nombre,
                segundo_nombre = model.segundo_nombre,
                primer_apellido = model.primer_apellido,
                segundo_apellido = model.segundo_apellido,
                telefono = model.telefono,
                direccion = model.direccion,
                estado = true
            };

            _context.empleado.Add(cat_);
            try
            {
                await _context.SaveChangesAsync();

                //bitacora.queryinsert(new Bitacora
                //{
                //    accion = "Insertar",
                //    controlador = "Categoria",
                //    usuario = "admin",
                //    descripcion = "Se Inserto un nuevo registro con el id " + cat_.id_categoria

                //});

            }
            catch (Exception ex)
            {
                //bitacora.queryinsert(new Bitacora
                //{
                //    accion = "Insertar",
                //    controlador = "Categoria",
                //    usuario = "admin",
                //    descripcion = "Ocurrio un error al Insertar el registro con id" + cat_.id_categoria + " " + ex.Message

                //});
                return BadRequest(ex);
            }

            return Ok();
        }

        // PUT: api/Actualizar
        //[Authorize(Policy = "Cargo_mo")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Exists(model.cedula, model.id_empleado, 2))
            {
                return BadRequest("El numero de cedula ya existe");
            }


            var cat = await _context.empleado.FirstOrDefaultAsync(p => p.id_empleado == model.id_empleado);

            if (cat == null)
            {
                return NotFound("Registro no encontrado.");
            }

            cat.cedula = model.cedula;
            cat.primer_nombre = model.primer_nombre;
            cat.segundo_nombre = model.segundo_nombre;
            cat.primer_apellido = model.primer_apellido;
            cat.segundo_apellido = model.segundo_apellido;
            cat.telefono = model.telefono;
            cat.direccion = model.direccion;

            try
            {
                await _context.SaveChangesAsync();

                //bitacora.queryinsert(new Bitacora
                //{
                //    accion = "Actualizar",
                //    controlador = "Categoria",
                //    usuario = "admin",
                //    descripcion = "Se Actulizo un nuevo registro con el id " + cat.id_categoria

                //});
            }
            catch (Exception ex)
            {
                //bitacora.queryinsert(new Bitacora
                //{
                //    accion = "Actualizar",
                //    controlador = "Categoria",
                //    usuario = "admin",
                //    descripcion = "Ocurrio un error al actualizar el registro con id" + cat.id_categoria + " " + ex.Message

                //});
                // Guardar Excepción
                return BadRequest(ex);

            }

            return Ok();
        }

        // GET: api/ListarClientes
        //[Authorize(Policy = "Cargo_lect")]
        [HttpGet("[action]/{usuario}")]
        public async Task<IEnumerable<ListarViewModel>> Listar([FromRoute] string usuario)
        {
            var tipo = await _context.empleado
                .OrderByDescending(c => c.id_empleado)
                .ToListAsync();

            //bitacora.queryinsert(new Bitacora
            //{
            //    id = 0,
            //    accion = "Listar",
            //    controlador = "Categoria",
            //    fecha = DateTime.Now,
            //    usuario = "admin",
            //    descripcion = "Se listo las categorias con un total de " + tipo.Count

            //});

            return tipo.Select(p => new ListarViewModel
            {
                id_empleado = p.id_empleado,
                cedula = p.cedula,
                nombre = p.primer_nombre + " " + p.segundo_nombre + " " + p.primer_apellido + " " + p.segundo_apellido,
                primer_nombre = p.primer_nombre,
                segundo_nombre = p.segundo_nombre,
                primer_apellido = p.segundo_nombre,
                segundo_apellido = p.segundo_apellido,
                telefono = p.telefono,
                direccion = p.direccion,
                estado = p.estado
            });




        }

        // GET: api/SelectClientes
        [HttpGet("[action]/{usuario}")]
        public async Task<IEnumerable<SelectViewModel>> Select([FromRoute] string usuario)
        {
            var tipo = await _context.empleado
                .OrderByDescending(c => c.id_empleado)
                .ToListAsync();

            return tipo.Select(p => new SelectViewModel
            {
                id_empleado = p.id_empleado,
                cedula = p.cedula,
                nombre = p.primer_nombre + " " + p.segundo_nombre + " " + p.primer_apellido + " " + p.segundo_apellido
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

            var cat = await _context.empleado.FirstOrDefaultAsync(p => p.id_empleado == id);

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

            var cat = await _context.empleado.FirstOrDefaultAsync(p => p.id_empleado == id);

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
        private bool Exists(string cedula, int id, int type)
        {

            bool result = false;

            if (type == 1)
            {
                var cat = _context.empleado.Where(p => p.cedula.ToUpper() == cedula.ToUpper()).FirstOrDefault();

                result = cat is null ? false : true;
            }

            if (type == 2)
            {
                var cat = _context.empleado.Where(p => p.id_empleado == id).FirstOrDefaultAsync();

                var otros = _context.empleado.Where(p => p.cedula == cat.Result.cedula).ToListAsync();

                result = otros.Result.Count() == 1 ? false : true;
            }

            return result;
        }

    }
}
