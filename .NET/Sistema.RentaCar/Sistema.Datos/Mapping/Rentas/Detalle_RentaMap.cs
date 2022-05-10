using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Rentas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Rentas
{
    public class Detalle_RentaMap : IEntityTypeConfiguration<Detalle_Renta>
    {
        public void Configure(EntityTypeBuilder<Detalle_Renta> builder)
        {
            builder.ToTable("Detalle_Renta")
               .HasKey(c => c.id_detalle_renta);
        }
    }
}
