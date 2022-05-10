using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Vehiculos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Vehiculos
{
    public class VehiculoMap : IEntityTypeConfiguration<Vehiculo>
    {
        public void Configure(EntityTypeBuilder<Vehiculo> builder)
        {
            builder.ToTable("Vehiculo")
               .HasKey(c => c.id_vehiculo);
        }
    }
}
