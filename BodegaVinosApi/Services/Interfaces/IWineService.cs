using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IWineService
    {
        int AddWine(WineDto wineDto);
        List<WineDto> GetAllWines(); 
        WineDto GetWineByName(string name); 
        bool UpdateStock(int id, int newStock); 
    }
}
