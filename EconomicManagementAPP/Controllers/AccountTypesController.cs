using EconomicManagementAPP.Models;
using EconomicManagementAPP.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class AccountTypesController : Controller
    {
        private readonly IRepositorieAccountTypes repositorieAccountTypes;
        private readonly IUserServices userServices;

        public AccountTypesController(IRepositorieAccountTypes repositorieAccountTypes,
                                      IUserServices userServices)
        {
            this.repositorieAccountTypes = repositorieAccountTypes;
            this.userServices = userServices;
        }

        // Creamos index para ejecutar la interfaz
        // Task : Representa una operación asincrónica que puede devolver un valor.
        public async Task<IActionResult> Index()
        {
            // Simula que estamos logeados en la app.
            var userId = userServices.GetUserId();
            var accountTypes = await repositorieAccountTypes.GetAccounts(userId);
            return View(accountTypes);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountTypes accountTypes)
        {
            if (!ModelState.IsValid)
            {
                return View(accountTypes);
            }

            accountTypes.UserId = userServices.GetUserId();

            // Validamos si ya existe antes de registrar
            var accountTypeExist =
               await repositorieAccountTypes.Exist(accountTypes.Name, accountTypes.UserId);

            if (accountTypeExist)
            {
                // AddModelError ya viene predefinido en .net
                // nameOf es el tipo del campo
                ModelState.AddModelError(nameof(accountTypes.Name),
                    $"The account {accountTypes.Name} already exist.");

                return View(accountTypes);
            }
            await repositorieAccountTypes.Create(accountTypes);
            // Redireccionamos a la lista
            return RedirectToAction("Index");
        }

        // Hace que la validacion se active automaticamente desde el front
        [HttpGet]
        public async Task<IActionResult> VerificaryAccountType(string Name)
        {
            var UserId = userServices.GetUserId();
            var accountTypeExist = await repositorieAccountTypes.Exist(Name, UserId);

            if (accountTypeExist)
            {
                // permite acciones directas entre front y back
                return Json($"The account {Name} already exist");                                                                
            }

            return Json(true);
        }

        //Actualizar
        [HttpGet]
        public async Task<ActionResult> Modify(int id)
        {
            var userId = userServices.GetUserId();

            var accountType = await repositorieAccountTypes.GetAccountById(id, userId);

            if (accountType is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(accountType);
        }
        [HttpPost]
        public async Task<ActionResult> Modify(AccountTypes accountTypes)
        {
            var userId = userServices.GetUserId();
            var accountType = await repositorieAccountTypes.GetAccountById(accountTypes.Id, userId);

            if (accountType is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccountTypes.Modify(accountTypes);// el que llega
            return RedirectToAction("Index");
        }
        // Eliminar
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = userServices.GetUserId();
            var account = await repositorieAccountTypes.GetAccountById(id, userId);

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
            var account = await repositorieAccountTypes.GetAccountById(id, userId);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var isUsed = await repositorieAccountTypes.AccountTypeIsUsed(id);

            if (isUsed)
            {
                return RedirectToAction("Error", "Home");
            }

            await repositorieAccountTypes.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
