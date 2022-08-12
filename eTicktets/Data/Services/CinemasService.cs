using eTicktets.Data.Base;
using eTicktets.Models;

namespace eTicktets.Data.Services
{
    public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext context) : base(context)
        {
            
        }
    }
}
