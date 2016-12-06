
using SM_CUSTEIO_WEB.Models;
using SM_CUSTEIO_WEB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SM_CUSTEIO.Controllers
{
    public class MaterialController : Controller
    {

        private MaterialRepository _MaterialRepository;

        public MaterialRepository MaterialRepository
        {
            get
            {
                if (_MaterialRepository == null)
                    _MaterialRepository = new MaterialRepository();

                return _MaterialRepository;
            }
            set { _MaterialRepository = value; }
        }


        // GET: /Material/
        public ActionResult Index()
        {
            ViewData["message"] = "";
            List<Material> lista = MaterialRepository.GetAll();
            return View(lista);
        }
        // GET: /Material/
     

        //
        // GET: /Material/Details/5
        public ActionResult Details(int id)
        {
            return View(MaterialRepository.GetOne(id));
        }
        public bool Validate(Material entity)
        {
            bool retorno = false;
         
            if (string.IsNullOrEmpty(entity.Nome_Material))
            {
                ModelState.AddModelError("Nome", "Campo obrigatório");
                retorno = true;
            }
        
            return retorno;
        }
        //
        // GET: /Material/Create
        public ActionResult Create()
        {
            return View(new Material());
        }

        //
        // POST: /Material/Create
        [HttpPost]
        public ActionResult Create(Material entity)
        {
            try
            {
                // TODO: Add insert logic here
                if (Validate(entity))
                    return View(entity);

                MaterialRepository.Save(entity);
                ViewBag.Message = "Dados salvos com sucesso.";
                return RedirectToAction("Index", new { message = "Dados salvos com sucesso." });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Material/Edit/5
        public ActionResult Edit(int id)
        {

            return View(MaterialRepository.GetOne(id));
        }

        //
        // POST: /Material/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Material entity)
        {
            try
            {

                if (Validate(entity))
                    return View(entity);

                MaterialRepository.Save(entity);
                ViewBag.Message = "Dados editados com sucesso.";
                return RedirectToAction("Index", new { message = "Dados editados com sucesso." });
            }
            catch
            {
                return View(entity);
            }
        }

        //
        // GET: /Material/Delete/5
        public ActionResult Delete(int id)
        {
            return View(MaterialRepository.GetOne(id));
        }

        //
        // POST: /Material/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Material entity)
        {
            try
            {
                entity = MaterialRepository.GetOne(id);
                MaterialRepository.Delete(entity);
                ViewBag.Message = "Dados deletados com sucesso.";
                return RedirectToAction("Index", new { message = "Dados deletados com sucesso" });
            }
            catch
            {
                return View();
            }
        }

    }
}
