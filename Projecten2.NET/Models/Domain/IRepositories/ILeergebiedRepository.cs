using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecten2.NET.Models.Domain.IRepositories
{
    interface ILeergebiedRepository
    {
        Leergebied FindById(int Id);
        IQueryable<Leergebied> FindAll();
        void SaveChanges();
        void AddLeergebied(Leergebied leergebied);
    }
}
