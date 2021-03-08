using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNADomain
{
    public interface IUnitOfWork
    {
        IDatasetRepository DatasetRepository { get; set; }
        ILinkRepository LinkRepository { get; set; }
        Task Save();
    }
}
