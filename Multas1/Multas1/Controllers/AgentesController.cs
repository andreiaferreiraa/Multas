using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Multas1.Models;

namespace Multas1.Controllers
{
    public class AgentesController : Controller
    {
        //cria um objeto privado, que representa a base de dados
        private MultasDb db = new MultasDb();
        private object fileUploadFotografia;

        // GET: Agentes
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //(LINQ)db.Agente.ToList() --> em SQL : SELECT * FROM Agentes
            //construi uma lista com os dados de todos os Agentes e envia-o para a view

            //obter a lista de todos os agentes
            //em SQL: SELECT * FROM Agentes ORDER BY Nome;
            //ToLista() é  equivalente ao SELECT *
;            var listaDeAgentes = db.Agentes.ToList().OrderBy(a=>a.Nome); //=> serve como separador

            return View(listaDeAgentes);
        }

        // GET: Agentes/Details/5
        /// <summary>
        /// Apresenta detalhes de um Agente
        /// </summary>
        /// <param name="id"> representa a PK  que identifca o Agente</param>
        /// <returns></returns>
        public ActionResult Details(int? id) //int? significa que pode haver valores nulos ( o parametro de entrada pode nao ser preenchido)
        {
            //protege a execução do método contra a Não existência de dados
            if (id == null)
            {
                //instrucao original
                //devolve um erro quando nao ha ID
                //logo nao é possivel pesquisar por um Agente
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                //redirecionar para uma pagina que nós controlamos
                return RedirectToAction("Index");

            }

            //vai procurar o Agente cujo ID foi fornecido
            Agentes agente = db.Agentes.Find(id);

            //se o agente nao for encontrado
            if (agente == null)
            {
                //o agente nao foi encontrado
                //logo gera se uma mensagem de erro
                //return HttpNotFound();

                //redirecionar para uma pagina que nós controlamos
                return RedirectToAction("Index");

            }

            //envia para a view os dados do Agente
            return View(agente);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Agentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agentes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        //protege o metodo contra o ataque de roubo de entidade
        [ValidateAntiForgeryToken]
        //assinatura de um metodo: é o nome do metodo, o valor devolvido e os parametros de entrada
        public ActionResult Create([Bind(Include = "Nome,Esquadra")] Agentes agente,
                                    HttpPostedFileBase fileUploadFotografia)
        {
            //determinar o ID do novo Agente
            //testar se ha registos na tabela dos Agentes
            //count devolve o numero de registos
            // if (db.Agentes.Count()!=0)  {}  //count devolve o numero de registos

            //ou entao, usar a instrucao TRY/CATCH
            var novoID = 0;
            try
            {
                novoID=db.Agentes.Max(a => a.ID) + 1;
            }
            catch (Exception)
            {
                novoID = 1;
            }

            //atribuir o ID ao novo agente
            agente.ID = novoID;

            //var auxiliar
            string nomeFotografia = "Agente_" + novoID + ".jpg";
            string caminhoParaFoto = Path.Combine(Server.MapPath("~/imagens/"), nomeFotografia);

            //verificar se chega efetivamente um ficheiro ao servidor
            if (fileUploadFotografia != null)
            {
                //guardar o nome da imagem na base de dados
                agente.Fotografia = nomeFotografia;

            }
            else
            {
                //nao há imagem...
                ModelState.AddModelError("", "Nao foi fornecida uma imagem..."); //gera msg de erro
                return View(agente);//reenvia os dados do 'Agente' para  a View
            }

            //verifica se o ficheiro é realmente uma imagem --> TPC
            //if(fileUploadFotografia == {}
            //redimensionar a imagem --> TPC
            //escrever a fotografia no disco rigido

            //escrever o nome da imagem na BD

            //ModelState --> controla os dados fornecidos com o modelo, se nao respeitar as regras 
            //do modelo, rejeita os dados
            if (ModelState.IsValid)
            {
                try
                {

                    //adiciona na estrutura de dados, na memória do servidor, o objeto Agentes
                    db.Agentes.Add(agente);
                    //faz "commit"
                    db.SaveChanges();
                    //guardar a imagem no disco rigido
                    fileUploadFotografia.SaveAs(caminhoParaFoto);

                    //redireciona o utilizadora para a página de inicio 
                    return RedirectToAction("Index");
                }

                catch (Exception)
                {
                    ModelState.AddModelError("", "Houve um erro na criacao de um novo agente");
                }
            }

            //se houver um erro
            //reapresenta os dados de agente na view
            return View(agente);
        }

        // GET: Agentes/Edit/5
        //esta funcao é gual à Details
        //ambas tem o mesmo onjetivo, pois pegam nos dados do gente e mostram na View
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                //instrucao original
                //devolve um erro quando nao ha ID
                //logo nao é possivel pesquisar por um Agente
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                //redirecionar para uma pagina que nós controlamos
                return RedirectToAction("Index");

            }

            //vai procurar o Agente cujo ID foi fornecido
            Agentes agente = db.Agentes.Find(id);

            //se o agente nao for encontrado
            if (agente == null)
            {
                //o agente nao foi encontrado
                //logo gera se uma mensagem de erro
                //return HttpNotFound();

                //redirecionar para uma pagina que nós controlamos
                return RedirectToAction("Index");

            }

            //envia para a view os dados do Agente
            return View(agente);
        }

        // POST: Agentes/Edit
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentes"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome, Fotografia, Esquadra")] Agentes agentes)
                               
        {
            
                if (ModelState.IsValid)
            {
                //atualiza os dados do Agente, na estrutura de dados em memória
                db.Entry(agentes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agentes);
        }

        // GET: Agentes/Delete/5 
        /// <summary>
        /// apresenta na view os dados de um Agente,
        /// com vista à sua, eventual, eliminacao
        /// </summary>
        /// <param name="id"> identificador do agente a apagar </param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            //verificar se foi fornecido um ID valido
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            // pesquisa pelo Agente, cujo ID foi fornecido 
            Agentes agente = db.Agentes.Find(id);

            //verifica se o Agente foi encontrado
            if (agente == null)
            {
                //o Agente nao existe
                //redireciona para a pagina inicial
                return RedirectToAction("Index");
            }
            //apresentar os dados na view
            return View(agente);
        }

        // POST: Agentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //procura o Agente
            Agentes agente = db.Agentes.Find(id);
            try {
                
                //remove da memória
                db.Agentes.Remove(agente);
                //commit na bd
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", String.Format("Nao é possvel apagar o Agente nº {0} - {1}, porque há multas associadas a ele", id, agente.Nome));
              
            }
            //se cheguei aqui é pq houve problema
            //devolvo os dados do Agente a view
            return View(agente);
        }
    

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
