using AnApiOfIceAndFire.Data.Houses;

namespace AnApiOfIceAndFire.DataFeeder
{
    public class HouseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Seats { get; set; }
        public string Region { get; set; }
        public string CoatOfArms { get; set; }
        public string Words { get; set; }
        public string[] Titles { get; set; }
        public int? CurrentLord { get; set; }
        public int? Founder { get; set; }
        public string Founded { get; set; }
        public int? Heir { get; set; }
        public int? Overlord { get; set; }
        public string DiedOut { get; set; }
        public string[] AncestralWeapons { get; set; }
        public int[] CadetBranches { get; set; }

        public HouseEntity ToHouseEntity()
        {
            return new HouseEntity()
            {
                Id = Id,
                Name = Name,
                CoatOfArms = CoatOfArms,
                DiedOut = DiedOut,
                Region = Region,
                Words = Words,
                CurrentLordId = CurrentLord,
                Founded = Founded,
                FounderId = Founder,
                HeirId = Heir,
                OverlordId = Overlord,
                CadetBranchIdentifiers = CadetBranches,
                Titles = string.Join(";", Titles),
                Seats = string.Join(";", Seats),
                AncestralWeapons = string.Join(";", AncestralWeapons),
            };
        }
    }
}