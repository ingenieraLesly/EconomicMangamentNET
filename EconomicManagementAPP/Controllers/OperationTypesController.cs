using EconomicManagementAPP.Models;
using EconomicManagementAPP.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class OperationTypesController : Controller
    {
        private readonly IRepositorieOperationTypes repositorieOperationTypes;
        public OperationTypesController(IRepositorieOperationTypes repositorieOperationTypes)
        {
            this.repositorieOperationTypes = repositorieOperationTypes;
        }

        public async Task<IActionResult> Index()
        {
            var operationTypes = await repositorieOperationTypes.GetOperationTypes();
            return View(operationTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OperationTypes operationTypes)
        {
            if (!ModelState.IsValid)
            {
                return View(operationTypes);
            }

            var operationTypesExist =
               await repositorieOperationTypes.Exist(operationTypes.Description);

            if (operationTypesExist)
            {
                ModelState.AddModelError(nameof(operationTypes.Description),
                    $"The operationTypes {operationTypes.Description} already exist.");

                return View(operationTypes);
            }
            await repositorieOperationTypes.Create(operationTypes);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerificaryAccountType(string Description)
        {
            var operationTypesExist = await repositorieOperationTypes.Exist(Description);

            if (operationTypesExist)
            {
                return Json($"The operationTypes {Description} already exist");
            }

            return Json(true);
        }

        [HttpGet]
        public async Task<ActionResult> Modify(int id)
        {
            var operationTypes = await repositorieOperationTypes.GetOperationTypeById(id);

            if (operationTypes is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(operationTypes);
        }

        [HttpPost]
        public async Task<ActionResult> Modify(OperationTypes operationTypes)
        {
            var operationTypesExists = await repositorieOperationTypes.GetOperationTypeById(operationTypes.Id);

            if (operationTypesExists is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieOperationTypes.Modify(operationTypes);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var operationTypes = await repositorieOperationTypes.GetOperationTypeById(id);

            if (operationTypes is null)
            {
                return RedirectToAction("NotFount", "Home");
            }

            return View(operationTypes);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var operationTypes = await repositorieOperationTypes.GetOperationTypeById(id);

            if (operationTypes is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieOperationTypes.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
