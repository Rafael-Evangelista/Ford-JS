using System;

namespace FordAPI.Models
{
    public partial class FilesUpload
    {
        public int Id { get; set; }
        public byte[] Arquivo { get; set; }
        public DateTime Data { get; set; }
    }
}