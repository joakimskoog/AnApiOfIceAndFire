namespace AnApiOfIceAndFire.DataFeeder
{
    public class DatabaseScripts
    {
        public static readonly string CreateDatabase = @"CREATE DATABASE [anapioficeandfire] CONTAINMENT = NONE ON  PRIMARY 
            ( NAME = N'anapioficeandfire', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\anapioficeandfire.mdf' , 
                SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
            LOG ON 
            ( NAME = N'anapioficeandfire_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\anapioficeandfire_log.ldf' , 
                SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )";

        public static readonly string DeleteDatabase = @"DROP DATABASE [anapioficeandfire]";

        public static readonly string TruncateAllTables =
                    @"TRUNCATE TABLE anapioficeandfire.dbo.books TRUNCATE TABLE anapioficeandfire.dbo.characters TRUNCATE TABLE anapioficeandfire.dbo.houses 
                    TRUNCATE TABLE anapioficeandfire.dbo.book_character_link TRUNCATE TABLE anapioficeandfire.dbo.character_house_link TRUNCATE TABLE anapioficeandfire.dbo.house_cadetbranch_link";


        public static readonly string CreateBooksTable =
            @"CREATE TABLE [dbo].[books](
	        [Id] [int] NOT NULL,
	        [Name] [varchar](50) NOT NULL,
	        [ISBN] [varchar](50) NOT NULL,
	        [Authors] [varchar](200) NOT NULL,
	        [NumberOfPages] [int] NOT NULL,
	        [Publisher] [varchar](50) NOT NULL,
	        [MediaType] [int] NOT NULL,
	        [Country] [varchar](50) NOT NULL,
	        [ReleaseDate] [datetime2](7) NOT NULL,
            CONSTRAINT [PK_books] PRIMARY KEY CLUSTERED 
            (
	            [Id] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY]";
        
        public static readonly string CreateCharactersTable =
            @"CREATE TABLE [dbo].[characters](
	        [Id] [int] NOT NULL,
	        [Name] [varchar](100) NOT NULL,
	        [Culture] [varchar](50) NULL,
	        [Born] [varchar](200) NOT NULL,
	        [Died] [varchar](100) NOT NULL,
	        [IsFemale] [bit] NOT NULL,
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
            ) ON [PRIMARY]";

        public static readonly string CreateHousesTable =
            @"CREATE TABLE [dbo].[houses](
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
            ) ON [PRIMARY]";

        public static readonly string CreateBookCharacterLinkTable =
            @"CREATE TABLE [dbo].[book_character_link](
	        [BookId] [int] NOT NULL,
	        [CharacterId] [int] NOT NULL,
	        [Type] [int] NOT NULL,
            CONSTRAINT [PK_book_character_link] PRIMARY KEY CLUSTERED 
            (
	            [BookId] ASC,
	            [CharacterId] ASC,
	            [Type] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY]";

        public static readonly string CreateCharacterHouseLinkTable =
            @"CREATE TABLE [dbo].[character_house_link](
	        [CharacterId] [int] NOT NULL,
	        [HouseId] [int] NOT NULL,
            CONSTRAINT [PK_character_house_link] PRIMARY KEY CLUSTERED 
            (
	            [CharacterId] ASC,
	            [HouseId] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY]";
        
        public static readonly string CreateHouseCadetBranchLinkTable =
            @"CREATE TABLE [dbo].[house_cadetbranch_link](
	        [HouseId] [int] NOT NULL,
	        [CadetBranchHouseId] [int] NOT NULL,
            CONSTRAINT [PK_house_cadetbranch_link] PRIMARY KEY CLUSTERED 
            (
	            [HouseId] ASC,
	            [CadetBranchHouseId] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY]";
    }
}