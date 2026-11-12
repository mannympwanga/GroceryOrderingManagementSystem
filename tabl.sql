USE [master]
GO

/****** Object:  Table [dbo].[tblGroceryItems]    Script Date: 11/5/2026 4:40:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblGroceryItems](
	[ItemID] [varchar](10) NOT NULL,
	[Description] [varchar](150) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[Image] [varchar](255) NULL,
	[Available] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

