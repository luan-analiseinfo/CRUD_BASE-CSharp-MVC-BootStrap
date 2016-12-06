
using SM_CUSTEIO.Models;
using SM_CUSTEIO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SM_CUSTEIO.Controllers
{
    public class AssProdutoMaterialController : Controller
    {

        private AssProdutoMaterialRepository _AssProdutoMaterialRepository;

        public AssProdutoMaterialRepository AssProdutoMaterialRepository
        {
            get
            {
                if (_AssProdutoMaterialRepository == null)
                    _AssProdutoMaterialRepository = new AssProdutoMaterialRepository();

                return _AssProdutoMaterialRepository;
            }
            set { _AssProdutoMaterialRepository = value; }
        }


        // GET: /AssProdutoMaterial/
        public ActionResult Index()
        {
            ViewData["message"] = "";
            return View(AssProdutoMaterialRepository.GetAll());
        }
        // GET: /AssProdutoMaterial/
        public ActionResult List(AssProdutoMaterial entity, string message)
        {
            ViewData["message"] = message;
            if (string.IsNullOrEmpty(entity.Receita.Nome))
                return View(AssProdutoMaterialRepository.GetAll());
            else
                return View(AssProdutoMaterialRepository.GetAllByName(entity.Receita.Nome));
        }

        //
        // GET: /AssProdutoMaterial/Details/5
        public ActionResult Details(int id)
        {
            return View(AssProdutoMaterialRepository.GetOne(id));
        }
        public bool Validate(AssProdutoMaterial entity)
        {
            bool retorno = false;
         
            if (string.IsNullOrEmpty(entity.Receita.Nome))
            {
                ModelState.AddModelError("Nome", "Campo obrigatório");
                ModelState.AddModelError("Endereco", "Campo obrigatório");
                ModelState.AddModelError("Telefone", "Campo obrigatório");
                ModelState.AddModelError("Website", "Campo obrigatório");
                retorno = true;
            }
        
            return retorno;
        }
        //
        // GET: /AssProdutoMaterial/Create
        public ActionResult Create()
        {
            return View(new AssProdutoMaterial());
        }

        //
        // POST: /AssProdutoMaterial/Create
        [HttpPost]
        public ActionResult Create(AssProdutoMaterial entity)
        {
            try
            {
                // TODO: Add insert logic here
                if (Validate(entity))
                    return View(entity);

                AssProdutoMaterialRepository.Save(entity);
                return RedirectToAction("Index", new { message = "Dados salvos com sucesso." });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /AssProdutoMaterial/Edit/5
        public ActionResult Edit(int id)
        {

            return View(AssProdutoMaterialRepository.GetOne(id));
        }

        //
        // POST: /AssProdutoMaterial/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AssProdutoMaterial entity)
        {
            try
            {

                if (Validate(entity))
                    return View(entity);

                AssProdutoMaterialRepository.Save(entity);
                return RedirectToAction("Index", new { message = "Dados salvos com sucesso." });
            }
            catch
            {
                return View(entity);
            }
        }

        //
        // GET: /AssProdutoMaterial/Delete/5
        public ActionResult Delete(int id)
        {
            return View(AssProdutoMaterialRepository.GetOne(id));
        }

        //
        // POST: /AssProdutoMaterial/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, AssProdutoMaterial entity)
        {
            try
            {
                entity = AssProdutoMaterialRepository.GetOne(id);
                AssProdutoMaterialRepository.Delete(entity);

                return RedirectToAction("Index", new { message = "Dados excluidos com sucesso" });
            }
            catch
            {
                return View();
            }
        }

    }
}
