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
using Sistema.Web.Models.Vehiculos.Categoria;

namespace Sistema.Web.Controllers.Vehiculos
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        BitacoraSP bitacora = new BitacoraSP();
        public CategoriasController(DbContextSistema context)
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


            if (CategoriaExists(model.nombre,0,1)) {
                return BadRequest("El nombre ya existe");
            }


            Categoria cat_ = new Categoria
            {
                nombre = model.nombre,
                descripcion = model.descripcion,
                estado = true
            };

            _context.categoria.Add(cat_);
            try
            {
                await _context.SaveChangesAsync();

                bitacora.queryinsert(new Bitacora
                {
                    accion = "Insertar",
                    controlador = "Categoria",
                    usuario = "admin",
                    descripcion = "Se Inserto un nuevo registro con el id " + cat_.id_categoria

                });

            }
            catch (Exception ex)
            {
                bitacora.queryinsert(new Bitacora
                {
                    accion = "Insertar",
                    controlador = "Categoria",
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

            if (CategoriaExists(model.nombre, model.id_categoria, 2))
            {
                return BadRequest("El nombre ya existe");
            }


            var cat = await _context.categoria.FirstOrDefaultAsync(p => p.id_categoria == model.id_categoria);

            if (cat == null)
            {
                return NotFound("Registro no encontrado.");
            }

            cat.nombre = model.nombre;
            cat.descripcion = model.descripcion;

            try
            {
                await _context.SaveChangesAsync();

                bitacora.queryinsert(new Bitacora
                {
                    accion = "Actualizar",
                    controlador = "Categoria",
                    usuario = "admin",
                    descripcion = "Se Actulizo un nuevo registro con el id " + cat.id_categoria

                });
            }
            catch (Exception ex)
            {
                bitacora.queryinsert(new Bitacora
                {
                    accion = "Actualizar",
                    controlador = "Categoria",
                    usuario = "admin",
                    descripcion = "Ocurrio un error al actualizar el registro con id" + cat.id_categoria+" "+ex.Message

                });
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
            var tipo = await _context.categoria.OrderByDescending(c => c.id_categoria)
                .ToListAsync();

            bitacora.queryinsert(new Bitacora
            {
                id = 0,
                accion = "Listar",
                controlador = "Categoria",
                fecha = DateTime.Now,
                usuario = "admin",
                descripcion = "Se listo las categorias con un total de " +tipo.Count 

            });

            return tipo.Select(p => new ListarViewModel
            {
                id_categoria = p.id_categoria,
                nombre = p.nombre,
                descripcion =p.descripcion,
                estado =p.estado
            });


            

        }

        // GET: api/SelectClientes
        [HttpGet("[action]/{usuario}")]
        public async Task<IEnumerable<SelectViewModel>> Select([FromRoute] string usuario)
        {
            var tipo = await _context.categoria
                .Where(c=> c.estado == true)
                .OrderByDescending(c => c.id_categoria )
                .ToListAsync();

            bitacora.queryinsert(new Bitacora
            {
                accion = "Select",
                controlador = "Categoria",
                usuario = "admin",
                descripcion = "Se selecciono un total de registros : " +tipo.Count() 
            });

            return tipo.Select(p => new SelectViewModel
            {
                id_categoria = p.id_categoria,
                nombre = p.nombre
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

            var cat = await _context.categoria.FirstOrDefaultAsync(p => p.id_categoria == id);

            if (cat == null)
            {
                return NotFound("Registro no encontrado.");
            }

            cat.estado = false;

            try
            {
                await _context.SaveChangesAsync();

                bitacora.queryinsert(new Bitacora
                {
                    accion = "Inactivar",
                    controlador = "Categoria",
                    usuario = "admin",
                    descripcion = "Se Inactivo el registro con el id " + cat.id_categoria

                });
            }
            catch (Exception ex)
            {
                bitacora.queryinsert(new Bitacora
                {
                    accion = "Inactivar",
                    controlador = "Categoria",
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

            var cat = await _context.categoria.FirstOrDefaultAsync(p => p.id_categoria == id);

            if (cat == null)
            {
                return NotFound("Id no encontrado.");
            }

            cat.estado = true;

            bitacora.queryinsert(new Bitacora
            {
                accion = "Inactivar",
                controlador = "Categoria",
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
                    controlador = "Categoria",
                    usuario = "admin",
                    descripcion = "Ocurrio un error al activar el registro con id" + cat.id_categoria + " " + ex.Message

                });
                // Guardar Excepción
                return BadRequest(ex);
            }

            return Ok();
        }

        private bool CategoriaExists(string categ, int id, int type)
        {
           
            bool result = false;

            if (type == 1){
                var cat = _context.categoria.Where(p => p.nombre.ToUpper() == categ.ToUpper()).FirstOrDefault();

                result = cat is null ? false : true;
            }
                 
            if (type == 2) {
                var cat = _context.categoria.Where(p=> p.id_categoria == id).FirstOrDefaultAsync();

                var otros = _context.categoria.Where(p => p.nombre == cat.Result.nombre).ToListAsync();

                result = otros.Result.Count() == 1 ? false : true;
            }

            return result;
        }
    }
}
