using BOs.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class JewelryDAO : IJewelryDAO 
    {
        private readonly SilverJewelry2023DbContext _context;

        public JewelryDAO()
        {
            _context = new SilverJewelry2023DbContext();
        }

        public bool AddJewelry(SilverJewelry jewelry)
        {
            _context.SilverJewelries.Add(jewelry);
            return _context.SaveChanges() > 0;
        }

        public bool RemoveJewelry(string jewelryId)
        {
            var jewelry = _context.SilverJewelries.Find(jewelryId);
            if (jewelry == null) return false;

            _context.SilverJewelries.Remove(jewelry);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateJewelry(SilverJewelry jewelry)
        {
            _context.SilverJewelries.Update(jewelry);
            return _context.SaveChanges() > 0;
        }

        public SilverJewelry GetJewelry(string jewelryId)
        {
            return _context.SilverJewelries
                .Include(j => j.Category)
                .FirstOrDefault(j => j.SilverJewelryId == jewelryId);
        }

        public List<SilverJewelry> GetJewelrys()
        {
            return _context.SilverJewelries.ToList();
        }
    }
}
