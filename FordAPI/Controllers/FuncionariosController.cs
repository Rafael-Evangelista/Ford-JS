using FordAPI.App_Data;
using FordAPI.Models;
using FordAPI.Utils;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace FordAPI.Controllers
{
    public class FuncionariosController : ApiController
    {
        private FordEntities db = new FordEntities();

        // GET: api/Funcionarios
        public IHttpActionResult GetFuncionarios()
        {
            JsonConvert.SerializeObject(db.Funcionarios);
            return Json(db.Funcionarios);
        }

        // GET: api/Funcionarios/5
        [ResponseType(typeof(Funcionarios))]
        public IHttpActionResult GetFuncionarios(int id)
        {
            Funcionarios funcionario = db.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        // PUT: api/Funcionarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFuncionario(int id, Funcionarios funcionario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != funcionario.Id)
            {
                return BadRequest();
            }

            db.Entry(funcionario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Funcionarios
        [ResponseType(typeof(Funcionarios))]
        public IHttpActionResult PostFuncionario(Funcionarios funcionario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Funcionarios.Add(funcionario);
            if (funcionario.CPF != null && funcionario.NomeCompleto != null && funcionario.Email != null
                && funcionario.CEP != null && funcionario.Endereco != null && funcionario.Estado != null
                && funcionario.Login != null && funcionario.Senha != null)
            {
                //Criptografar senha
                funcionario.Senha = GeradorDeDeHash.Criptografar(funcionario.Senha);
                db.SaveChanges();
            }

            //Id do funcionario para adicionar as permissões
            var Idfuncionario = funcionario.Id;

            //Adicionando as permissões do usuario
            var userRoles = new UserRoles();
            userRoles.FuncionarioId = Idfuncionario;
            userRoles.RoleId = 2;
            db.UserRoles.Add(userRoles);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { Idfuncionario }, funcionario);
        }

        // DELETE: api/Funcionarios/5
        [ResponseType(typeof(Funcionarios))]
        public IHttpActionResult DeleteFuncionario(int id)
        {
            Funcionarios funcionario = db.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            db.Funcionarios.Remove(funcionario);
            db.SaveChanges();

            return Ok(funcionario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FuncionarioExists(int id)
        {
            return db.Funcionarios.Count(e => e.Id == id) > 0;
        }
    }
}