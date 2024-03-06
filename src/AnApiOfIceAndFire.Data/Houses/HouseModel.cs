using System.Collections.Generic;

namespace AnApiOfIceAndFire.Data.Houses
{
    public class HouseModel : BaseModel
    {
        public string CoatOfArms { get; set; }
        public string Words { get; set; }
        public string Region { get; set; }
        public string Founded { get; set; }
        public string DiedOut { get; set; }

        public string Seats { get; set; }
        public string Titles { get; set; }
        public string AncestralWeapons { get; set; }

        public int? FounderId { get; set; }
        public int? CurrentLordId { get; set; }
        public int? HeirId { get; set; }
        public int? OverlordId { get; set; }


        public int? MainBranchId { get; set; }
        public ICollection<int> CadetBranchIdentifiers { get; set; } = [];

        public ICollection<int> SwornMemberIdentifiers { get; set; } = [];
    }

    internal class HouseCharacter
    {
        public int HouseId { get; set; }
        public int CharacterId { get; set; }
    }

    internal class HouseCadetBranch
    {
        public int MainBranchId { get; set; }
        public int CadetBranchId { get; set; }
    }
}