using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecten2.NET.Models.Domain.IRepositories
{
    public interface IReservatieRepository
    {
        IQueryable<Reservatie> getbyDate(DateTime datum);
        IQueryable<Reservatie> FindAll();
        void AddReservatie(Reservatie r);
        void RemoveReservatie(Reservatie r);
        void SaveChanges();
    }
}
