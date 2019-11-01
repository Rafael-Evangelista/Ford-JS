using System;

namespace FordAPI.Models
{
    public class Sorteios
    {
        public Guid SorteioId { get; set; }
        public int FuncionarioId { get; set; }
        public Guid VeiculoId { get; set; }
        public bool Sorteado { get; set; }
        public int NumeroDaSorte { get; set; }
        public string Item { get; set; }
        public string UserName { get; set; }

        public virtual Funcionarios Funcionarios { get; set; }
        public virtual Veiculos Veiculos { get; set; }
    }
}