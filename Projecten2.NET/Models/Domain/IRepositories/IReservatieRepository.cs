using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecten2.NET.Models.Domain.IRepositories
{
    public interface IReservatieRepository
    {
        IQueryable<ReservatieLijn> getbyDate(DateTime datum);
    }
}
