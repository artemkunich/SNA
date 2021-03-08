using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SNADomain;

namespace SNAEntityFramework
{
    public class EFDatasetRepository: IDatasetRepository
    {
        SNADbContext dbContext;

        public EFDatasetRepository(SNADbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Dataset>> GetAll() {
            return await Task.FromResult<IEnumerable<Dataset>>(dbContext.Datasets);
        }
        public async Task<Dataset> Get(int id) {
            return await Task.FromResult(dbContext.Datasets.Where(d=>d.Id == id).FirstOrDefault());
        }
        public async Task Create(Dataset item) {
            await dbContext.Datasets.AddAsync(item);
        }
        public async Task Update(Dataset item) {
            await new Task(() => dbContext.Entry(item).State = EntityState.Modified);
        }
        public async Task Delete(int id) {
            
            var dataset = dbContext.Datasets.Where(p => p.Id == id).ToList();            
            dbContext.Datasets.RemoveRange(dataset);
        }
    }
}
