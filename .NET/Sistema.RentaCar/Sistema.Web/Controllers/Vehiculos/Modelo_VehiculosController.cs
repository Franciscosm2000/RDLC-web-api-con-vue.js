using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Contexto;
using Sistema.Datos.ContextoSp.Bitacoras;
using Sistema.Entidades.Seguridad;
using Sistema.Entidades.Vehiculos;
using Sistema.Web.Models.Vehiculos.Modelo_Vehiculo;

namespace Sistema.Web.Controllers.Vehiculos
{
    [Route("api/[controller]")]
    [ApiController]
    public class Modelo_VehiculosController : ControllerBase
    {
        private readonly DbContextSistema _context;
        BitacoraSP bitacora = new BitacoraSP();

        public Modelo_VehiculosController(DbContextSistema context)
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


            /*if (CategoriaExists(model.nombre, 0, 1))
            {
                return BadRequest("El nombre ya existe");
            }*/


            Modelo_Vehiculo cat_ = new Modelo_Vehiculo
            {
                id_categoria = model.id_categoria,
                marca = model.marca,
                modelo = model.modelo,
                combustible = model.combustible,
                tipo_carroceria = model.tipo_carroceria,
                estado = true
            };

            _context.modelo_vehiculo.Add(cat_);
            try
            {
                bitacora.queryinsert(new Bitacora
                {
                    accion = "Insertar",
                    controlador = "Modelo",
                    usuario = "admin",
                    descripcion = "Se Inserto un nuevo registro con el id " + cat_.id_categoria

                });

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                bitacora.queryinsert(new Bitacora
                {
                    accion = "Insertar",
                    controlador = "Modelo",
                    usuario = "admin",
                    descripcion = "Ocurrio un error al Insertar el registro con id" + cat_.id_categoria + " " + ex.Message

                });

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

            //if (CategoriaExists(model.nombre, model.id_categoria, 2))
            //{
            //    return BadRequest("El nombre ya existe");
            //}


            var cat = await _context.modelo_vehiculo.FirstOrDefaultAsync(p => p.id_modelo == model.id_modelo);

            if (cat == null)
            {
                return NotFound("Registro no encontrado.");
            }

            cat.id_categoria = model.id_categoria;
            cat.marca = model.marca;
            cat.modelo = model.modelo;
            cat.combustible = model.combustible;
            cat.tipo_carroceria = model.tipo_carroceria;

            try
            {
                bitacora.queryinsert(new Bitacora
                {
                    accion = "Actualizar",
                    controlador = "Modelo",
                    usuario = "admin",
                    descripcion = "Se Actulizo un nuevo registro con el id " + cat.id_categoria

                });
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                bitacora.queryinsert(new Bitacora
                {
                    accion = "Actualizar",
                    controlador = "Categoria",
                    usuario = "admin",
                    descripcion = "Ocurrio un error al actualizar el registro con id" + cat.id_categoria + " " + ex.Message

                });
                // Guardar Excepción
                return BadRequest(ex);
            }

            return Ok();
        }

        // GET: api/ListarClientes
        //[Authorize(Policy = "Cargo_lect")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ListarViewModel>> Listar()
        {
            var tipo = await _context.modelo_vehiculo
                .Include(d=> d.categoria)
                .OrderByDescending(c => c.id_modelo)
                .ToListAsync();

            bitacora.queryinsert(new Bitacora
            {
                accion = "Select",
                controlador = "Modelo",
                usuario = "admin",
                descripcion = "Se selecciono un total de registros : " + tipo.Count()
            });

            return tipo.Select(p => new ListarViewModel
            {
                id_modelo = p.id_modelo,
                id_categoria = p.id_categoria,
                categoria = p.categoria.nombre,
                marca = p.marca,
                modelo = p.modelo,
                combustible = p.combustible,
                tipo_carroceria = p.tipo_carroceria,
                estado = p.estado
            });

        }

        // GET: api/SelectClientes
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var tipo = await _context.modelo_vehiculo
                .Include(d=> d.categoria)
                .Where(c => c.estado == true)
                .OrderByDescending(c => c.id_modelo)
                .ToListAsync();

            bitacora.queryinsert(new Bitacora
            {
                accion = "Inactivar",
                controlador = "Modelo",
                usuario = "admin",
                descripcion = "Se Inactivo el registro con el id " + tipo.Count()

            });


            return tipo.Select(p => new SelectViewModel
            {
                id_modelo = p.id_modelo,
                id_categoria = p.id_categoria,
                categoria = p.categoria.nombre,
                marca = p.marca,
                modelo = p.modelo
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

            var cat = await _context.modelo_vehiculo.FirstOrDefaultAsync(p => p.id_modelo == id);

            if (cat == null)
            {
                return NotFound("Registro no encontrado.");
            }

            cat.estado = false;


            bitacora.queryinsert(new Bitacora
            {
                accion = "Inactivar",
                controlador = "Modelo",
                usuario = "admin",
                descripcion = "Se activo el registro con el id " + cat.id_categoria

            });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                bitacora.queryinsert(new Bitacora
                {
                    accion = "Inactivar",
                    controlador = "Modelo",
                    usuario = "admin",
                    descripcion = "Ocurrio un error al inactivar el registro con id" + cat.id_categoria + " " + ex.Message

                });
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

            var cat = await _context.modelo_vehiculo.FirstOrDefaultAsync(p => p.id_modelo == id);

            if (cat == null)
            {
                return NotFound("Id no encontrado.");
            }

            cat.estado = true;


            try
            {
                bitacora.queryinsert(new Bitacora
                {
                    accion = "Activar",
                    controlador = "Modelo",
                    usuario = "admin",
                    descripcion = "Se activo el registro con el id " + cat.id_categoria
                   

                });
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                bitacora.queryinsert(new Bitacora
                {
                    accion = "Inactivar",
                    controlador = "Modelo",
                    usuario = "admin",
                    descripcion = "Ocurrio un error al activar el registro con id" + cat.id_categoria + " " + ex.Message

                });
                // Guardar Excepción
                return BadRequest(ex);
            }

            return Ok();
        }
        private bool CategoriaExists(string categ, int id_cat,int id, int type)
        {

            bool result = false;

            if (type == 1)
            {
                var cat = _context.modelo_vehiculo.Where(p => p.modelo.ToUpper() == categ.ToUpper() && p.id_categoria == id_cat).FirstOrDefault();

                result = cat is null ? false : true;
            }

            if (type == 2)
            {
                var cat = _context.categoria.Where(p => p.id_categoria == id).FirstOrDefaultAsync();

                var otros = _context.categoria.Where(p => p.nombre == cat.Result.nombre).ToListAsync();

                result = otros.Result.Count() == 1 ? false : true;
            }

            return result;
        }
    }
}
