namespace AnApiOfIceAndFire.Data.Houses
{
    public class HouseFilter
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string Words { get; set; }
        public bool? HasWords { get; set; }
        public bool? HasTitles { get; set; }
        public bool? HasSeats { get; set; }
        public bool? HasDiedOut { get; set; }
        public bool? HasAncestralWeapons { get; set; }
    }
}