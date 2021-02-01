using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SNAEntityFramework.Entities
{
    public class Link
    {

        [Key, Column(Order = 0)]
        public int DatasetId { get; set; }
        public Dataset Dataset { get; set; }

        [Key, Column(Order = 1)]
        public int User1Id { get; set; }
        [Key, Column(Order = 2)]
        public int User2Id { get; set; }
    }
}
