using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Vehiculos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Vehiculos
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria")
               .HasKey(c => c.id_categoria);
        }
    }
}
