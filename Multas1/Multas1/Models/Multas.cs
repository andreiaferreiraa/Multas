using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Multas1.Models
{
    
    //camel case -> ao ha espacos em branco, e a primeira letra de cada palavra comeca com minuscula
    //e as seguintes por maiuscula 
    //variaveis/objeto/classe privadas comecam com minuscula//not obrigatorio
    //variaveis/objeto/classe publicas comecam por maiuscula//not obrigatorio

    public class Multas
    {
        [Key]
        public int ID { get; set; } 

        public string Infracao { get; set; }

        public string LocalMulta { get; set; }

        public decimal ValorMulta { get; set; }

        public DateTime DataMulta { get; set; }

        //*****************************************************************//
        //               construcao das chaves forasteiras                  //
        //*******************************************************************//
        //----> EM SQL: ForeignKey NomeAtributoQueÉFK references TABELA(pk DaTabela)
        //EM C#:

        //FK Agentes
        [ForeignKey("Agente")]
        public int AgenteFK { get; set; }
        public virtual Agentes Agente { get; set; }


        //FKCondutores
        [ForeignKey("Condutor")]
        public int CondutorFK { get; set; }
        public virtual Condutores Condutor { get; set; }

        //FKViasturas
        [ForeignKey("Viatura")]
        public int ViaturaFK { get; set; }
        public virtual Viaturas Viatura { get; set; }






    }
}