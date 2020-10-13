using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Models
{
    public class TopicoModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Titulo { get; set; }
        [MaxLength(2000)]
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}
