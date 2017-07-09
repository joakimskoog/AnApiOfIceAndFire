using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Books
{
    public class BookRepository : BaseRepository<BookEntity, BookFilter>
    {
        private const string SelectSingleBookQuery = @"SELECT* FROM dbo.books WHERE Id = @Id
                                                       SELECT* FROM dbo.book_character_link WHERE BookId = @Id";

        public BookRepository(IOptions<ConnectionOptions> options) : base(options)
        {
        }

        public override async Task<BookEntity> GetEntityAsync(int id)
        {
            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                using (var reader = await connection.QueryMultipleAsync(SelectSingleBookQuery, new { Id = id }))
                {
                    var book = await reader.ReadFirstOrDefaultAsync<BookEntity>();

                    if (book != null)
                    {
                        foreach (var characterInBook in await reader.ReadAsync())
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

        public override async Task<IPagedList<BookEntity>> GetPaginatedEntitiesAsync(int page, int pageSize, BookFilter filter)
        {
            var builder = new SqlBuilder();
            var countBuilder = new SqlBuilder();
            var template = builder.AddTemplate("SELECT* FROM dbo.books /**where**/ ORDER BY ID OFFSET @RowsToSkip ROWS FETCH NEXT @PageSize ROWS ONLY");
            var countTemplate = countBuilder.AddTemplate("SELECT COUNT(Id) FROM dbo.books /**where**/");

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
                builder.Where("ReleaseDate <= @FromReleaseDate", new { filter.FromReleaseDate });
                countBuilder.Where("ReleaseDate >= @FromReleaseDate", new { filter.FromReleaseDate });
            }

            var rowsToSkip = (page - 1) * pageSize;
            builder.AddParameters(new { RowsToSkip = rowsToSkip, PageSize = pageSize });

            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                await connection.OpenAsync();
                var countTask = connection.QuerySingleAsync<int>(countTemplate.RawSql, countTemplate.Parameters);
                var books = (await connection.QueryAsync<BookEntity>(template.RawSql, template.Parameters)).ToList();

                var bookCharacterRelationships = (await connection.QueryAsync(
                    "SELECT* FROM book_character_link WHERE BookId IN @identifiers", new
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

                return  new PagedList<BookEntity>(new PageMetadata(totalNumberOfBooks, page, pageSize), books);
            }
        }
    }
}