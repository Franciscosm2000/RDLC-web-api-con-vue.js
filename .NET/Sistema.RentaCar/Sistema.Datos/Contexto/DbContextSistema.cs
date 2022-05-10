using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.Persona;
using Sistema.Datos.Mapping.Rentas;
using Sistema.Datos.Mapping.Vehiculos;
using Sistema.Entidades.Persona;
using Sistema.Entidades.Rentas;
using Sistema.Entidades.Vehiculos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Contexto
{
    public class DbContextSistema : DbContext
    {
        #region DBSET

        #region PERSONA
        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Empleado> empleado { get; set; }
        #endregion

        #region Rentas
        public DbSet<Detalle_Renta> detalle_renta { get; set; }
        public DbSet<Renta> renta { get; set; }
        #endregion

        #region Vehiculos
        public DbSet<Categoria> categoria { get; set; }
        public DbSet<Modelo_Vehiculo> modelo_vehiculo { get; set; }
        public DbSet<Vehiculo> vehiculo { get; set; }
        #endregion
        /*

        #region SEGURIDAD
        public DbSet<permisos> premiso { get; set; }
        public DbSet<roles> rol { get; set; }
        public DbSet<usuario> usuario { get; set; }
        #endregion*/
        #endregion

        public DbContextSistema(DbContextOptions<DbContextSistema> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region MODELBUILDER

            #region CLIENTES
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new EmpleadoMap());
            #endregion

            #region RENTAS
            modelBuilder.ApplyConfiguration(new Detalle_RentaMap());
            modelBuilder.ApplyConfiguration(new RentaMap());
            #endregion

            #region VEHICULOS
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new Modelo_VehiculoMap());
            modelBuilder.ApplyConfiguration(new VehiculoMap());
            #endregion
            /*
            #region SEGURIDAD
            modelBuilder.ApplyConfiguration(new permisoMap());
            modelBuilder.ApplyConfiguration(new rolesMap());
            modelBuilder.ApplyConfiguration(new usuarioMap());
            #endregion*/
            #endregion
        }
    }
}
