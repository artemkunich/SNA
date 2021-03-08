using System.Collections.Generic;
using System.Threading.Tasks;

namespace SNADomain
{
    public interface IDatasetRepository
    {
        Task<IEnumerable<Dataset>> GetAll();
        Task<Dataset> Get(int id);
        Task Create(Dataset item);
        Task Update(Dataset item);
        Task Delete(int id);
    }
}
