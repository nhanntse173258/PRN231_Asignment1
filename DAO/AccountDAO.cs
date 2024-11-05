using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class AccountDAO : IAccountDAO
    {
        private readonly SilverJewelry2023DbContext _dbContext;

        public AccountDAO()
        {
            _dbContext = new SilverJewelry2023DbContext();
        }

        public BranchAccount GetAccount(string email, string password)
        {
            return _dbContext.BranchAccounts.FirstOrDefault(a => a.EmailAddress == email && a.AccountPassword == password);
        }
    }
}
