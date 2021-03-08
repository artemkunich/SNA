using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SNADomain;

namespace SNAEntityFramework
{
    public class EFLinkRepository: ILinkRepository
    {
        SNADbContext dbContext;

        public EFLinkRepository(SNADbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Link>> Get(int datasetId) {
            var links = await dbContext.Links.Where(r => r.DatasetId == datasetId).ToListAsync();
            return links;
        }
        public async Task Create(Link item) {
            await dbContext.Links.AddAsync(item);
        }
        public async Task Update(Link item) {
            await new Task(() => dbContext.Entry(item).State = EntityState.Modified);
        }
        public async Task Delete(int datasetId) {
            var links = dbContext.Links.Where(l => l.DatasetId == datasetId).ToList();
            await new Task(() => dbContext.Links.RemoveRange(links));
        }
    }
}
