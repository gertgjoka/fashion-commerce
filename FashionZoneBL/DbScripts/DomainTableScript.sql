USE [priv]
GO
/****** Object:  Table [dbo].[SHIPPING]    Script Date: 04/08/2012 11:27:16 ******/
SET IDENTITY_INSERT [dbo].[SHIPPING] ON
INSERT [dbo].[SHIPPING] ([ID], [ShippingType], [ShippingCost]) VALUES (1, N'In-Store Pickup', CAST(0.00 AS Decimal(6, 2)))
INSERT [dbo].[SHIPPING] ([ID], [ShippingType], [ShippingCost]) VALUES (2, N'Postal', CAST(5.00 AS Decimal(6, 2)))
INSERT [dbo].[SHIPPING] ([ID], [ShippingType], [ShippingCost]) VALUES (3, N'Carrier', CAST(5.00 AS Decimal(6, 2)))
SET IDENTITY_INSERT [dbo].[SHIPPING] OFF
/****** Object:  Table [dbo].[ROLE]    Script Date: 04/08/2012 11:27:16 ******/
SET IDENTITY_INSERT [dbo].[ROLE] ON
INSERT [dbo].[ROLE] ([ID], [Name], [Description]) VALUES (1, N'Administrator', N'Maximum power')
INSERT [dbo].[ROLE] ([ID], [Name], [Description]) VALUES (2, N'Moderator', N'Medium power')
INSERT [dbo].[ROLE] ([ID], [Name], [Description]) VALUES (3, N'User', N'Common user')
SET IDENTITY_INSERT [dbo].[ROLE] OFF

SET IDENTITY_INSERT [dbo].[D_ADDRESS_TYPE] ON
INSERT [dbo].[D_ADDRESS_TYPE] ([ID], [Name]) VALUES (1, N'Home')
INSERT [dbo].[D_ADDRESS_TYPE] ([ID], [Name]) VALUES (2, N'Office')
INSERT [dbo].[D_ADDRESS_TYPE] ([ID], [Name]) VALUES (3, N'Other')
SET IDENTITY_INSERT [dbo].[D_ADDRESS_TYPE] OFF
/****** Object:  Table [dbo].[D_STATE]    Script Date: 04/08/2012 11:27:16 ******/
SET IDENTITY_INSERT [dbo].[D_STATE] ON
INSERT [dbo].[D_STATE] ([ID], [Name], [Enabled]) VALUES (1, N'Shqiperi', 1)
INSERT [dbo].[D_STATE] ([ID], [Name], [Enabled]) VALUES (2, N'Kosove', 0)
INSERT [dbo].[D_STATE] ([ID], [Name], [Enabled]) VALUES (3, N'Maqedoni', 0)
INSERT [dbo].[D_STATE] ([ID], [Name], [Enabled]) VALUES (4, N'Mal i Zi', 0)
SET IDENTITY_INSERT [dbo].[D_STATE] OFF
/****** Object:  Table [dbo].[D_RETURN_MOTIVATION]    Script Date: 04/08/2012 11:27:16 ******/
SET IDENTITY_INSERT [dbo].[D_RETURN_MOTIVATION] ON
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (1, N'The item purchased is not what I had requested')
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (2, N'I have not received all the products I had bought')
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (3, N'My order arrived too late')
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (4, N'The product is disappointing compared to how it appeared on the site')
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (5, N'The size is too large / too small')
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (6, N'The product is defective')
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (7, N'The item arrived broken or in poor condition')
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (8, N'Other')
SET IDENTITY_INSERT [dbo].[D_RETURN_MOTIVATION] OFF
/****** Object:  Table [dbo].[D_ORDER_STATUS]    Script Date: 04/08/2012 11:27:16 ******/
INSERT [dbo].[D_ORDER_STATUS] ([ID], [Name]) VALUES (1, N'Open')
INSERT [dbo].[D_ORDER_STATUS] ([ID], [Name]) VALUES (2, N'Waiting Brand')
INSERT [dbo].[D_ORDER_STATUS] ([ID], [Name]) VALUES (3, N'In warehouse')
INSERT [dbo].[D_ORDER_STATUS] ([ID], [Name]) VALUES (4, N'On delivery')
INSERT [dbo].[D_ORDER_STATUS] ([ID], [Name]) VALUES (5, N'Delivered')

SET IDENTITY_INSERT [dbo].[D_CITY] ON
INSERT [dbo].[D_CITY] ([ID], [Name], [StateID]) VALUES (1, N'Tirane', 1)
INSERT [dbo].[D_CITY] ([ID], [Name], [StateID]) VALUES (2, N'Durres', 1)
INSERT [dbo].[D_CITY] ([ID], [Name], [StateID]) VALUES (3, N'Vlore', 1)
INSERT [dbo].[D_CITY] ([ID], [Name], [StateID]) VALUES (4, N'Shkoder', 1)
INSERT [dbo].[D_CITY] ([ID], [Name], [StateID]) VALUES (5, N'Korce', 1)
INSERT [dbo].[D_CITY] ([ID], [Name], [StateID]) VALUES (6, N'Gjirokaster', 1)
INSERT [dbo].[D_CITY] ([ID], [Name], [StateID]) VALUES (7, N'Elbasan', 1)
SET IDENTITY_INSERT [dbo].[D_CITY] OFF

SET IDENTITY_INSERT [dbo].[USER] ON
INSERT [dbo].[USER] ([ID], [Name], [Email], [Login], [Password], [RoleID], [Enabled], [LastIP]) VALUES (1, N'Gerti', N'gertgjoka@gmail.com', N'gerti', N'90795a0ffaa8b88c0e250546d8439bc9c31e5a5e', 1, 1, NULL)
INSERT [dbo].[USER] ([ID], [Name], [Email], [Login], [Password], [RoleID], [Enabled], [LastIP]) VALUES (2, N'Holti', N'holtiony@gmail.com', N'holti', N'90795a0ffaa8b88c0e250546d8439bc9c31e5a5e', 2, 1, NULL)
INSERT [dbo].[USER] ([ID], [Name], [Email], [Login], [Password], [RoleID], [Enabled], [LastIP]) VALUES (3, N'Ermal', N'ermal.aliraj@gmail.com', N'ermal', N'90795a0ffaa8b88c0e250546d8439bc9c31e5a5e', 3, 1, NULL)
INSERT [dbo].[USER] ([ID], [Name], [Email], [Login], [Password], [RoleID], [Enabled], [LastIP]) VALUES (4, N'Alda2', N'alda.hasani@gmail.com', N'alda', N'90da50a01224eea90d4fda090376bd42402c1757', 3, 1, NULL)
INSERT [dbo].[USER] ([ID], [Name], [Email], [Login], [Password], [RoleID], [Enabled], [LastIP]) VALUES (5, N'Aida Xhaxho', N'aida.xhaxho@gmail.com', N'aida', N'355276b52949740769b353fb1fcac58b9d37820d', 1, 1, NULL)
INSERT [dbo].[USER] ([ID], [Name], [Email], [Login], [Password], [RoleID], [Enabled], [LastIP]) VALUES (6, N'gdsa', N'gfds@fasd.com', N'gds', N'58e9d0e4808c7b43fdc17aa52cd1ed46d7012fc8', 1, 1, NULL)
SET IDENTITY_INSERT [dbo].[USER] OFF