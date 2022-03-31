using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class AccountTypesController : Controller
    {
        private readonly IRepositorieAccountTypes repositorieAccountTypes; //Interfaz para qe cargue solo los nombres y no todo el método. Luego de importar hagamos un constructor para esta class

        public AccountTypesController(IRepositorieAccountTypes repositorieAccountTypes) //para que construya esto de primeras porque los services (como en anglar) ya deben estar funcionando
        {
            this.repositorieAccountTypes = repositorieAccountTypes;//repositorie igual a la lista de los métodos que me trae la interfaz
        }
        public IActionResult Create() //una función que va a crear algo. Esto va a retornar una vista. Va a venir aquí y va a ejecutar una vista. IA de visual2022
        {//un formularrio para crear (por eso el Create) para lo que necesitará una vista (view)
            return View();//vista para que las personas puedan crear un tipo de cuenta
        }

        [HttpPost] //indicamos que vamos a hacer un POST con HTTP
        public IActionResult Create(AccountTypes accountTypes)//el modelo llega acá y sobre él serán lass validaciones. 
        {
            if (!ModelState.IsValid) //esta validación de modelo sabe que debe validar el modelo que se importo en la fxn CREATE(es cierto q esto NO es válid?
            { 
                return View(accountTypes); //TRUE ->esto se envía a la vista para el manejo de errores (Princeton text) 
            }
            accountTypes.UserId = 2; //quemar UserIde del modelo accountTypes porque aún no lo tengo en el formulario. id de un número real de un user creado en Mysql
            accountTypes.OrderAccount = 1;
            repositorieAccountTypes.Create(accountTypes);

            return View();//sino, retorna la vista nomal, lo que tenga que mostrar. Tenemos q ativar el repositorie activando conectionString, el constructor(de configuración->URL q esta DefaultConnection(server y cómo conectarse) y por último ejecuta el create (que usa el plugin de sql para hacer la query que inserta un tipo de cuenta y en la que además guarde el id en accountTypes
        }
    }
}
