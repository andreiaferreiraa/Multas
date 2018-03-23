﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas1.Models
{
    public class Agentes
    {
    public Agentes()
        {
            ListaMultas = new HashSet<Multas>();
        }
        [Key]

        public int ID { get; set; }

        public string Nome { get; set; }

        public string  Fotografia { get; set; }

        public string Esquadra { get; set; }

        /*
         * completar informacao sobre o relacionamento de um agente com as multas
         * por ele 'passadas'
         */

        public virtual ICollection<Multas> ListaMultas { get; set; }
    }
}