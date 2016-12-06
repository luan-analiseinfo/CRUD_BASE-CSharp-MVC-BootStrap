using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SM_CUSTEIO_WEB.Models;

namespace SM_CUSTEIO_WEB.Repository
{
    public class EmpresaRepository : BaseRepository
    {
        public Empresa GetOne(int codigo)
        {
            return DataModel.Empresa.FirstOrDefault(e => e.Id == codigo);
        }

        public List<Empresa> GetAll()
        {
            return DataModel.Empresa.ToList();
        }

        public List<Empresa> GetAllByName(string name)
        {
            return DataModel.Empresa.Where(e => e.Nome == name).ToList();
        }

        public void Delete(Empresa entity)
        {
            DataModel.Empresa.Remove(entity);
            DataModel.SaveChanges();

        }

        public void Save(Empresa entity)
        {
            DataModel.Entry(entity).State = entity.Id == 0 ?
                EntityState.Added : EntityState.Modified;
            DataModel.SaveChanges();
        }

    }
}