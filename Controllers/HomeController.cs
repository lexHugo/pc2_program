using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using portInmbo.Models;

namespace portInmbo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public async Task<IActionResult> Index(string ciudad, TipoInmueble? tipo, decimal? precioMin, decimal? precioMax, int? dormitorios, int page = 1)
{
    // Validaciones básicas
    if (precioMin < 0 || precioMax < 0 || (dormitorios.HasValue && dormitorios < 0))
        ModelState.AddModelError("", "Los valores numéricos no pueden ser negativos.");

    if (precioMin.HasValue && precioMax.HasValue && precioMin > precioMax)
        ModelState.AddModelError("", "Precio mínimo no puede ser mayor que precio máximo.");

    if (!ModelState.IsValid)
    {
        // devolver vista con mensajes
    }

    var query = _context.Inmuebles.AsNoTracking().Where(i => i.Activo);

    if (!string.IsNullOrEmpty(ciudad)) query = query.Where(i => i.Ciudad == ciudad);
    if (tipo.HasValue) query = query.Where(i => i.Tipo == tipo);
    if (precioMin.HasValue) query = query.Where(i => i.Precio >= precioMin.Value);
    if (precioMax.HasValue) query = query.Where(i => i.Precio <= precioMax.Value);
    if (dormitorios.HasValue) query = query.Where(i => i.Dormitorios >= dormitorios.Value);

    // Paginación
    const int pageSize = 6;
    var items = await query.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();

    return View(items); // view recibe lista + metadata de pag
}
