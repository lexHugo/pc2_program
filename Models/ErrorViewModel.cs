namespace portInmbo.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}

public enum TipoInmueble { Departamento, Casa, Oficina, Local }
public enum VisitaEstado { Solicitada, Confirmada, Cancelada }

public class Inmueble
{
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string Codigo { get; set; }   // único

    [Required] public string Titulo { get; set; }
    public string Imagen { get; set; }
    public TipoInmueble Tipo { get; set; }
    public string Ciudad { get; set; }
    public string Direccion { get; set; }

    [Range(0, int.MaxValue)] public int Dormitorios { get; set; }
    [Range(0, int.MaxValue)] public int Banos { get; set; }

    [Range(0.01,double.MaxValue)]
    public double MetrosCuadrados { get; set; }

    [Range(0.01,double.MaxValue)]
    public decimal Precio { get; set; }

    public bool Activo { get; set; } = true;
}

public class Visita
{
    public int Id { get; set; }
    public int InmuebleId { get; set; }
    public string UsuarioId { get; set; }    // Identity user id
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public VisitaEstado Estado { get; set; } = VisitaEstado.Solicitada;
    public string Notas { get; set; }

    public Inmueble Inmueble { get; set; }
}

public class Reserva
{
    public int Id { get; set; }
    public int InmuebleId { get; set; }
    public string UsuarioId { get; set; }
    public DateTime FechaExpiracion { get; set; }
    public DateTime FechaCreacion { get; set; }

    public Inmueble Inmueble { get; set; }
}
