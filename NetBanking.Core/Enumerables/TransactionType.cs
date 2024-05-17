using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Enumerables
{
    public enum TransactionType
    {
        transaction,
        withdrawal,
        deposit,
        depositCheck,
        issueCheck,
        consumeCard,
        payCard,
        payLoan
    }
}
