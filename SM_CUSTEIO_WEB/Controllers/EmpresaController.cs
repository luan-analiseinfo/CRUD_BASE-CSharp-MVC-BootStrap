
using SM_CUSTEIO_WEB.Models;
using SM_CUSTEIO_WEB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SM_CUSTEIO.Controllers
{
    public class EmpresaController : Controller
    {

        private EmpresaRepository _EmpresaRepository;

        public EmpresaRepository EmpresaRepository
        {
            get
            {
                if (_EmpresaRepository == null)
                    _EmpresaRepository = new EmpresaRepository();

                return _EmpresaRepository;
            }
            set { _EmpresaRepository = value; }
        }


        // GET: /Empresa/
        public ActionResult Index()
        {
            ViewData["message"] = "";
            List<Empresa> lista = EmpresaRepository.GetAll();
            return View(lista);
        }
        // GET: /Empresa/
        public ActionResult List(Empresa entity, string message)
        {
            
            if (string.IsNullOrEmpty(entity.Nome))
                return View(EmpresaRepository.GetAll());
            else
                return View(EmpresaRepository.GetAllByName(entity.Nome));
        }

        //
        // GET: /Empresa/Details/5
        public ActionResult Details(int id)
        {
            return View(EmpresaRepository.GetOne(id));
        }
        public bool Validate(Empresa entity)
        {
            bool retorno = false;
         
            if (string.IsNullOrEmpty(entity.Nome))
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
        // GET: /Empresa/Create
        public ActionResult Create()
        {
            return View(new Empresa());
        }

        //
        // POST: /Empresa/Create
        [HttpPost]
        public ActionResult Create(Empresa entity)
        {
            try
            {
                // TODO: Add insert logic here
                if (Validate(entity))
                    return View(entity);

                EmpresaRepository.Save(entity);
                ViewBag.Message = "Dados salvos com sucesso.";
                return RedirectToAction("Index", new { message = "Dados salvos com sucesso." });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Empresa/Edit/5
        public ActionResult Edit(int id)
        {

            return View(EmpresaRepository.GetOne(id));
        }

        //
        // POST: /Empresa/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Empresa entity)
        {
            try
            {

                if (Validate(entity))
                    return View(entity);

                EmpresaRepository.Save(entity);
                ViewBag.Message = "Dados editados com sucesso.";
                return RedirectToAction("Index", new { message = "Dados editados com sucesso." });
            }
            catch
            {
                return View(entity);
            }
        }

        //
        // GET: /Empresa/Delete/5
        public ActionResult Delete(int id)
        {
            return View(EmpresaRepository.GetOne(id));
        }

        //
        // POST: /Empresa/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Empresa entity)
        {
            try
            {
                entity = EmpresaRepository.GetOne(id);
                EmpresaRepository.Delete(entity);
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
