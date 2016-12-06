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
    
    public class ProdutoMaterialController : Controller
    {

        private ProdutoMaterialRepository _ProdutoMaterialRepository;

        public ProdutoMaterialRepository ProdutoMaterialRepository
        {
            get
            {
                if (_ProdutoMaterialRepository == null)
                    _ProdutoMaterialRepository = new ProdutoMaterialRepository();

                return _ProdutoMaterialRepository;
            }
            set { _ProdutoMaterialRepository = value; }
        }

        public ActionResult Index(int Id)
        {
            ViewBag.Message = "";
            ViewBag.ProdutoID = Id;
            ViewData["ProdutoID"] = Id;
            TempData["ProdutoID"] = Id;


            List<ProdutoMaterial> lista = new ProdutoMaterialRepository().GetAll();
            List<ProdutoMaterial> materiais = new List<ProdutoMaterial>();

            

            foreach (var material in lista)
            {
                if (material.Produto_Id == Id)
                {
                    materiais.Add(material);
                }


            }

            return View(materiais);
        }


        public ActionResult Relatorio(int Id)
        {
            ViewBag.Message = "";
            ViewBag.ProdutoID = Id;
            ViewData["ProdutoID"] = Id;
            TempData["ProdutoID"] = Id;


            List<ProdutoMaterial> lista = new ProdutoMaterialRepository().GetAll();
            List<ProdutoMaterial> materiais = new List<ProdutoMaterial>();



            foreach (var material in lista)
            {
                if (material.Produto_Id == Id)
                {
                    materiais.Add(material);
                }


            }
            double totalMateriais = calculaTotalMateriais(materiais);
            double totalProduto = Convert.ToDouble(new ProdutoRepository().GetOne(Id).Preco);

            ViewBag.TotalMateriais = totalMateriais;
            ViewBag.TotalProduto = totalProduto;
            var total = (1 -(totalMateriais / totalProduto)) * 100;
            ViewBag.MLucro = total;
            


            return View(materiais);
        }

        private double calculaTotalMateriais(List<ProdutoMaterial> materiais)
        {
            double total = 0;
            foreach(var material in materiais){
                total += (material.QtdMaterial * Convert.ToDouble(material.Material.Preco_produto)); 
            }

            return total;
        }

        //
        // GET: /ProdutoMaterial/Details/5
        public ActionResult Details(int id)
        {
            return View(ProdutoMaterialRepository.GetOne(id));
        }
        public bool Validate(ProdutoMaterial entity)
        {
        
            return true;
        }
    
        public int ProdutoMaterialID;
      
        public ActionResult listMateriais(int Id)
        {
            ProdutoMaterialID = Id;
            ViewBag.ProdutoMaterialID = Id;
            ViewData["ProdutoMaterialID"] = Id;
            TempData["ProdutoMaterialID"] = Id;
            List<MateriaPrima> lista = new MateriaPrimaRepository().GetAll();
            List<MateriaPrima> materiais = new List<MateriaPrima>();

          

            return View(materiais);
            
        }


    
        //
        // POST: /ProdutoMaterial/Create
        [HttpPost]
        public ActionResult Create(ProdutoMaterial entity)
        {
            try

            {
                entity.Id = 0;
                

                ProdutoMaterialRepository.Save(entity);
                ViewBag.Message = "Dados salvos com sucesso.";
                return RedirectToAction("Index", "ProdutoMaterial", new { Id = entity.Produto_Id });
            }
            catch
            {
                return View();
            }
        }
    

        public ActionResult Create(int Id)
        {
            loadForm();
            ProdutoMaterial material = new ProdutoMaterial();
            material.Produto_Id = Id;
            return View(material);
        }

       
        public ActionResult createMaterial(MateriaPrima entity)
        {
            new MateriaPrimaRepository().Save(entity);

            return RedirectToAction("Index", new { Message = "Dados salvos com sucesso." });
        }

        public ActionResult createMaterail(int Id)
        {
            MateriaPrima material = new MateriaPrima();
          
            return View(material);
        }

        //
        // GET: /ProdutoMaterial/Edit/5
        public ActionResult Edit(int id)
        {
            loadForm();
            return View(ProdutoMaterialRepository.GetOne(id));
        }

        //
        // POST: /ProdutoMaterial/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProdutoMaterial entity)
        {
            try
            {

                if (Validate(entity))
                    return View(entity);

                ProdutoMaterialRepository.Save(entity);
                ViewBag.Message = "Dados editados com sucesso.";
                return RedirectToAction("Index", new { Message = "Dados salvos com sucesso." });
            }
            catch
            {
                return View(entity);
            }
        }

        //
        // GET: /ProdutoMaterial/Delete/5
        public ActionResult Delete(int id)
        {
            return View(ProdutoMaterialRepository.GetOne(id));
        }

        //
        // POST: /ProdutoMaterial/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ProdutoMaterial entity)
        {
            try
            {
                entity = ProdutoMaterialRepository.GetOne(id);
                ProdutoMaterialRepository.Delete(entity);
                ViewBag.Message = "Dados deletados com sucesso.";
                return RedirectToAction("Index", new { message = "Dados excluidos com sucesso" });
            }
            catch
            {
                return View();
            }
        }


        public void loadForm()
        {
            ViewData["lstMaterial"] = new MaterialRepository().GetAll();
            ViewData["lstProdutoMaterial"] = new ProdutoMaterialRepository().GetAll();
                       
        }

        public ActionResult Report(String id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report"), "ProdutoMaterialReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<ProdutoMaterial> cm = new List<ProdutoMaterial>();
            ProdutoMaterialRepository dc = new ProdutoMaterialRepository();
           
                cm = dc.GetAll();
           
            ReportDataSource rd = new ReportDataSource("DataSetProdutoMaterial", cm);
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


        public ActionResult ReportProdutoMaterialView(String id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report"), "ProdutoMaterialMaterialViewReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<ProdutoMaterial> cm = new List<ProdutoMaterial>();
            ProdutoMaterialRepository dc = new ProdutoMaterialRepository();

                        cm = dc.GetAll();
            List<MateriaPrima> mp = new MateriaPrimaRepository().GetAll();

            ReportDataSource rd = new ReportDataSource("DataSetProdutoMaterialView", cm);
            ReportDataSource rd2 = new ReportDataSource("DataSetProdutoMaterialView", mp);
            lr.DataSources.Add(rd);
            lr.DataSources.Add(rd2);

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
