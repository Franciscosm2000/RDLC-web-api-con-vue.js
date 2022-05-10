using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Rentas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Rentas
{
    public class RentaMap : IEntityTypeConfiguration<Renta>
    {
        public void Configure(EntityTypeBuilder<Renta> builder)
        {
            builder.ToTable("Renta")
               .HasKey(c => c.id_renta);
        }
    }
}
