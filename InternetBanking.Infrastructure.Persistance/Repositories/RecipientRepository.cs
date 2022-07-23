using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;


namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class RecipientRepository : GenericRepository<Recipient>, IRecipientRepository
    {
        private readonly AppDbContext _db;
        public RecipientRepository(AppDbContext db): base(db)
        {
            _db = db;
        }


    }
}
