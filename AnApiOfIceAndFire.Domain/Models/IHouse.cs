using System.Collections.Generic;

namespace AnApiOfIceAndFire.Domain.Models
{
    public interface IHouse
    {
        int Identifier { get; }
        string Name { get; }
        string Region { get; }
        string CoatOfArms { get; }
        string Words { get; }
        IReadOnlyCollection<string> Titles { get; }
        IReadOnlyCollection<string> Seats { get; }

        ICharacter CurrentLord { get; }
        ICharacter Heir { get; }
        ICharacter Overlord { get; }

        string Founded { get; }
        ICharacter Founder { get; }
        string DiedOut { get; }

        IReadOnlyCollection<string> AncestralWeapons { get; }

        IReadOnlyCollection<IHouse> CadetBranches { get; }
        IReadOnlyCollection<ICharacter> SwornMembers { get; }
    }
}