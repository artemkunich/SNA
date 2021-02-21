using SNAEntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SNAServices.Datasets
{
    public interface IDatasetParser
    {
        public List<Link> Parse(DatasetInput input);
    }
}
