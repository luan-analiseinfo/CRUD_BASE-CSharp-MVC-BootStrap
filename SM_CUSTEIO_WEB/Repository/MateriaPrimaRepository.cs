using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SM_CUSTEIO_WEB.Models;

namespace SM_CUSTEIO_WEB.Repository
{
    public class MateriaPrimaRepository : BaseRepository
    {
        public MateriaPrima GetOne(int codigo)
        {
            return DataModel.MateriaPrima.FirstOrDefault(e => e.Id == codigo);
        }

        public List<MateriaPrima> GetAll()
        {
            return DataModel.MateriaPrima.ToList();
        }

        public List<MateriaPrima> GetAllByCodigo(int codigo)
        {
            return DataModel.MateriaPrima.Where(e => e.Id == codigo).ToList();
        }

        public void Delete(MateriaPrima entity)
        {
            DataModel.MateriaPrima.Remove(entity);
            DataModel.SaveChanges();

        }

        public void Save(MateriaPrima entity)
        {
            DataModel.Entry(entity).State = entity.Id == 0 ?
                EntityState.Added : EntityState.Modified;
            DataModel.SaveChanges();
        }

    }
}