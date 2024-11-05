using BOs.Entities;

namespace Repository
{
    public interface IJewelryRepo
    {
        public bool AddJewelry(SilverJewelry jewelry);
        public bool RemoveJewelry(string jewelryId);
        public bool UpdateJewlry(SilverJewelry jewelry);
        public SilverJewelry GetJewelry(string jewelryId);
        public List<SilverJewelry> GetJewelrys();
    }
}
