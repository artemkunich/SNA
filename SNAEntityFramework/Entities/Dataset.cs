using Microsoft.EntityFrameworkCore;
using System;

namespace SNAEntityFramework.Entities
{
    public class Dataset : DBEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int LinksCount { get; set; }
        public int UsersCount { get; set; }
        public double AvgFriendsCount { get; set; }
    }
}
