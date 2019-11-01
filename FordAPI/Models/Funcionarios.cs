using Newtonsoft.Json;
using System.Collections.Generic;

namespace FordAPI.Models
{
    public partial class Funcionarios
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }

        [JsonIgnore]
        public virtual ICollection<Sorteios> Sorteios { get; set; }

        [JsonIgnore]
        public virtual ICollection<Sorteados_Bckp> Sorteados_Bckp { get; set; }
    }
}