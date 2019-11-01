using Newtonsoft.Json;

namespace FordAPI.Models
{
    public class UserRoles
    {
        public int UserRolesId { get; set; }
        public int RoleId { get; set; }
        public int FuncionarioId { get; set; }
        [JsonIgnore]
        public virtual Roles Roles { get; set; }
        [JsonIgnore]
        public virtual Funcionarios Funcionarios { get; set; }
    }
}