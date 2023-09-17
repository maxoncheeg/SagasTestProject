using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SagasTestProject.Models;
using SagasTestProject.SagasService.Contracts;
using System.Diagnostics;

namespace SagasTestProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBus _bus;

        public HomeController(IBus bus, ILogger<HomeController> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task<IActionResult> SagaTest()
        {
            var x = Guid.NewGuid();
            var response = await _bus.Request<BuyItemsRequest, BuyItemsResponse>(new BuyItemsRequestModel
            {
                OrderId = x,
            });
            ViewBag.Result = x + " -- " + response.Message.OrderId;
            return View("Index");
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
}