using FordAPI.App_Data;
using FordAPI.Models;
using FordAPI.Utils;
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
    public class SorteiosController : ApiController
    {
        private FordEntities db = new FordEntities();

        // GET: api/Sorteios
        public IHttpActionResult GetSorteio()
        {
            JsonConvert.SerializeObject(db.Sorteios);
            return Json(db.Sorteios);
        }
        // GET: api/Sorteios/5
        [ResponseType(typeof(Sorteios))]
        public IHttpActionResult GetSorteios(Guid id)
        {
            Sorteios sorteio = db.Sorteios.Find(id);
            if (sorteio == null)
            {
                return NotFound();
            }

            return Ok(sorteio);
        }


        [HttpGet]
        [Route("api/Sorteios/MeusLotes")]
        public IHttpActionResult GetSorteioParticipanteTop(string userName)
        {
            List<Sorteios> sorteios = db.Sorteios.ToList();
            var top2 = (from t in sorteios
                        where t.UserName == userName
                        select new { t.Veiculos.Marca, t.Veiculos.Modelo, t.Veiculos.Potencia, t.Veiculos.Carroceria, t.Veiculos.Combustivel, t.Veiculos.Valor, t.Veiculos.Imagem, t.Veiculos.Lote }).Take(2);

            return Json(top2);
        }

        [HttpGet]
        [Route("api/Sorteios/participante")]
        [ResponseType(typeof(Sorteios))]
        public bool GetSorteiosParticipante(Guid VeiculoId, string userName)
        {
            bool participante = false;

            var sorteio = db.Sorteios.ToList();

            var participandoDoSorteio = sorteio.Where(x => x.UserName == userName && x.VeiculoId == VeiculoId).Count();

            if (participandoDoSorteio != 0)
            {
                participante = true;
            }

            return participante;
        }

        [HttpGet]
        [Route("api/Sorteios/UltimoSorteio")]
        public IHttpActionResult GetSorteioUltimoSorteio()
        {
            dynamic ultimoSorteio = "";

            List<VeiculosSorteados_Bckp> VeiculosBckp = db.VeiculosSorteados_Bckp.ToList();
            List<Sorteados_Bckp> ultimosSorteados = db.Sorteados_Bckp.ToList();
            if (ultimosSorteados.Count != 0)
            {
                ultimoSorteio = (from t in ultimosSorteados
                                 join v in VeiculosBckp on t.VeiculoId equals v.VeiculoId
                                 select new { Sorteado = t, Veiculo = v }).ToList();
            }

            return Json(ultimoSorteio);
        }


        [HttpPost]
        [Route("api/Sorteios/Sortear")]
        [ResponseType(typeof(Sorteios))]
        public List<Sorteios> PostSortearVeiculos(DateTime DataInicial)
        {
            Guid Id = Guid.Empty;
            var eventos = db.Eventos.ToList();
            var sorteios = db.Sorteios.ToList();

            foreach (var item in eventos)
            {
                if (item.start == DataInicial)
                {
                    Id = item.Id;
                }
            }

            var sorteado = SortearNumeros.SortearNumero(Id);

            return sorteios;
        }


        // PUT: api/Sorteios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSorteio(Guid id, Sorteios sorteio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sorteio.SorteioId)
            {
                return BadRequest();
            }

            db.Entry(sorteio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SorteioExists(id))
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

        // POST: api/Sorteios
        [ResponseType(typeof(Veiculos))]
        public IHttpActionResult PostSorteio(Sorteios sorteio)
        {
            if (sorteio.SorteioId == Guid.Empty)
            {
                sorteio.FuncionarioId = Convert.ToInt32(FiltrarUsuario.FiltrandoUsuarioPorId(sorteio.UserName));
                sorteio.SorteioId = Guid.NewGuid();
                sorteio.NumeroDaSorte = SortearNumeros.GerarNumeroDaSorte();
                sorteio.Item = ObterItens.itemDoSorteio(sorteio.VeiculoId);
                //SortearNumeros.SortearNumero();

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sorteios.Add(sorteio);
            db.SaveChanges();

            //Adicionando Participante
            Participantes.ParticipanteDoSorteio(sorteio.VeiculoId);

            return CreatedAtRoute("DefaultApi", new { id = sorteio.SorteioId }, sorteio);
        }

        // DELETE: api/Sorteios/5
        [ResponseType(typeof(Veiculos))]
        public IHttpActionResult DeleteSorteio(Guid id, string username)
        {
            var sorteios = db.Sorteios.ToList();

            Sorteios sorteio = (from c in sorteios
                                where c.VeiculoId == id && c.UserName == username
                                select c).FirstOrDefault();

            if (sorteio == null)
            {
                return NotFound();
            }

            db.Sorteios.Remove(sorteio);
            db.SaveChanges();

            return Ok(sorteio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SorteioExists(Guid id)
        {
            return db.Sorteios.Count(e => e.SorteioId == id) > 0;
        }
    }
}