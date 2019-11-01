using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FordAPI.Models
{
    public class Eventos
    {
        public Guid Id { get; set; }
        public String title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public bool finished { get; set; }
        [JsonIgnore]
        public virtual ICollection<Veiculos> Veiculos { get; set; }

    }
}