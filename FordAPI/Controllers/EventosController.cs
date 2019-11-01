using FordAPI.App_Data;
using FordAPI.Models;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace FordAPI.Controllers
{
    public class EventosController : ApiController
    {
        private FordEntities db = new FordEntities();

        // GET: api/Eventos
        public IHttpActionResult GetEventos()
        {
            JsonConvert.SerializeObject(db.Eventos);
            return Json(db.Eventos);
        }

        // GET: api/Eventos/5
        [ResponseType(typeof(Eventos))]
        public IHttpActionResult GetEventos(Guid id)
        {
            Eventos evento = db.Eventos.Find(id);
            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        // PUT: api/Eventos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEventos(Guid id, Eventos evento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != evento.Id)
            {
                return BadRequest();
            }

            db.Entry(evento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
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

        // POST: api/Eventos
        [ResponseType(typeof(Eventos))]
        public IHttpActionResult PostEventos(Eventos evento)
        {
            if (evento.Id == Guid.Empty)
            {
                evento.Id = Guid.NewGuid();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Eventos.Add(evento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = evento.Id }, evento);
        }

        // DELETE: api/Eventos/5
        [ResponseType(typeof(Eventos))]
        public IHttpActionResult DeleteEvento(Guid id)
        {
            Eventos evento = db.Eventos.Find(id);
            if (evento == null)
            {
                return NotFound();
            }

            db.Eventos.Remove(evento);
            db.SaveChanges();

            return Ok(evento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventoExists(Guid id)
        {
            return db.Eventos.Count(e => e.Id == id) > 0;
        }
    }
}