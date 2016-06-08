using System;
using System.Collections.Generic;
using AnApiOfIceAndFire.Domain.Characters;

// ReSharper disable InconsistentNaming

namespace AnApiOfIceAndFire.Domain.Books
{
    public interface IBook
    {
        int Identifier { get; }
        string Name { get; }
        string ISBN { get; }
        IReadOnlyCollection<string> Authors { get; }
        int NumberOfPages { get; }
        string Publisher { get; }
        string Country { get; }
        MediaType MediaType { get; }
        DateTime Released { get; }
        IReadOnlyCollection<ICharacter> Characters { get; }
        IReadOnlyCollection<ICharacter> POVCharacters { get; }  
    }
}