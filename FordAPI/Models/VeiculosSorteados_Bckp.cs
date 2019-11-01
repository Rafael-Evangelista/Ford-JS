using System;

namespace FordAPI.Models
{
    public class VeiculosSorteados_Bckp
    {
        public int Id { get; set; }
        public int Lote { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int Ano { get; set; }
        public decimal Valor { get; set; }
        public int Participantes { get; set; }
        public string Descricao { get; set; }
        public decimal Quilometragem { get; set; }
        public string Cambio { get; set; }
        public string Carroceria { get; set; }
        public string Combustivel { get; set; }
        public decimal ValorDoIPVA { get; set; }
        public int FinalDaPlaca { get; set; }
        public string Opcionais { get; set; }
        public string Potencia { get; set; }
        public int Portas { get; set; }
        public string Imagem { get; set; }
        public string InclusoNaCompra { get; set; }
        public Guid IdEvento { get; set; }
        public string Placa { get; set; }
        public string Cor { get; set; }
        public bool Garantia { get; set; }
        public Guid VeiculoId { get; set; }
        public DateTime DataDoSorteio { get; set; }
    }
}