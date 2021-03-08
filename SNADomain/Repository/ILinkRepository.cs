using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNADomain
{
    public interface ILinkRepository
    {
        Task<IEnumerable<Link>> Get(int datasetId);
        Task Create(Link item);
        Task Update(Link item);
        Task Delete(int id);
    }
}
