using System.Collections.Generic;
using SNADomain;

namespace SNAApplication
{
    public interface IDatasetParser
    {
        public List<Link> Parse(DatasetInput input);
    }
}
