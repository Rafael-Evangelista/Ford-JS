using FordAPI.App_Data;
using FordAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FordAPI.Utils
{
    public class SortearNumeros
    {
        private static FordEntities db = new FordEntities();

        static List<int> random_caches = new List<int>();

        public static int GerarNumeroDaSorte()
        {
            Random a = new Random();

            // obtemos nosso número aleatório
            int c = a.Next(10000000, 99999999);
            // verifica se o número está em cache
            while (random_caches.Contains(c)) c = a.Next(10000000, 99999999);
            // adiciona o número ao cache
            random_caches.Add(c);

            var sorteio = db.Sorteios.ToList();

            foreach (var item in sorteio)
            {
                if (item.NumeroDaSorte == c)
                {
                    c = 0;
                    return GerarNumeroDaSorte();
                }
            }
            return c;
        }

        public static string SortearNumero(Guid idEvento)
        {
            var veiculos = db.Veiculos.ToList();
            var sorteios = db.Sorteios.ToList();

            List<Veiculos> VeiculosDoSorteio = new List<Veiculos>();

            foreach (var item in veiculos)
            {
                if (item.IdEvento == idEvento)
                {
                    VeiculosDoSorteio.Add(item);
                }
            }
            try
            {
                List<Sorteios> ParticipantesDoSorteio = new List<Sorteios>();

                foreach (var item in sorteios)
                {
                    foreach (var idVeiculo in VeiculosDoSorteio)
                    {
                        if (item.VeiculoId == idVeiculo.Id)
                        {
                            ParticipantesDoSorteio.Add(item);
                        }
                    }
                }

                for (int i = 0; i < VeiculosDoSorteio.Count(); i++)
                {
                    var lote = 1 + i;

                    if (ParticipantesDoSorteio != null)
                    {
                        var rnd = new Random();

                        //Embaralhando participantes
                        var query =
                        from s in ParticipantesDoSorteio
                        let r = rnd.Next()
                        orderby r
                        select s;

                        //Lista participantes embaralhados
                        var embaralhado = query.ToList();

                        //Listando o sorteado por lote
                        var sorteado = embaralhado.Where(x => x.Veiculos.Lote == lote).First();
                        sorteado.Sorteado = true;
                        db.SaveChanges();
                        //Enviando E-mail ao vencedor
                        Utils.EnviarEmail.EnviarEmailAoSorteado(sorteado);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            Utils.FinalizarSorteios.FinalizarSorteio();

            var finalizarEvento = db.Eventos.Find(idEvento);
            finalizarEvento.finished = true;
            db.SaveChanges();

            return "Veiculos Sorteados";
        }

    }
}