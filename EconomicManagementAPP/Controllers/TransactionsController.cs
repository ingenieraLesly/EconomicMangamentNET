using EconomicManagementAPP.Models;
using EconomicManagementAPP.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;

namespace EconomicManagementAPP.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly IRepositorieTransactions repositorieTransactions;
        private readonly IRepositorieOperationTypes repositorieOperationTypes;
        private readonly IRepositorieCategories repositorieCategories;
        private readonly IUserServices userServices;
        private readonly IMapper mapper;

        public TransactionsController(IRepositorieTransactions repositorieTransactions,
                                      IRepositorieOperationTypes repositorieOperationTypes,
                                      IRepositorieCategories repositorieCategories,
                                      IUserServices userServices,
                                      IMapper mapper)
        {
            this.repositorieTransactions = repositorieTransactions;
            this.repositorieOperationTypes = repositorieOperationTypes;
            this.repositorieCategories = repositorieCategories;
            this.userServices = userServices;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var userId = userServices.GetUserId();
            var transaction = await repositorieTransactions.GetTransactions(userId);
            return View(transaction);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var userId = userServices.GetUserId();
            var model = new TransactionsViewModel();
            model.OperationTypes = await GetOperationTypes();
            model.Categories = await GetCategories(userId, 1);
            model.AccountId = id;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transactions transaction)
        {
            transaction.UserId = userServices.GetUserId();
            if (!ModelState.IsValid)
            {
                return View(transaction);
            }

            var operationType = await repositorieOperationTypes.GetOperationTypeById(transaction.OperationTypeId);
            if (operationType.Description == "Egreso" || operationType.Description == "Gasto")
            {
                transaction.Total *= -1;
            }

            await repositorieTransactions.Create(transaction);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> Modify(int id)
        {
            var userId = userServices.GetUserId();

            var transaction = await repositorieTransactions.GetTransactionById(id, userId);

            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var model = mapper.Map<TransactionsViewModel>(transaction);
            model.OperationTypes = await GetOperationTypes();
            model.Categories = await GetCategories(userId, 1);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Modify(Transactions transaction)
        {
            var userId = userServices.GetUserId();
            var transactionExists = await repositorieTransactions.GetTransactionById(transaction.Id, userId);

            if (transactionExists is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            var operationType = await repositorieOperationTypes.GetOperationTypeById(transaction.OperationTypeId);
            if (operationType.Description == "Egreso" || operationType.Description == "Gasto")
            {
                transaction.Total *= -1;
            }

            await repositorieTransactions.Modify(transaction);
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = userServices.GetUserId();
            var transaction = await repositorieTransactions.GetTransactionById(id, userId);

            if (transaction is null)
            {
                return RedirectToAction("NotFount", "Home");
            }

            return View(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var userId = userServices.GetUserId();
            var transaction = await repositorieTransactions.GetTransactionById(id, userId);

            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieTransactions.Delete(id);
            return RedirectToAction("Index","Home");
        }

        private async Task<IEnumerable<SelectListItem>> GetCategories(int userId, int operationTypes)
        {
            var categories = await repositorieCategories.GetCategories(userId, operationTypes);
            return categories.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
        }

        private async Task<IEnumerable<SelectListItem>> GetOperationTypes() 
        {
            var OperationTypes = await repositorieOperationTypes.GetOperationTypes();
            return OperationTypes.Select(x => new SelectListItem(x.Description, x.Id.ToString()));
        }

        [HttpPost]
        public async Task<IActionResult> GetCategories([FromBody] int operationTypeId)
        {
            var userId = userServices.GetUserId();
            var categories = await GetCategories(userId, operationTypeId);
            return Ok(categories);
        }
    }
}
