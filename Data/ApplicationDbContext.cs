using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace portInmbo.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<Inmueble> Inmuebles { get; set; }
    public DbSet<Visita> Visitas { get; set; }
    public DbSet<Reserva> Reservas { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Inmueble>()
               .HasIndex(i => i.Codigo)
               .IsUnique();

        // Seed: mínimo 3-4 inmuebles
        builder.Entity<Inmueble>().HasData(
            new Inmueble { Id = 1, Codigo = "A-001", Titulo = "Depto céntrico 1", Tipo = TipoInmueble.Departamento, Ciudad="Lima", Direccion="Av. X 123", Dormitorios=2, Banos=2, MetrosCuadrados=60, Precio=120000, Activo=true, Imagen="img1.jpg" },
            new Inmueble { Id = 2, Codigo = "C-002", Titulo = "Casa con patio", Tipo = TipoInmueble.Casa, Ciudad="Lima", Direccion="Calle Y 456", Dormitorios=3, Banos=2, MetrosCuadrados=120, Precio=250000, Activo=true, Imagen="img2.jpg" },
            new Inmueble { Id = 3, Codigo = "O-003", Titulo = "Oficina pequeña", Tipo = TipoInmueble.Oficina, Ciudad="San Isidro", Direccion="Of. 12", Dormitorios=0, Banos=1, MetrosCuadrados=35, Precio=90000, Activo=true, Imagen="img3.jpg" }
        );
    }
}

