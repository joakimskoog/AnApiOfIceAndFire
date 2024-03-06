using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Books
{
    public class BookRepository : BaseRepository<BookModel, BookFilter>
    {
        private const string SelectSingleBookQuery = @"SELECT* FROM Books WHERE Id = @Id;
                                                       SELECT* FROM BookCharacters WHERE BookId = @Id;";

        public BookRepository(IOptions<ConnectionOptions> options) : base(options)
        {
        }

        public override async Task<BookModel> GetEntityAsync(int id)
        {
            using (var connection = new SqliteConnection(Options.AnApiOfIceAndFireDatabase))
            {
                using (var reader = await connection.QueryMultipleAsync(SelectSingleBookQuery, new { Id = id }))
                {
                    var book = await reader.ReadFirstOrDefaultAsync<BookModel>();

                    if (book != null)
                    {
                        foreach (var characterInBook in await reader.ReadAsync<BookCharacter>())
                        {
                            var characterId = characterInBook.CharacterId;
                            var type = characterInBook.Type;

                            if (type == 0)
                            {
                                book.CharacterIdentifiers.Add(characterId);
                            }
                            else
                            {
                                book.PovCharacterIdentifiers.Add(characterId);
                            }
                        }
                    }

                    return book;
                }
            }
        }

        public override async Task<IPagedList<BookModel>> GetPaginatedEntitiesAsync(int page, int pageSize, BookFilter filter)
        {
            var builder = new SqlBuilder();
            var countBuilder = new SqlBuilder();
            var template = builder.AddTemplate("SELECT* FROM Books /**where**/ ORDER BY ID LIMIT @PageSize OFFSET @RowsToSkip");
            var countTemplate = countBuilder.AddTemplate("SELECT COUNT(Id) FROM Books /**where**/");

            if (!string.IsNullOrEmpty(filter.Name))
            {
                builder.Where("Name = @Name", new { filter.Name });
                countBuilder.Where("Name = @Name", new { filter.Name });
            }
            if (filter.FromReleaseDate != null)
            {
                builder.Where("ReleaseDate >= @FromReleaseDate", new { filter.FromReleaseDate });
                countBuilder.Where("ReleaseDate >= @FromReleaseDate", new { filter.FromReleaseDate });
            }
            if (filter.ToReleaseDate != null)
            {
                builder.Where("ReleaseDate <= @ToReleaseDate", new { filter.ToReleaseDate });
                countBuilder.Where("ReleaseDate >= @ToReleaseDate", new { filter.ToReleaseDate });
            }

            var rowsToSkip = (page - 1) * pageSize;
            builder.AddParameters(new { RowsToSkip = rowsToSkip, PageSize = pageSize });

            using (var connection = new SqliteConnection(Options.AnApiOfIceAndFireDatabase))
            {
                await connection.OpenAsync();
                var countTask = connection.QuerySingleAsync<int>(countTemplate.RawSql, countTemplate.Parameters);
                var books = (await connection.QueryAsync<BookModel>(template.RawSql, template.Parameters)).ToList();

                var bookCharacterRelationships = (await connection.QueryAsync<BookCharacter>(
                    "SELECT* FROM BookCharacters WHERE BookId IN @identifiers", new
                    {
                        identifiers = books.Select(b => b.Id)
                    })).GroupBy(bcl => bcl.BookId).ToList();
                
                foreach (var book in books)
                {
                    var charactersInBook = bookCharacterRelationships.FirstOrDefault(g => g.Key == book.Id);
                    if (charactersInBook != null)
                    {
                        foreach (var characterInBook in charactersInBook)
                        {
                            var characterId = characterInBook.CharacterId;
                            var type = characterInBook.Type;

                            if (type == 0)
                            {
                                book.CharacterIdentifiers.Add(characterId);
                            }
                            else
                            {
                                book.PovCharacterIdentifiers.Add(characterId);
                            }
                        }    
                    }
                }

                var totalNumberOfBooks = await countTask;

                return  new PagedList<BookModel>(new PageMetadata(totalNumberOfBooks, page, pageSize), books);
            }
        }
    }
}