CREATE TABLE [dbo].[houses](
	[Id] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[CoatOfArms] [varchar](300) NOT NULL,
	[Words] [varchar](200) NOT NULL,
	[Region] [varchar](200) NOT NULL,
	[Founded] [varchar](200) NOT NULL,
	[DiedOut] [varchar](200) NOT NULL,
	[Seats] [varchar](200) NULL,
	[Titles] [varchar](200) NULL,
	[AncestralWeapons] [varchar](200) NULL,
	[FounderId] [int] NULL,
	[CurrentLordId] [int] NULL,
	[HeirId] [int] NULL,
	[OverlordId] [int] NULL,
 CONSTRAINT [PK_houses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]