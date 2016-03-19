using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecten2.NET.Models.Domain.IRepositories
{
    public interface IBlokkeringRepository
    {
        IQueryable<Blokkering> getbyDate(DateTime datum);
        IQueryable<Blokkering> FindAll();
        void AddBlokkering(Blokkering r);
        void RemoveBlokkering(Blokkering r);
        void SaveChanges();
    }
}