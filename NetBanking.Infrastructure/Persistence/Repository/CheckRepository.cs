using NetBanking.Core.Entitys;
using NetBanking.Core.Enumerables;
using NetBanking.Core.Interfaces.Persistence;
using NetBanking.Infrastructure.Data;


namespace NetBanking.Infrastructure.Persistence.Repository
{
    public class CheckRepository : BaseRepository<Check>, ICheckRepository
    {

        public CheckRepository(NetBankingDbContext context) : base(context)
        {}

        public IEnumerable<Check> GetCirculatingMethod()
        {
            return _entity.Where(x => x.CheckStatus == CheckStatus.inCirculation);
        }
    }
}
