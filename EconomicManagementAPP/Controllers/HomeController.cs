using AutoMapper;
using EconomicManagementAPP.Interfaces;
using EconomicManagementAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace EconomicManagementAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositorieAccounts repositorieAccounts;
        private readonly IRepositorieTransactions repositorieTransactions;
        private readonly IUserServices userServices;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger,
                              IRepositorieAccounts repositorieAccounts,
                              IRepositorieTransactions repositorieTransactions,
                              IUserServices userServices,
                              IMapper mapper)
        {
            _logger = logger;
            this.repositorieAccounts = repositorieAccounts;
            this.repositorieTransactions = repositorieTransactions;
            this.userServices = userServices;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var userId = userServices.GetUserId();
            var model = new AccountsIndexViewModel();
            model.Accounts = await GetAccounts(userId);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Interfaz para error de no encontrar el id
        public IActionResult NotFound()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<IEnumerable<SelectListItem>> GetAccounts(int userId)
        {
            var accounts = await repositorieAccounts.GetAccounts(userId);
            return accounts.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
        }

        [HttpPost]
        public async Task<IActionResult> GetTransactions([FromBody] int accountId)
        {
            var userId = userServices.GetUserId();
            var transactions = await repositorieTransactions.GetTransactions(accountId, userId);
            var account = await repositorieAccounts.GetAccountById(accountId, userId);
            return Ok(new { transactions, account });
        }
    }
}