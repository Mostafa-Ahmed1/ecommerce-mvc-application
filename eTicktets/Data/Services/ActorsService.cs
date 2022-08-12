using eTicktets.Data.Base;
using eTicktets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicktets.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext context) : base(context) { } 
    }
}
