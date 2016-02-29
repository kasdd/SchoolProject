using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecten2.NET.Models.Domain.IRepositories
{
    interface IDoelgroepRepository
    {
        Doelgroep FindById(int id);
        IQueryable<Doelgroep> FindAll();
        void SaveChanges();
        void AddDoelgroep(Doelgroep doelgroep);
    }
}
