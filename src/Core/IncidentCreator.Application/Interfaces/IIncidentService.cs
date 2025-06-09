using IncidentCreator.Application.Dtos;
using System.Threading.Tasks;

namespace IncidentCreator.Application.Interfaces
{
    public interface IIncidentService
    {
        Task<string> CreateIncidentProcessAsync(IncidentCreationRequest request);
    }
}