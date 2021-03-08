using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNADomain
{
    public class Dataset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int LinksCount { get; set; }
        public int UsersCount { get; set; }
        public double AvgFriendsCount { get; set; }
    }
}
