using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Persona;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Persona
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente")
               .HasKey(c => c.id_cliente);
        }
    }
}
