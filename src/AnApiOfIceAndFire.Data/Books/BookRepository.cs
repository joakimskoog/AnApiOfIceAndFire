﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Books
{
    public class BookRepository : BaseRepository<BookEntity, BookFilter>
    {
        private const string SelectSingleBookQuery = @"SELECT* FROM dbo.books WHERE Id = @Id
                                                       SELECT* FROM dbo.book_character_link WHERE BookId = @Id";

        public BookRepository(string connectionString) : base(connectionString)
        {
        }

        //Id Name    ISBN Authors NumberOfPages Publisher   MediaType Country ReleaseDate BookId  CharacterId Type
        //1	A Game of Thrones	978-0553103540	George R.R.Martin 694	Bantam Books	0	United States	1996-08-01 00:00:00.0000000	1	2	0


        public override async Task<BookEntity> GetEntityAsync(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var reader = await connection.QueryMultipleAsync(SelectSingleBookQuery, new { Id = id }))
                {
                    var book = reader.ReadFirstOrDefault<BookEntity>();

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

        public override Task<IPagedList<BookEntity>> GetPaginatedEntitiesAsync(int page, int pageSize, BookFilter filter = null)
        {
            throw new NotImplementedException();
        }
    }
}