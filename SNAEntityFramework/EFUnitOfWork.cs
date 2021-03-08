using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNADomain;

namespace SNAEntityFramework
{
    public class EFUnitOfWork: IUnitOfWork
    {
        private SNADbContext _dbContext;

        public EFUnitOfWork(SNADbContext dbContext) {
            DatasetRepository = new EFDatasetRepository(dbContext);
            LinkRepository = new EFLinkRepository(dbContext);
            _dbContext = dbContext;
        }

        public IDatasetRepository DatasetRepository { get; set; }
        public ILinkRepository LinkRepository { get; set; }

        public async Task Save() {
            await _dbContext.SaveChangesAsync();
        }
    }
}
