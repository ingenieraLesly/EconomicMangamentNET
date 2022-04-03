using AutoMapper;
using EconomicManagementAPP.Models;
using EconomicManagementAPP.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EconomicManagementAPP.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IRepositorieCategories repositorieCategories;
        private readonly IRepositorieOperationTypes repositorieOperationTypes;
        private readonly IUserServices userServices;
        private readonly IMapper mapper;

        public CategoriesController(IRepositorieCategories repositorieCategories,
                                    IRepositorieOperationTypes repositorieOperationTypes,
                                    IUserServices userServices,
                                    IMapper mapper)
        {
            this.repositorieCategories = repositorieCategories;
            this.repositorieOperationTypes = repositorieOperationTypes;
            this.userServices = userServices;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var userId = userServices.GetUserId();
            var categorie = await repositorieCategories.GetCategories(userId);
            return View(categorie);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CategoriesViewModel();
            model.OperationTypes = await GetOperationTypes();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Categories categorie)
        {
            var userId = userServices.GetUserId();
            categorie.UserId = userId;

            if (!ModelState.IsValid)
            {
                return View(categorie);
            }

            var categorieExist =
               await repositorieCategories.Exist(categorie.Name, userId);

            if (categorieExist)
            {
                ModelState.AddModelError(nameof(categorie.Name),
                    $"The categorie {categorie.Name} already exist.");

                return View(categorie);
            }
            await repositorieCategories.Create(categorie);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerificaryCategorie(string Name)
        {
            var UserId = 1;
            var categorieExist = await repositorieCategories.Exist(Name, UserId);

            if (categorieExist)
            {
                return Json($"The categorie {Name} already exist");
            }

            return Json(true);
        }

        [HttpGet]
        public async Task<ActionResult> Modify(int id)
        {
            var userId = userServices.GetUserId();
            var categorie = await repositorieCategories.GetCategorieById(id, userId);

            if (categorie is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var model = mapper.Map<CategoriesViewModel>(categorie);
            model.OperationTypes = await GetOperationTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Modify(Categories categorie)
        {
            var userId = userServices.GetUserId();
            var categorieExists = await repositorieCategories.GetCategorieById(categorie.Id, userId);

            if (categorieExists is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieCategories.Modify(categorie);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = userServices.GetUserId();
            var categorie = await repositorieCategories.GetCategorieById(id, userId);

            if (categorie is null)
            {
                return RedirectToAction("NotFount", "Home");
            }

            return View(categorie);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var userId = userServices.GetUserId();
            var categorie = await repositorieCategories.GetCategorieById(id, userId);

            if (categorie is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var isUsed = await repositorieCategories.CategorieIsUsed(id);
            if (isUsed)
            {
                return RedirectToAction("Error", "Home");
            }

            await repositorieCategories.Delete(id);
            return RedirectToAction("Index");
        }
        //Lista de Tipos de operacion
        private async Task<IEnumerable<SelectListItem>> GetOperationTypes() 
        {
            var operationTypes = await repositorieOperationTypes.GetOperationTypes();
            return operationTypes.Select(x => new SelectListItem(x.Description, x.Id.ToString()));
        }
    }
}
