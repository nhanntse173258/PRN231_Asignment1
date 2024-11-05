using BOs.Entities;
using DAO;

namespace Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly IAccountDAO _accountDAO;

        public AccountRepo(IAccountDAO accountDAO)
        {
            _accountDAO = accountDAO;
        }
        public BranchAccount GetAccount(string email, string password)
        {
            return _accountDAO.GetAccount(email, password);
        }
    }
}
