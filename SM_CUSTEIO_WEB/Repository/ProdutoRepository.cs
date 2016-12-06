using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SM_CUSTEIO_WEB.Models;

namespace SM_CUSTEIO_WEB.Repository
{
    public class ProdutoRepository : BaseRepository
    {
        public Produto GetOne(int codigo)
        {
            return DataModel.Produto.FirstOrDefault(e => e.Id == codigo);
        }

        public List<Produto> GetAll()
        {
            return DataModel.Produto.ToList();
        }

        public List<Produto> GetAllByCodigo(int codigo)
        {
            return DataModel.Produto.Where(e => e.Id == codigo).ToList();
        }

       


        public void Delete(Produto entity)
        {
            DataModel.Produto.Remove(entity);
            DataModel.SaveChanges();

        }

        public void Save(Produto entity)
        {
            DataModel.Entry(entity).State = entity.Id == 0 ?
                EntityState.Added : EntityState.Modified;
            DataModel.SaveChanges();
        }

    }
}