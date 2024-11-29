using Common.DTOs;
using Data.Entities;
using Data.Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class WineService : IWineService
    {
        private readonly IWineRepository _wineRepository;

        public WineService(IWineRepository wineRepository)
        {
            _wineRepository = wineRepository;
        }

        public int AddWine(WineDto wineDto)
        {
            var wine = new Wine
            {
                Name = wineDto.Name,
                Variety = wineDto.Variety,
                Year = wineDto.Year,
                Region = wineDto.Region,
                Stock = wineDto.Stock
            };

            return _wineRepository.AddWine(wine);
        }

        public List<WineDto> GetAllWines()
        {
            var wines = _wineRepository.GetAllWines();
            return wines.Select(w => new WineDto
            {
                Id = w.Id,
                Name = w.Name,
                Variety = w.Variety,
                Year = w.Year,
                Region = w.Region,
                Stock = w.Stock,
                CreatedAt = w.CreatedAt
            }).ToList();
        }

        public WineDto GetWineByName(string name)
        {
            var wine = _wineRepository.GetByName(name);

            if (wine == null)
            {
                throw new KeyNotFoundException($"El vino con el nombre '{name}' no fue encontrado.");
            }

            return new WineDto
            {
                Id = wine.Id,
                Name = wine.Name,
                Variety = wine.Variety,
                Year = wine.Year,
                Region = wine.Region,
                Stock = wine.Stock,
                CreatedAt = wine.CreatedAt
            };
        }

        public bool UpdateStock(int id, int newStock)
        {
            return _wineRepository.UpdateStock(id, newStock);
        }
    }
}
