USE [sunwingVouchers]
GO

/****** Object:  Table [dbo].[ApiUsers]    Script Date: 4/1/2019 6:48:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ApiUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Token] [varchar](150) NULL,
	[TokenTimestamp] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModefiedOn] [datetime] NULL,
	[ModefiedBy] [varchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_api_users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


