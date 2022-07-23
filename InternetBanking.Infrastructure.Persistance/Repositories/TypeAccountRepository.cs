using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Infrastructure.Identity.Entities;
using InternetBanking.Infrastructure.Persistence.Context;


namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class TypeAccountRepository : GenericRepository<TypeAccount>, ITypeAccountRepository
    {
        private readonly AppDbContext _db;
        public TypeAccountRepository(AppDbContext db): base(db)
        {
            _db = db;
        }


    }
}
