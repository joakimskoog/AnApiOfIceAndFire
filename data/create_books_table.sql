CREATE TABLE [dbo].[books](
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
) ON [PRIMARY]