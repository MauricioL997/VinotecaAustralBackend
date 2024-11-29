using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface IWineRepository
    {
        int AddWine(Wine wine);
        List<Wine> GetAllWines();
        Wine? GetByName(string name);
        public bool UpdateStock(int id, int newStock);
    }
}
