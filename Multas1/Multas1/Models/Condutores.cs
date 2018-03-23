using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas1.Models
{
    public class Condutores
    {
        public Condutores()
        {
            ListaMultas = new HashSet<Multas>();
        }
        [Key]

        //ao dizer que e é ID o ASP.NET sabe que é chave primária
        public int ID { get; set; } //Chave primaria

        //dados próprios do condutor
        public string Nome { get; set; }

        public string BI { get; set; }

        public string Telemovel { get; set; }

        public DateTime DataNascimento { get; set; }

        //dados da carta de condução do condutor
        public string NumCartaConducao { get; set; }

        public string LocalEmissao { get; set; }

        public DateTime DataValidadeCarta { get; set; }

        public virtual ICollection<Multas> ListaMultas { get; set; }

    }
}