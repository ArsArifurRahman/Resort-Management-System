using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

namespace Web.Controllers;
public class VillaController : Controller
{
    private readonly DataContext _context;

    public VillaController(DataContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Villas.ToList());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Villa villa)
    {
        if (villa.Name == villa.Description)
        {
            ModelState.AddModelError("name", "The description cannot exactly match the Name.");
        }

        if (ModelState.IsValid)
        {
            _context.Villas.Add(villa);
            _context.SaveChanges();
            return RedirectToAction("Index", "Villa");
        }
        return View();
    }
}
