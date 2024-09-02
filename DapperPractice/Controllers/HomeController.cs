using DapperPractice.Models;
using DapperPractice.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DapperPractice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CustomerService _customerService;
        public HomeController(ILogger<HomeController> logger, CustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetAllCustomers();
            return View(customers);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.GetCustomerByID(id);
            return View(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] Customer customer)
        {
            var customerUpdated = await _customerService.UpdateCustomer(customer);
            return View(customerUpdated);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.DeleteCustomerByID(id);
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
