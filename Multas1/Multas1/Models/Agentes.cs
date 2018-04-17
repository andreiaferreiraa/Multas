using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        /*
         * instrucoes escritas em parentesis retos pq sao anotações
         * é uma anotacao que vai influenciar o comportamento deste atributo
         */
    
        [Key]
        //anotador que enibe a hipotese do id ser auto number
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required (ErrorMessage = "O {0} é de preenchimento obrigatório!")] //o atributo nome é de preenchimento obrigatorio
        //{0} faz referencia ao nome do atributo ( neste caso o nome)
        [RegularExpression ("[A-ZÂÍ][a-záéíóúãõàèìòâêîôäëïöüç.]+(( | de | da | dos | d' |-)[A-ZÂÍ][a-záéíóúãõàèìòâêîôäëïöüç]+){1,4}", 
            ErrorMessage = "O nome so aceita letras. Cada palavra começa por maiuscula e o restante maiuscula")] //expressoes regulares, ou seja
        [StringLength(40)] //especifica o tamanho do nome do agente
        public string Nome { get; set; }

        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório!")]
        public string  Fotografia { get; set; }

        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório!")]
        [RegularExpression("[A-Za-záéíóúãõàèìòâêîôäëïöüç. 0-9-]+", ErrorMessage ="Nao encontrado")]
        public string Esquadra { get; set; }

        /*
         * completar informacao sobre o relacionamento de um agente com as multaspor ele 'passadas'
         */

        public virtual ICollection<Multas> ListaMultas { get; set; }
    }
}