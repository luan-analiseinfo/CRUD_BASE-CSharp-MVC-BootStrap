using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SM_CUSTEIO_WEB.Models;

namespace SM_CUSTEIO_WEB.Repository
{
    public class MaterialRepository : BaseRepository
    {
        public Material GetOne(int codigo)
        {
            return DataModel.Material.FirstOrDefault(e => e.Id == codigo);
        }

        public List<Material> GetAll()
        {
            return DataModel.Material.ToList();
        }

        public List<Material> GetAllByCodigo(int codigo)
        {
            return DataModel.Material.Where(e => e.Id == codigo).ToList();
        }

       


        public void Delete(Material entity)
        {
            DataModel.Material.Remove(entity);
            DataModel.SaveChanges();

        }

        public void Save(Material entity)
        {
            DataModel.Entry(entity).State = entity.Id == 0 ?
                EntityState.Added : EntityState.Modified;
            DataModel.SaveChanges();
        }

    }
}