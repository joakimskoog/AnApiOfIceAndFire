CREATE TABLE [dbo].[characters](
	[Id] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Culture] [varchar](50) NULL,
	[Born] [varchar](200) NOT NULL,
	[Died] [varchar](100) NOT NULL,
	[IsFemale] [bit] NULL,
	[Aliases] [varchar](200) NULL,
	[Titles] [varchar](200) NULL,
	[TvSeries] [varchar](200) NULL,
	[PlayedBy] [varchar](200) NULL,
	[FatherId] [int] NULL,
	[MotherID] [int] NULL,
	[SpouseId] [int] NULL,
 CONSTRAINT [PK_characters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]