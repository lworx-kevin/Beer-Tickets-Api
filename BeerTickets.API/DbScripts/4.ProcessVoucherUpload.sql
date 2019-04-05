USE [sunwingVouchers]
GO

/****** Object:  Table [dbo].[ProcessVouchersUpload]    Script Date: 4/5/2019 2:48:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProcessVouchersUpload](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UploadId] [int] NULL,
	[Status] [smallint] NULL,
	[IsEmailSent] [bit] NULL,
	[IsPdfSent] [bit] NULL,
	[ErrorMsg] [varchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModefiedOn] [datetime] NULL,
	[ModefiedBy] [varchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_ProcessVouchersUpload] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProcessVouchersUpload]  WITH CHECK ADD  CONSTRAINT [FK_ProcessVouchersUpload_VouchersUpload] FOREIGN KEY([UploadId])
REFERENCES [dbo].[VouchersUpload] ([Id])
GO

ALTER TABLE [dbo].[ProcessVouchersUpload] CHECK CONSTRAINT [FK_ProcessVouchersUpload_VouchersUpload]
GO


