using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUD.Models;
using System.Data.Entity;

namespace CRUD.Repository
{
    public class PessoaRepository : BaseRepository
    {
        public Pessoa GetOne(int codigo)
        {
            return DataModel.Pessoa.FirstOrDefault(e => e.Codigo == codigo);
        }

        public List<Pessoa> GetAll()
        {
            return DataModel.Pessoa.ToList();
        }

        public List<Pessoa> GetAllByName(string name)
        {
            return DataModel.Pessoa.Where(e => e.Nome == name).ToList();
        }

        public void Delete(Pessoa entity)
        {
            DataModel.Pessoa.Remove(entity);
            DataModel.SaveChanges();

        }

        public void Save(Pessoa entity)
        {
            DataModel.Entry(entity).State = entity.Codigo == 0 ?
                EntityState.Added : EntityState.Modified;
            DataModel.SaveChanges();
        }

    }
}