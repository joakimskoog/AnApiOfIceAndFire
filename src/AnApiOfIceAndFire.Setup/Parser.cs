using AnApiOfIceAndFire.Data.Books;
using AnApiOfIceAndFire.Data.Characters;
using AnApiOfIceAndFire.Data.Houses;
using nietras.SeparatedValues;

namespace AnApiOfIceAndFire.Database.Seeder
{
    internal static class Parser
    {
        public static (BookModel[] books, CharacterModel[] characters, HouseModel[] houses) Parse()
        {
            var books = ParseBooks();
            var characters = ParseCharacters();
            var houses = ParseHouses();

            ParseAndBuildRelationships(books, characters, houses);

            return (books, characters, houses);
        }

        private static BookModel[] ParseBooks()
        {
            var books = new BookModel[DataConstants.BooksMaxId + 1];

            using var reader = Sep.Reader().FromFile(DataConstants.BooksFilePath);
            foreach (var row in reader)
            {
                var book = new BookModel();
                book.Id = row["id"].Parse<int>();
                book.Name = row["name"].ToString();
                book.ISBN = row["isbn"].ToString();
                book.Authors = row["authors"].ToString();
                book.NumberOfPages = row["number_of_pages"].Parse<int>();
                book.Publisher = row["publisher"].ToString();
                book.MediaType = Enum.Parse<MediaType>(row["media_type"].Span);
                book.Country = row["country"].ToString();
                book.ReleaseDate = row["release_date"].Parse<DateTime>();

                books[book.Id] = book;
            }

            return books;
        }

        private static CharacterModel[] ParseCharacters()
        {
            var characters = new CharacterModel[DataConstants.CharactersMaxId + 1];

            using var reader = Sep.Reader(options => options with { Unescape = true }).FromFile(DataConstants.CharactersFilePath);
            foreach (var row in reader)
            {
                var character = new CharacterModel();
                character.Id = row["id"].Parse<int>();
                character.Name = row["name"].ToString();
                character.Gender = Enum.Parse<Gender>(row["gender"].Span, true);
                character.Culture = row["culture"].ToString();
                character.Born = row["born"].ToString();
                character.Died = row["died"].ToString();
                characters[character.Id] = character;
            }

            using var aliasesReader = Sep.Reader(options => options with { Unescape = true }).FromFile(DataConstants.CharacterAliasesFilePath);
            foreach (var row in aliasesReader)
            {
                var characterId = row["character_id"].Parse<int>();
                var alias = row["alias"].ToString();

                characters[characterId].Aliases += $";{alias}";
            }

            using var playedByReader = Sep.Reader().FromFile(DataConstants.CharacterPlayedByFilePath);
            foreach (var row in playedByReader)
            {
                var characterId = row["character_id"].Parse<int>();
                var playedBy = row["played_by"].ToString();

                characters[characterId].PlayedBy += $";{playedBy}";
            }

            var titlesLookup = new string[DataConstants.TitlesMaxId + 1];
            using var titlesReader = Sep.Reader(options => options with { Unescape = true }).FromFile(DataConstants.TitlesFilePath);
            foreach (var row in titlesReader)
            {
                var id = row["id"].Parse<int>();
                var title = row["title"].ToString();

                titlesLookup[id] = title;
            }

            using var characterTitlesReader = Sep.Reader().FromFile(DataConstants.CharacterTitlesFilePath);
            foreach (var row in characterTitlesReader)
            {
                var characterId = row["character_id"].Parse<int>();
                var titleId = row["title_id"].Parse<int>();

                characters[characterId].Titles += $";{titlesLookup[titleId]}";
            }

            var tvSeriesLookup = new string[DataConstants.TvSeriesMaxId + 1];
            using var tvSeriesReader = Sep.Reader().FromFile(DataConstants.TvSeriesFilePath);
            foreach (var row in tvSeriesReader)
            {
                var id = row["id"].Parse<int>();
                var tvSeries = row["tv_series"].ToString();

                tvSeriesLookup[id] = tvSeries;
            }

            using var characterTvSeriesReader = Sep.Reader().FromFile(DataConstants.CharacterTvSeriesFilePath);
            foreach (var row in characterTvSeriesReader)
            {
                var characterId = row["character_id"].Parse<int>();
                var tvSeriesId = row["tv_series_id"].Parse<int>();

                characters[characterId].TvSeries += $";{tvSeriesLookup[tvSeriesId]}";
            }

            return characters;
        }

