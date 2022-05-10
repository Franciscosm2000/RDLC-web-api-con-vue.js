using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Contexto;
using Sistema.Entidades.Vehiculos;
using Sistema.Web.Models.Vehiculos.Vehiculo;

namespace Sistema.Web.Controllers.Vehiculos
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public VehiculosController(DbContextSistema context)
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

            Vehiculo cat_ = new Vehiculo
            {
                id_modelo = model.id_modelo,
                placa = model.placa,
                no_Chasis = model.no_Chasis,
                motor = model.motor,
                color = model.color,
                Costo_dia = model.Costo_dia,
                estado = true
            };

            _context.vehiculo.Add(cat_);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
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


            var cat = await _context.vehiculo.FirstOrDefaultAsync(p => p.id_vehiculo == model.id_vehiculo);

            if (cat == null)
            {
                return NotFound("Registro no encontrado.");
            }

            cat.id_modelo = model.id_modelo;
            cat.placa = model.placa;
            cat.no_Chasis = model.no_Chasis;
            cat.motor = model.motor;
            cat.color = model.color;
            cat.Costo_dia = model.Costo_dia;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
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
            var tipo = await _context.vehiculo
                .Include(d=> d.modelo_vehiculo)
                .OrderByDescending(c => c.id_vehiculo)
                .ToListAsync();

            return tipo.Select(p => new ListarViewModel
            {
                id_vehiculo = p.id_vehiculo,
                id_modelo = p.id_modelo,
                modelo = p.modelo_vehiculo.modelo,
                placa = p.placa,
                no_Chasis = p.no_Chasis,
                motor = p.motor,
                color = p.color,
                Costo_dia = p.Costo_dia,
                estado = p.estado
            });

        }

        // GET: api/SelectClientes
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var tipo = await _context.vehiculo
                .Include(d=> d.modelo_vehiculo)
                .Where(c => c.estado == true)
                .OrderByDescending(c => c.id_vehiculo)
                .ToListAsync();

            return tipo.Select(p => new SelectViewModel
            {
                id_vehiculo = p.id_vehiculo,
                id_modelo = p.id_modelo,
                modelo = p.modelo_vehiculo.modelo,
                placa = p.placa,
                precio_dia = Math.Round(p.Costo_dia)
            });;

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

            var cat = await _context.vehiculo.FirstOrDefaultAsync(p => p.id_vehiculo == id);

            if (cat == null)
            {
                return NotFound("Registro no encontrado.");
            }

            cat.estado = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
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

            var cat = await _context.vehiculo.FirstOrDefaultAsync(p => p.id_vehiculo == id);

            if (cat == null)
            {
                return NotFound("Id no encontrado.");
            }

            cat.estado = true;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Guardar Excepción
                return BadRequest(ex);
            }

            return Ok();
        }

        private bool CategoriaExists(string categ, int id, int type)
        {

            bool result = false;

            if (type == 1)
            {
                var cat = _context.categoria.Where(p => p.nombre.ToUpper() == categ.ToUpper()).FirstOrDefault();

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
