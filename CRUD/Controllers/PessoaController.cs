using CRUD.Models;
using CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CRUD.Controllers
{
    public class PessoaController : Controller
    {

        private PessoaRepository _PessoaRepository;

        public PessoaRepository PessoaRepository
        {
            get
            {
                if (_PessoaRepository == null)
                    _PessoaRepository = new PessoaRepository();

                return _PessoaRepository;
            }
            set { _PessoaRepository = value; }
        }


        // GET: /Pessoa/
        public ActionResult Index()
        {
            ViewData["message"] = "";
            return View(new Pessoa());
        }
        // GET: /Pessoa/
        public ActionResult List(Pessoa entity, string message)
        {
            ViewData["message"] = message;
            if (string.IsNullOrEmpty(entity.Nome))
                return View(PessoaRepository.GetAll());
            else
                return View(PessoaRepository.GetAllByName(entity.Nome));
        }

        //
        // GET: /Pessoa/Details/5
        public ActionResult Details(int id)
        {
            return View(PessoaRepository.GetOne(id));
        }
        public bool Validate(Pessoa entity)
        {
            bool retorno = false;
            if (string.IsNullOrEmpty(entity.Nome))
            {
                ModelState.AddModelError("Nome", "Campo obrigatório");
                retorno = true;
            }
            return retorno;
        }
        //
        // GET: /Pessoa/Create
        public ActionResult Create()
        {
            return View(new Pessoa());
        }

        //
        // POST: /Pessoa/Create
        [HttpPost]
        public ActionResult Create(Pessoa entity)
        {
            try
            {
                // TODO: Add insert logic here
                if (Validate(entity))
                    return View(entity);

                PessoaRepository.Save(entity);
                return RedirectToAction("Index", new { message = "Dados salvos com sucesso." });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Pessoa/Edit/5
        public ActionResult Edit(int id)
        {

            return View(PessoaRepository.GetOne(id));
        }

        //
        // POST: /Pessoa/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Pessoa entity)
        {
            try
            {

                if (Validate(entity))
                    return View(entity);

                PessoaRepository.Save(entity);
                return RedirectToAction("Index", new { message = "Dados salvos com sucesso." });
            }
            catch
            {
                return View(entity);
            }
        }

        //
        // GET: /Pessoa/Delete/5
        public ActionResult Delete(int id)
        {
            return View(PessoaRepository.GetOne(id));
        }

        //
        // POST: /Pessoa/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Pessoa entity)
        {
            try
            {
                entity = PessoaRepository.GetOne(id);
                PessoaRepository.Delete(entity);

                return RedirectToAction("Index", new { message = "Dados excluidos com sucesso" });
            }
            catch
            {
                return View();
            }
        }

    }
}
