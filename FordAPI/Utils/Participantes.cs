using FordAPI.App_Data;
using FordAPI.Models;
using System;

namespace FordAPI.Utils
{

    public class Participantes
    {
        private static FordEntities db = new FordEntities();
        public static void ParticipanteDoSorteio(Guid VeiculoId)
        {
            Veiculos veiculo = db.Veiculos.Find(VeiculoId);
            veiculo.Participantes = veiculo.Participantes + 1;
            db.SaveChanges();
        }

    }
}