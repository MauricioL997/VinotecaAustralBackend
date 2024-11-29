using Data.Context;
using Data.Entities;
using Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Repositories
{
    public class WineRepository : IWineRepository
    {
        private readonly ApplicationContext _context;

        public WineRepository(ApplicationContext context)
        {
            _context = context;
        }

        // Registrar un nuevo vino
        public int AddWine(Wine wine)
        {
            var existingWine = _context.Wines.FirstOrDefault(w => w.Name == wine.Name);
            if (existingWine != null)
            {
                throw new InvalidOperationException($"Ya existe un vino registrado con el nombre '{wine.Name}'.");
            }

            _context.Wines.Add(wine);
            _context.SaveChanges();
            return wine.Id;
        }

        // Consultar todos los vinos
        public List<Wine> GetAllWines()
        {
            return _context.Wines.ToList();
        }

        // Consultar un vino por su nombre
        public Wine? GetByName(string name)
        {
            return _context.Wines.FirstOrDefault(w => w.Name == name);
        }

        // Actualizar el stock de un vino
        public bool UpdateStock(int id, int newStock)
        {
            var wine = _context.Wines.Find(id);

            if (wine == null)
            {
                return false;
            }

            if (newStock < 0)
            {
                throw new ArgumentException("El stock no puede ser negativo.");
            }

            wine.Stock = newStock;
            _context.SaveChanges();
            return true; 
        }

    }
}