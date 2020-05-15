using Service.Pattern;
using Solution.Data.Infrastructure;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
    public class ReclamationPService : Service<ReclamationP>, IReclamationPService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public ReclamationPService() : base(utk)
        {

        }
        public ReclamationP GetReclamByName(string name)
        {
            return Get(f => f.Nom.Equals(name));
        }

        public IEnumerable<ReclamationP> GetReclamByTitle(string Nom)
        {
            return GetMany(f => f.Nom.Contains(Nom));
        }

        public string GetTitleReclamById(int id)
        {
            return GetById(id).Nom;
        }
    }
}
