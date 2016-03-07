using System.Collections.Generic;

namespace AnApiOfIceAndFire.Domain.Models
{
    public interface ICharacter
    {
        int Identifier { get; }
        string Name { get; }
        Gender Gender { get; }
        string Culture { get; }
        string Born { get; }
        string Died { get; }
        IReadOnlyCollection<string> Titles { get; }
        IReadOnlyCollection<string> Aliases { get; }

        ICharacter Father { get; }
        ICharacter Mother { get; }
        ICharacter Spouse { get; }

        IReadOnlyCollection<IHouse> Allegiances { get; }

        IReadOnlyCollection<IBook> Books { get; }
        IReadOnlyCollection<IBook> PovBooks { get; }

        IReadOnlyCollection<string> TvSeries { get; }
        IReadOnlyCollection<string> PlayedBy { get; }
    }
}