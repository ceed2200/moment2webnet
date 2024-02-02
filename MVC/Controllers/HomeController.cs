using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Text.Json;

namespace MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // Mer specifikt innehåll för olika routes
    [Route("/")]
    [Route("/start")]
    public IActionResult Index()
    {
        // Ta fram lista med städer
        string jsonStr = System.IO.File.ReadAllText("./wwwroot/cities.json");
        var cities = JsonSerializer.Deserialize<List<CityModel>>(jsonStr);

        // Ta fram antal lagrade städer
        var citiesAmount = cities.Count;

        // Transport av data kring antal städer
        ViewBag.tellAmount = citiesAmount;
        
        return View(cities);
    }

    [Route("/about")]
    public IActionResult About()
    {
        // Transport av data kring webbplatsens skapare
        ViewData["CreatorName"] = "Cecilia Edvardsson";
        return View();
    }

    [Route("/counties")]
    public IActionResult Counties()
    {
        // Lista med landskap som transporteras till vyn
        List<string> counties = new List<string> () {
            "Småland",
            "Närke",
            "Södermanland",
            "Östergötland",
            "Uppland",
            "Blekinge",
            "Öland",
            "Gotland",
            "Västerbotten",
            "Norrbotten",
            "Lappland",
            "Härjedalen",
            "Hälsingland",
            "Dalarna",
            "Västergötland",
            "Västmanland",
            "Medelpad",
            "Skåne",
            "Halland",
            "Värmland",
            "Jämtland",
            "Gästrikland",
            "Dalsland",
            "Bohuslän"
        };
        return View(counties);
    }

    [Route("/cities")]
    public IActionResult Cities()
    {
        return View();
    }

    // cities-sidan men vid POST-anrop
    [Route("/cities")]
    [HttpPost]
    public IActionResult Cities(CityModel model)
    {
        if (ModelState.IsValid) { // vid godkända inputs
            string jsonStr = System.IO.File.ReadAllText("./wwwroot/cities.json");
            var cities = JsonSerializer.Deserialize<List<CityModel>>(jsonStr);

            // lagring och formulärtömmande vid existens av lista med lagrade städer
            if (cities != null) {
                cities.Add(model);

                jsonStr = JsonSerializer.Serialize(cities);
                System.IO.File.WriteAllText("./wwwroot/cities.json", jsonStr);

                ModelState.Clear();
            }
        } 

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
