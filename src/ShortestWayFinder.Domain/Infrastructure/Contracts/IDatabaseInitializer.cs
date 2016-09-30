using System.Threading.Tasks;

namespace ShortestWayFinder.Domain.Infrastructure.Contracts
{
    public interface IDatabaseInitializer
    {
        Task Seed();
    }
}
