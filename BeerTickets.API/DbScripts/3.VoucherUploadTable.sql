﻿USE [sunwingVouchers]
GO

/****** Object:  Table [dbo].[VouchersUpload]    Script Date: 4/5/2019 2:48:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VouchersUpload](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UploadContent] [varchar](max) NULL,
	[Status] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModefiedOn] [datetime] NULL,
	[ModefiedBy] [varchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
	[SuccessfullyProcessed] [int] NULL,
	[NotSuccessfullyProcessed] [int] NULL,
	[TotalProcessed] [int] NULL,
 CONSTRAINT [PK_VouchersUpload] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


