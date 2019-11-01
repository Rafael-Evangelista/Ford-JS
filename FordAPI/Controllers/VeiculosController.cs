using FordAPI.App_Data;
using FordAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace FordAPI.Controllers
{
    //[Authorize]
    public class VeiculosController : ApiController
    {
        private FordEntities db = new FordEntities();

        // GET: api/Veiculos
        public IHttpActionResult GetVeiculos()
        {
            JsonConvert.SerializeObject(db.Veiculos);
            return Json(db.Veiculos);
        }

        // GET: api/Veiculos/5
        [ResponseType(typeof(Veiculos))]
        public IHttpActionResult GetVeiculos(Guid id)
        {
            Veiculos veiculo = db.Veiculos.Find(id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return Ok(veiculo);
        }

        [HttpGet]
        [Route("api/Veiculos/Top")]
        public IHttpActionResult GetVeiculosTop()
        {
            List<Veiculos> veiculos = db.Veiculos.ToList();
            var top2 = (from t in veiculos
                        select new { t.Marca, t.Modelo, t.Potencia, t.Carroceria, t.Combustivel, t.Valor, t.Imagem }).Take(2);

            return Json(top2);
        }

        // PUT: api/Veiculos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVeiculos(Guid id, Veiculos veiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != veiculo.Id)
            {
                return BadRequest();
            }

            db.Entry(veiculo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculoExists(id))
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

        // POST: api/Veiculos
        [ResponseType(typeof(Veiculos))]
        public IHttpActionResult PostVeiculos(Veiculos veiculo)
        {
            if (veiculo.Id == Guid.Empty)
            {
                veiculo.Id = Guid.NewGuid();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Veiculos.Add(veiculo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = veiculo.Id }, veiculo);
        }

        // DELETE: api/Veiculos/5
        [ResponseType(typeof(Veiculos))]
        public IHttpActionResult DeleteVeiculo(Guid id)
        {
            Veiculos veiculo = db.Veiculos.Find(id);
            if (veiculo == null)
            {
                return NotFound();
            }

            db.Veiculos.Remove(veiculo);
            db.SaveChanges();

            return Ok(veiculo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VeiculoExists(Guid id)
        {
            return db.Veiculos.Count(e => e.Id == id) > 0;
        }
    }
}