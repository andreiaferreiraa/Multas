using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Multas1.Models
{
    public class MultasDb : DbContext
    {
        //construtor da classe
        public MultasDb() : base("MultasDBConnectionString")
        {

        }

        //identificar as tabelas da base de dados
        public virtual DbSet<Multas> Multas { get; set; }
        public virtual DbSet<Condutores> Condutores { get; set; }
        public virtual DbSet<Agentes> Agentes { get; set; }
        public virtual DbSet<Viaturas> Viaturas { get; set; }
    }
}