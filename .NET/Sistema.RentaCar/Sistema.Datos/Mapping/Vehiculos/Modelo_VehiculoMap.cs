using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Vehiculos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Vehiculos
{
    public class Modelo_VehiculoMap : IEntityTypeConfiguration<Modelo_Vehiculo>
    {
        public void Configure(EntityTypeBuilder<Modelo_Vehiculo> builder)
        {
            builder.ToTable("Modelo_Vehiculo")
                .HasKey(c => c.id_modelo);
        }
    }
}
