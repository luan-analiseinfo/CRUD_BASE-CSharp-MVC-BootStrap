using Microsoft.Reporting.WebForms;
using SM_CUSTEIO_WEB.Models;
using SM_CUSTEIO_WEB.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SM_CUSTEIO.Controllers
{
    
    public class ProdutoController : Controller
    {

        private ProdutoRepository _ProdutoRepository;

        public ProdutoRepository ProdutoRepository
        {
            get
            {
                if (_ProdutoRepository == null)
                    _ProdutoRepository = new ProdutoRepository();

                return _ProdutoRepository;
            }
            set { _ProdutoRepository = value; }
        }

        // GET: /Produto/
        public ActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View(ProdutoRepository.GetAll());
        }
        // GET: /Produto/
  
        //
        // GET: /Produto/Details/5
        public ActionResult Details(int id)
        {
            return View(ProdutoRepository.GetOne(id));
        }
        public bool ValidateModel(Produto entity)
        {
            bool retorno = false;

            if (string.IsNullOrEmpty(entity.Nome))
            {
                ModelState.AddModelError("Nome", "Campo obrigatório");
                retorno = true;
            }

            if (entity.Preco == null) {
                ModelState.AddModelError("Preco", "Campo obrigatório");
                retorno = true;
            }
            
            return retorno;
        }
        //
        // GET: /Produto/Create
        public ActionResult Create()
        {
            LoadForm();
            return View(new Produto());
        }

        public int ProdutoID;
      
        public ActionResult listMateriais(int Id)
        {
            ProdutoID = Id;
            ViewBag.ProdutoID = Id;
            ViewData["ProdutoID"] = Id;
            TempData["ProdutoID"] = Id;
            List<MateriaPrima> lista = new MateriaPrimaRepository().GetAll();
            List<MateriaPrima> materiais = new List<MateriaPrima>();

            foreach (var material in lista)
            {
                if (material.Produto_Id == Id){
                    materiais.Add(material);
                }
                
              }

            return View(materiais);
            
        }


        public ActionResult novoMaterial(int Id)
        {
            MateriaPrima novo = new MateriaPrima();
            novo.Produto_Id = Id;
            return View(novo);
        }
        
        //
        // POST: /Produto/Create
        [HttpPost]
        public ActionResult Create(Produto entity)
        {
            try
            {

                // TODO: Add insert logic here
                if (ValidateModel(entity))
                {
                    LoadForm();
                    return View(entity);
                }
                entity.Codigo = CodigoGerado();
                ProdutoRepository.Save(entity);
                return RedirectToAction("Index","Produto", new { message = "Dados salvos com sucesso." });
            }
            catch
            {
                return View();
            }
        }

        public int CodigoGerado()
        {
            Random random = new Random();

            return random.Next(1000, 9999);
        }
       
        public ActionResult createMaterial(MateriaPrima entity)
        {
            new MateriaPrimaRepository().Save(entity);

            return RedirectToAction("Index", new { message = "Dados salvos com sucesso." });
        }

        public ActionResult createMaterail(int Id)
        {
            MateriaPrima material = new MateriaPrima();
            material.Produto_Id = Id;

            return View(material);
        }

        //
        // GET: /Produto/Edit/5
        public ActionResult Edit(int id)
        {
            LoadForm();
            return View(ProdutoRepository.GetOne(id));
        }

        //
        // POST: /Produto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Produto entity)
        {
            try
            {

                if (ValidateModel(entity))
                {
                    LoadForm();
                    return View(entity);
                }
                
                ProdutoRepository.Save(entity);
                return RedirectToAction("Index", new { message = "Dados editados com sucesso." });
            }
            catch
            {
                return View(entity);
            }
        }

        //
        // GET: /Produto/Delete/5
        public ActionResult Delete(int id)
        {
            return View(ProdutoRepository.GetOne(id));
        }

        //
        // POST: /Produto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Produto entity)
        {
            try
            {
                entity = ProdutoRepository.GetOne(id);
                ProdutoRepository.Delete(entity);
                return RedirectToAction("Index", new { message = "Dados excluidos com sucesso" });
            }
            catch
            {
                return View();
            }
        }


        public void LoadForm()
        {
            ViewData["lstEmpresa"] = new EmpresaRepository().GetAll();
            ViewData["lstProduto"] = new ProdutoRepository().GetAll();
            ViewBag.lstMateriais = new MultiSelectList(new MateriaPrimaRepository().GetAll(), "Id", "Nome");
            
        }

        public ActionResult Report(String id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report"), "ProdutoReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<Produto> cm = new List<Produto>();
            ProdutoRepository dc = new ProdutoRepository();
           
                cm = dc.GetAll();
           
            ReportDataSource rd = new ReportDataSource("DataSetProduto", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

                "<DeviceInfo>" +
                "<OutputFormat>" + id + "</OutputFormat>" +
                "<PageWidth>8.5in</PageWidth>" +
                "<PageHeight>11in</PageHeight>" +
                "<MarginTop>0.5in</MarginTop>" +
                "<MarginLeft>1in</MarginLeft>" +
                "<MarginRight>1in</MarginRight>" +
                "<MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);

        }


        public ActionResult ReportProdutoView(String id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report"), "ProdutoMaterialViewReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            
            List<Material> listMateriais = new MaterialRepository().GetAll();
            List<Produto> listProdutos = new ProdutoRepository().GetAll();

            ReportDataSource rd = new ReportDataSource("DataSetProdutoView");

            lr.DataSources.Add(rd);

            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

                "<DeviceInfo>" +
                "<OutputFormat>" + id + "</OutputFormat>" +
                "<PageWidth>8.5in</PageWidth>" +
                "<PageHeight>11in</PageHeight>" +
                "<MarginTop>0.5in</MarginTop>" +
                "<MarginLeft>1in</MarginLeft>" +
                "<MarginRight>1in</MarginRight>" +
                "<MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);

        }



    }
}
