using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace MyWebApplication1.Models.Db
{
    [Table("Requests")]
    public class Request
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
    }
}
