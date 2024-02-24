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

    public IActionResult Update(int? id)
    {
        Villa? villa = _context.Villas.FirstOrDefault(x => x.Id == id);

        if (villa is null)
        {
            return RedirectToAction("Error", "Home");
        }

        return View(villa);
    }

    [HttpPost]
    public IActionResult Update(Villa villa)
    {
        if (ModelState.IsValid && villa.Id > 0)
        {
            _context.Villas.Update(villa);
            _context.SaveChanges();
            return RedirectToAction("Index", "Villa");
        }
        return View();
    }

    public IActionResult Delete(int? id)
    {
        Villa? villa = _context.Villas.FirstOrDefault(x => x.Id == id);

        if (villa is null)
        {
            return RedirectToAction("Error", "Home");
        }

        return View(villa);
    }

    [HttpPost]
    public IActionResult Delete(Villa villa)
    {
        Villa? villaToDelete = _context.Villas.FirstOrDefault(x => x.Id == villa.Id);
        if (villaToDelete is not null)
        {
            _context.Villas.Remove(villaToDelete);
            _context.SaveChanges();
            return RedirectToAction("Index", "Villa");
        }
        return View();
    }
}
