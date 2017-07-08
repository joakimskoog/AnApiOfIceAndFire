CREATE TABLE [dbo].[character_house_link](
	[CharacterId] [int] NOT NULL,
	[HouseId] [int] NOT NULL,
 CONSTRAINT [PK_character_house_link] PRIMARY KEY CLUSTERED 
(
	[CharacterId] ASC,
	[HouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]