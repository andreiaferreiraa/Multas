using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Multas1.Models
{
    public class MultasDb : DbContext{

        //contrutor da classe
        public MultasDb() :base("MultasDbConnectionString")
        {

        }

        //identificar as tabelas da base de dados
        //esta classe terá 4 atributos que sao todos do mesmo tipo de dados
        public virtual DbSet<Multas> Multas { get; set; }
        public virtual DbSet<Condutores> Condutores { get; set; }
        public virtual DbSet<Viaturas> Viaturas { get; set; }
        public virtual DbSet<Agentes> Agentes { get; set; }



    }
}