using FordAPI.App_Data;
using FordAPI.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace FordAPI.Controllers
{
    public class MeusDadosController : ApiController
    {
        private FordEntities db = new FordEntities();

        [HttpGet]
        // GET: api/MeusDados/login
        [ResponseType(typeof(Funcionarios))]
        public IHttpActionResult ListarPorLogin(string login)
        {
            int Id = 0;
            var funcionarios = db.Funcionarios.ToList();

            foreach (var item in funcionarios)
            {
                if (item.Login == login)
                {
                    Id = item.Id;
                }
            }
            Funcionarios funcionario = db.Funcionarios.Find(Id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }
    }
}