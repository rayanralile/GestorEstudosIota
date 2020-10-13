using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Models
{
    public class NotificationModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TopicoId { get; set; }
        public string Titulo { get; set; }
        public DateTime DateTimeNotify { get; set; }
    }
}
