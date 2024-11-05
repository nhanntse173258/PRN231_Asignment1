using BOs.Entities;
using DAO;

namespace Repository
{
    public class JewelryRepo : IJewelryRepo
    {
        private readonly IJewelryDAO _jewelryDAO;

        public JewelryRepo(IJewelryDAO jewelryDAO)
        {
            _jewelryDAO = jewelryDAO;
        }

        public bool AddJewelry(SilverJewelry jewelry)
        {
            return _jewelryDAO.AddJewelry(jewelry);
        }

        public bool RemoveJewelry(string jewelryId)
        {
            return _jewelryDAO.RemoveJewelry(jewelryId);
        }

        public bool UpdateJewlry(SilverJewelry jewelry)
        {
            return _jewelryDAO.UpdateJewelry(jewelry);
        }

        public SilverJewelry GetJewelry(string jewelryId)
        {
            return _jewelryDAO.GetJewelry(jewelryId);
        }

        public List<SilverJewelry> GetJewelrys()
        {
            return _jewelryDAO.GetJewelrys();
        }
    }
}
