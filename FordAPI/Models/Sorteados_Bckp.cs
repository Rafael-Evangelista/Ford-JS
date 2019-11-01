using Newtonsoft.Json;
using System;

namespace FordAPI.Models
{
    public class Sorteados_Bckp
    {
        public int SorteioId { get; set; }
        public int FuncionarioId { get; set; }
        public Guid VeiculoId { get; set; }
        public bool Sorteado { get; set; }
        public int NumeroDaSorte { get; set; }
        public string Item { get; set; }
        public string UserName { get; set; }
        public DateTime Data { get; set; }

        public virtual Funcionarios Funcionarios { get; set; }

    }
}