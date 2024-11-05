using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IJewelryDAO
    {
        bool AddJewelry(SilverJewelry jewelry);
        bool RemoveJewelry(string jewelryId);
        bool UpdateJewelry(SilverJewelry jewelry);
        SilverJewelry GetJewelry(string jewelryId);
        List<SilverJewelry> GetJewelrys();
    }
}
