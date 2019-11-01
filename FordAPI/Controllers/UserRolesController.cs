using FordAPI.App_Data;
using FordAPI.Models;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace FordAPI.Controllers
{
    public class UserRolesController : ApiController
    {
        private FordEntities db = new FordEntities();

        // GET: api/UserRoles
        public IHttpActionResult GetUserRoles()
        {
            JsonConvert.SerializeObject(db.UserRoles);
            return Json(db.UserRoles);
        }

        // GET: api/UserRoles/5
        [ResponseType(typeof(UserRoles))]
        public IHttpActionResult GetUserRoles(int id)
        {
            UserRoles userRoles = db.UserRoles.Find(id);
            if (userRoles == null)
            {
                return NotFound();
            }

            return Ok(userRoles);
        }

        // PUT: api/UserRoles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserRole(int id, UserRoles userRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userRoles.UserRolesId)
            {
                return BadRequest();
            }

            db.Entry(userRoles).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRolesExists(id))
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

        // POST: api/UserRoles
        [ResponseType(typeof(UserRoles))]
        public IHttpActionResult PostUserRole(UserRoles userRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.UserRoles.Add(userRoles);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userRoles.UserRolesId }, userRoles);
        }

        // DELETE: api/UserRoles/5
        [ResponseType(typeof(Funcionarios))]
        public IHttpActionResult DeleteUserRole(int id)
        {
            UserRoles userRoles = db.UserRoles.Find(id);
            if (userRoles == null)
            {
                return NotFound();
            }

            db.UserRoles.Remove(userRoles);
            db.SaveChanges();

            return Ok(userRoles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserRolesExists(int id)
        {
            return db.UserRoles.Count(e => e.UserRolesId == id) > 0;
        }
    }
}