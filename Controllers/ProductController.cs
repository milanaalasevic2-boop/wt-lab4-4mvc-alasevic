using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_alasevic.Models;

namespace wt_lab4_4mvc_alasevic.Controllers;

public class ProductController : Controller
{
    private static readonly List<Product> Products = new()
    {
        new Product { Id = 1, Name = "Ноутбук Lenovo IdeaPad 5", Category = "Компьютеры",  Price = 74999m, IsAvailable = true  },
        new Product { Id = 2, Name = "Смартфон Samsung S24",      Category = "Смартфоны",   Price = 89999m, IsAvailable = true  },
        new Product { Id = 3, Name = "Мышь Logitech MX Master",   Category = "Аксессуары",  Price = 9990m,  IsAvailable = true  },
        new Product { Id = 4, Name = "Клавиатура Keychron K2",    Category = "Аксессуары",  Price = 12990m, IsAvailable = false },
        new Product { Id = 5, Name = "Монитор Dell 27\"",          Category = "Мониторы",    Price = 24990m, IsAvailable = true  }
    };

    // /Product  
    public IActionResult Index()
    {
        return View(Products);
    }


    public IActionResult Details(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();     // 404

        return View(product);
    }
        // GET: /Product/Create — показать форму
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);   
        }

        // новый Id
        int newId = Products.Any()
            ? Products.Max(p => p.Id) + 1
            : 1;

        product.Id = newId;

        Products.Add(product);

        return RedirectToAction("Index");
    }
       
    public IActionResult Edit(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (!ModelState.IsValid)
            return View(product);

        var existing = Products.FirstOrDefault(p => p.Id == product.Id);

        if (existing == null)
            return NotFound();

        existing.Name = product.Name;
        existing.Category = product.Category;
        existing.Price = product.Price;
        existing.IsAvailable = product.IsAvailable;

        return RedirectToAction("Index");
    }
      
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        Products.Remove(product);

        return RedirectToAction("Index");
    }
}