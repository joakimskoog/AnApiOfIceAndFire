CREATE TABLE [dbo].[house_cadetbranch_link](
	[HouseId] [int] NOT NULL,
	[CadetBranchHouseId] [int] NOT NULL,
 CONSTRAINT [PK_house_cadetbranch_link] PRIMARY KEY CLUSTERED 
(
	[HouseId] ASC,
	[CadetBranchHouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]