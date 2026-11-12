USE [master]
GO

/****** Object:  Table [dbo].[tblMemberOrders]    Script Date: 11/5/2026 4:42:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblMemberOrders](
	[OrderID] [varchar](10) NOT NULL,
	[OrderDate] [date] NULL,
	[MemberID] [varchar](10) NOT NULL,
	[ItemID] [varchar](10) NOT NULL,
	[Quantity] [int] NOT NULL,
	[TotalPrice] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblMemberOrders]  WITH CHECK ADD  CONSTRAINT [FK_tblMemberOrders_tblGroceryItems] FOREIGN KEY([ItemID])
REFERENCES [dbo].[tblGroceryItems] ([ItemID])
GO

ALTER TABLE [dbo].[tblMemberOrders] CHECK CONSTRAINT [FK_tblMemberOrders_tblGroceryItems]
GO

ALTER TABLE [dbo].[tblMemberOrders]  WITH CHECK ADD  CONSTRAINT [FK_tblMemberOrders_tblMembers] FOREIGN KEY([MemberID])
REFERENCES [dbo].[tblMembers] ([MemberID])
GO

ALTER TABLE [dbo].[tblMemberOrders] CHECK CONSTRAINT [FK_tblMemberOrders_tblMembers]
GO