        private static HouseModel[] ParseHouses()
        {
            var houses = new HouseModel[DataConstants.HousesMaxId + 1];

            //using var aliasesReader = Sep.Reader(options => options with { Unescape = true }).FromFile(DataConstants.CharacterAliasesFilePath);
            using var reader = Sep.Reader(options => options with { Unescape = true }).FromFile(DataConstants.HousesFilePath);
            foreach (var row in reader)
            {
                var house = new HouseModel();
                house.Id = row["id"].Parse<int>();
                house.Name = row["name"].ToString();
                house.Region = row["region"].ToString();
                house.CoatOfArms = row["coat_of_arms"].ToString();
                house.Words = row["words"].ToString();
                house.Founded = row["founded"].ToString();
                house.DiedOut = row["died_out"].ToString();

                houses[house.Id] = house;
            }

            using var ancestralWeaponsReader = Sep.Reader().FromFile(DataConstants.HouseAncestralWeaponsFilePath);
            foreach (var row in ancestralWeaponsReader)
            {
                var houseId = row["house_id"].Parse<int>();
                var ancestralWeapon = row["ancestral_weapon"].ToString();

                houses[houseId].AncestralWeapons += $";{ancestralWeapon}";
            }

            using var seatsReader = Sep.Reader().FromFile(DataConstants.HouseSeatsFilePath);
            foreach (var row in seatsReader)
            {
                var houseId = row["house_id"].Parse<int>();
                var seat = row["seat"].ToString();

                houses[houseId].Seats += $";{seat}";
            }

            using var titlesReader = Sep.Reader(options => options with { Unescape = true }).FromFile(DataConstants.HouseTitlesFilePath);
            foreach (var row in titlesReader)
            {
                var houseId = row["house_id"].Parse<int>();
                var title = row["title"].ToString();

                houses[houseId].Titles += $";{title}";
            }

            return houses;
        }

        private static void ParseAndBuildRelationships(BookModel[] books, CharacterModel[] characters, HouseModel[] houses)
        {
            using var bookCharactersReader = Sep.Reader().FromFile(DataConstants.BookCharactersFilePath);
            foreach (var row in bookCharactersReader)
            {
                var bookId = row["book_id"].Parse<int>();
                var characterId = row["character_id"].Parse<int>();

                characters[characterId].BookIdentifiers.Add(bookId);
            }

            using var bookPovCharactersReader = Sep.Reader().FromFile(DataConstants.BookPovCharactersFilePath);
            foreach (var row in bookPovCharactersReader)
            {
                var bookId = row["book_id"].Parse<int>();
                var characterId = row["character_id"].Parse<int>();

                characters[characterId].PovBookIdentifiers.Add(bookId);
            }

            using var characterRelationshipsReader = Sep.Reader().FromFile(DataConstants.CharacterRelationshipsFilePath);
            foreach (var row in characterRelationshipsReader)
            {
                var character = characters[row["character_id"].Parse<int>()];

                if (row["father"].TryParse<int>(out var fatherId))
                {
                    character.FatherId = fatherId;
                }
                if (row["mother"].TryParse<int>(out var motherId))
                {
                    character.MotherId = motherId;
                }
                if (row["spouse"].TryParse<int>(out var spouseId))
                {
                    character.SpouseId = spouseId;
                }
            }

            using var houseRelationshipsReader = Sep.Reader().FromFile(DataConstants.HouseRelationshipsFilePath);
            foreach (var row in houseRelationshipsReader)
            {
                var house = houses[row["house_id"].Parse<int>()];

                if (row["current_lord"].TryParse<int>(out var currentLordId))
                {
                    house.CurrentLordId = currentLordId;
                }
                if (row["founder"].TryParse<int>(out var founderId))
                {
                    house.FounderId = founderId;
                }
                if (row["heir"].TryParse<int>(out var heirId))
                {
                    house.HeirId = heirId;
                }
                if (row["overlord"].TryParse<int>(out var overlordId))
                {
                    house.OverlordId = overlordId;
                }
            }

            using var houseCadetBranchesReader = Sep.Reader().FromFile(DataConstants.HouseCadetBranchesFilePath);
            foreach (var row in houseCadetBranchesReader)
            {
                var houseId = row["house_id"].Parse<int>();
                var cadetBranchId = row["cadet_branch_id"].Parse<int>();

                houses[cadetBranchId].MainBranchId = houseId;
            }

            using var houseCharactersReader = Sep.Reader().FromFile(DataConstants.HouseCharactersFilePath);
            foreach (var row in houseCharactersReader)
            {
                var houseId = row["house_id"].Parse<int>();
                var characterId = row["character_id"].Parse<int>();

                houses[houseId].SwornMemberIdentifiers.Add(characterId);
            }
        }
    }
}