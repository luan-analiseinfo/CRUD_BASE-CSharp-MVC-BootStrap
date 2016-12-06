using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SM_CUSTEIO_WEB.Models;

namespace SM_CUSTEIO_WEB.Repository
{
    public class ProdutoMaterialRepository : BaseRepository
    {
        public ProdutoMaterial GetOne(int codigo)
        {
            return DataModel.ProdutoMaterial.FirstOrDefault(e => e.Id == codigo);
        }

        public List<ProdutoMaterial> GetAll()
        {
            return DataModel.ProdutoMaterial.ToList();
        }

        public List<ProdutoMaterial> GetAllByCodigo(int codigo)
        {
            return DataModel.ProdutoMaterial.Where(e => e.Id == codigo).ToList();
        }

       


        public void Delete(ProdutoMaterial entity)
        {
            DataModel.ProdutoMaterial.Remove(entity);
            DataModel.SaveChanges();

        }

        public void Save(ProdutoMaterial entity)
        {
            DataModel.Entry(entity).State = entity.Id == 0 ?
                EntityState.Added : EntityState.Modified;
            DataModel.SaveChanges();
        }

    }
}