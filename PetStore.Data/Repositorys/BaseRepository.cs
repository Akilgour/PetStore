using PetStore.Data.Context;
using Polly;

namespace PetStore.Data.Repositorys
{
    public abstract class BaseRepository
    {
        protected readonly PetStoreContext _context;
        protected readonly IAsyncPolicy _retryPolicy;

        public BaseRepository(PetStoreContext context, IAsyncPolicy retryPolicy)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
            _retryPolicy = retryPolicy ?? throw new System.ArgumentNullException(nameof(retryPolicy));
        }
    }
}
