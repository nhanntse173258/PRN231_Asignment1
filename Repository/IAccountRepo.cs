using BOs.Entities;

namespace Repository
{
    public interface IAccountRepo
    {
        public BranchAccount GetAccount(string username, string password);
    }
}
