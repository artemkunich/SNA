namespace SNADomain
{
    public class Link
    {

        public int DatasetId { get; set; }
        public Dataset Dataset { get; set; }

        public int User1Id { get; set; }
        public int User2Id { get; set; }
    }
}
