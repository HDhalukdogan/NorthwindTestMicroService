using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WebUI.Models;
using WebUI.Services.Interfaces;

namespace WebUI.Controllers
{
    [Authorize]
    public class SuppliersController : Controller
    {
        private readonly INorthwindService _northwindService;
        public SuppliersController(INorthwindService northwindService)
        {
            _northwindService = northwindService;
        }
        // GET: SupliersController
        public async Task<ActionResult> Index()
        {
            //var testt = User.Identity.Name;
            //var claimstest = User.Claims.FirstOrDefault(s => s.Type == "token").Value;
            //var token = User.FindFirst("token").Value;
            //var isAuth = User.Identity.IsAuthenticated;

            ////var client = new HttpClient() { BaseAddress = new Uri("http://localhost:5042/api/") };
            //var client = new HttpClient() { BaseAddress = new Uri("http://localhost:5000/northwindtest/") };
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            ////var suppliers = await client.GetFromJsonAsync<List<Supplier>>("Suppliers");
            //var response = await client.GetAsync("suppliers");
            //var suppliers = await response.Content.ReadFromJsonAsync<List<Supplier>>();
            var suppliers = await _northwindService.GetAllSupplierAsync();
            return View(suppliers);
        }

        // GET: SupliersController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //var client = new HttpClient() { BaseAddress = new Uri("http://localhost:5042/api/") };
            var client = new HttpClient() { BaseAddress = new Uri("http://localhost:5000/northwindtest/") };
            var supplier = await client.GetFromJsonAsync<Supplier>($"Suppliers/{id}");
            return View(supplier);
        }

        // GET: SupliersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupliersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SupliersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SupliersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SupliersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SupliersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
