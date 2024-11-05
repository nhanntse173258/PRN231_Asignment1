using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IAccountDAO
    {
        BranchAccount GetAccount(string email, string password);
    }
}
