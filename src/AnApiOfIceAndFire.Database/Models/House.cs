namespace AnApiOfIceAndFire.Database.Models
{
    public class House : BaseModel
    {
        public string CoatOfArms { get; set; }
        public string Words { get; set; }
        public string Region { get; set; }
        public string Founded { get; set; }
        public string DiedOut { get; set; }

        public string Seats { get; set; }
        public string Titles { get; set; }
        public string AncestralWeapons { get; set; }

        //public Character Founder { get; set; }
        public int? FounderId { get; set; }

        //public Character CurrentLord { get; set; }
        public int? CurrentLordId { get; set; }

        //public Character Heir { get; set; }
        public int? HeirId { get; set; }

        

        
        //public House MainBranch { get; set; }



        public int? OverlordId { get; set; }
        //public House Overlord { get; set; }


        //public List<House> CadetBranches { get; set; } = [];
        public int? MainBranchId { get; set; }
        public int[] CadetBranchIdentifiers { get; set; }

        public ICollection<int> SwornMemberIdentifiers { get; set; } = [];
        //public List<Character> Characters { get; set; } = [];
    }
}