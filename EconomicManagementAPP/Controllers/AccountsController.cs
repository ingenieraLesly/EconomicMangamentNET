using EconomicManagementAPP.Interfaces;
using EconomicManagementAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;

namespace EconomicManagementAPP.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IRepositorieAccounts repositorieAccounts;
        private readonly IRepositorieAccountTypes repositorieAccountTypes;
        private readonly IUserServices userServices;
        private readonly IMapper mapper;

        public AccountsController(IRepositorieAccounts repositorieAccounts,
                                  IRepositorieAccountTypes repositorieAccountTypes,
                                  IUserServices userServices,
                                  IMapper mapper)
        {
            this.repositorieAccounts = repositorieAccounts;
            this.repositorieAccountTypes = repositorieAccountTypes;
            this.userServices = userServices;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var userId = userServices.GetUserId();
            var model = new AccountsViewModel();
            model.AccountTypes = await GetAccountTypes(userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Accounts account)
        {
            var userId = userServices.GetUserId();
            if (!ModelState.IsValid)
            {
                return View(account);
            }

            var accountExist =
               await repositorieAccounts.Exist(account.Name, userId);

            if (accountExist)
            {
                ModelState.AddModelError(nameof(account.Name),
                    $"The account {account.Name} already exist.");

                return View(account);
            }
            await repositorieAccounts.Create(account);
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public async Task<IActionResult> VerificaryAccountType(string Name)
        {
            var UserId = 1;
            var accountExist = await repositorieAccounts.Exist(Name, UserId);

            if (accountExist)
            {
                return Json($"The account {Name} already exist");
            }

            return Json(true);
        }

        [HttpGet]
        public async Task<ActionResult> Modify(int id)
        {
            var userId = userServices.GetUserId();

            var account = await repositorieAccounts.GetAccountById(id, userId);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var model = mapper.Map<AccountsViewModel>(account);
            model.AccountTypes = await GetAccountTypes(userId);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Modify(Accounts account)
        {
            var userId = userServices.GetUserId();
            var accountExists = await repositorieAccounts.GetAccountById(account.Id, userId);

            if (accountExists is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccounts.Modify(account);
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = userServices.GetUserId();
            var account = await repositorieAccounts.GetAccountById(id, userId);

            if (account is null)
            {
                return RedirectToAction("NotFount", "Home");
            }

            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var userId = userServices.GetUserId();
            var account = await repositorieAccounts.GetAccountById(id, userId);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccounts.Delete(id);
            return RedirectToAction("Index","Home");
        }

        private async Task<IEnumerable<SelectListItem>> GetAccountTypes(int userId) 
        {
            var accountTypes = await repositorieAccountTypes.GetAccounts(userId);
            return accountTypes.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
        }
    }
}
