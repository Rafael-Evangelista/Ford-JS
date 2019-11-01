using Newtonsoft.Json;
using System.Collections.Generic;

namespace FordAPI.Models
{
    public class Roles
    {
        public int RoleId { get; set; }
        public string Nome { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}