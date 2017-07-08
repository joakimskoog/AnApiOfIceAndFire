CREATE TABLE [dbo].[book_character_link](
	[BookId] [int] NOT NULL,
	[CharacterId] [int] NOT NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_book_character_link] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC,
	[CharacterId] ASC,
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]