using FordAPI.App_Data;
using FordAPI.Models;
using System;

namespace FordAPI.Utils
{
    public class ObterItens
    {
        private static FordEntities db = new FordEntities();

        public static string itemDoSorteio(Guid VeiculoId)
        {
            Veiculos veiculo = db.Veiculos.Find(VeiculoId);

            if (veiculo == null)
            {
                return ("Item não encontrado");
            }

            return veiculo.Lote.ToString();
        }
    }
}