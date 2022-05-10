using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Persona;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Persona
{
    class EmpleadoMap : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.ToTable("Empleado")
               .HasKey(c => c.id_empleado);
        }
    }
}
