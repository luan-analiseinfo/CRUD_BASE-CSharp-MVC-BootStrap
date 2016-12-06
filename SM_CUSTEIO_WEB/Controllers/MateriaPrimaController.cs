using SM_CUSTEIO_WEB.Models;
using SM_CUSTEIO_WEB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SM_CUSTEIO_WEB.Controllers
{
    public class MateriaPrimaController : Controller
    {

        private MateriaPrimaRepository _MateriaPrimaRepository;

        public MateriaPrimaRepository MateriaPrimaRepository
        {
            get
            {
                if (_MateriaPrimaRepository == null)
                    _MateriaPrimaRepository = new MateriaPrimaRepository();

                return _MateriaPrimaRepository;
            }
            set { _MateriaPrimaRepository = value; }
        }


        public void loadForm()
        {
            ViewData["lstProdutos"] = new ProdutoRepository().GetAll();
        }

        // GET: /MateriaPrima/
        public ActionResult Index(int Id)
        {
            ViewBag.Message = "";
            ViewBag.ProdutoID = Id;
            ViewData["ProdutoID"] = Id;
            TempData["ProdutoID"] = Id;


            List<MateriaPrima> lista = new MateriaPrimaRepository().GetAll();
            List<MateriaPrima> materiais = new List<MateriaPrima>();

            foreach (var material in lista)
            {
                if (material.Produto_Id == Id)
                {
                    materiais.Add(material);
                }

            }

            return View(materiais);
        }
        // GET: /MateriaPrima/
     
        //
        // GET: /MateriaPrima/Details/5
        public ActionResult Details(int id)
        {
            return View(MateriaPrimaRepository.GetOne(id));
        }
        public bool Validate(MateriaPrima entity)
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
        // GET: /MateriaPrima/Create
        public ActionResult Create(int Id)
        {
            loadForm();
            MateriaPrima material = new MateriaPrima();
            material.Produto_Id = Id;
            return View(material);
        }

        //
        // POST: /MateriaPrima/Create
        [HttpPost]
        public ActionResult Create(MateriaPrima entity)
        {
            entity.Id = 0;

            try
            {
                // TODO: Add insert logic here
                if (Validate(entity))
                    return View(entity);

                MateriaPrimaRepository.Save(entity);
                ViewBag.Message = "Dados salvos com sucesso.";
                return RedirectToAction("Index", "Produto", new { Message = "Dados salvos com sucesso." });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MateriaPrima/Edit/5
        public ActionResult Edit(int id)
        {

            return View(MateriaPrimaRepository.GetOne(id));
        }

        //
        // POST: /MateriaPrima/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MateriaPrima entity)
        {
            try
            {

                if (Validate(entity))
                    return View(entity);

                MateriaPrimaRepository.Save(entity);
                ViewBag.Message = "Dados editados com sucesso.";
                return RedirectToAction("Index", "Produto", new { Message = "Dados salvos com sucesso." });
            }
            catch
            {
                return View(entity);
            }
        }

        //
        // GET: /MateriaPrima/Delete/5
        public ActionResult Delete(int id)
        {
            return View(MateriaPrimaRepository.GetOne(id));
        }

        //
        // POST: /MateriaPrima/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, MateriaPrima entity)
        {
            try
            {
                entity = MateriaPrimaRepository.GetOne(id);

                MateriaPrimaRepository.Delete(entity);
                ViewBag.Message = "Dados deletados com sucesso.";
                return RedirectToAction("Index", "Produto", new { Message = "Dados excluidos com sucesso" });
            }
            catch
            {
                return View();
            }
        }

    }
}
