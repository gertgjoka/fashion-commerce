USE [priv]
GO
SET IDENTITY_INSERT [dbo].[ROLE] ON 

GO
INSERT [dbo].[ROLE] ([ID], [Name], [Description]) VALUES (1, N'Administrator', N'Maximum power')
GO
INSERT [dbo].[ROLE] ([ID], [Name], [Description]) VALUES (2, N'Moderator', N'Medium power')
GO
INSERT [dbo].[ROLE] ([ID], [Name], [Description]) VALUES (3, N'User', N'Common user')
GO
SET IDENTITY_INSERT [dbo].[ROLE] OFF
GO
SET IDENTITY_INSERT [dbo].[USER] ON 

GO
INSERT [dbo].[USER] ([ID], [Name], [Email], [Login], [Password], [RoleID], [Enabled], [LastIP]) VALUES (1, N'Gerti Gjoka', N'gertgjoka@gmail.com', N'gerti', N'2206acc40e12985b2e1fbda0491d189e0cdd235b', 1, 1, NULL)
GO
INSERT [dbo].[USER] ([ID], [Name], [Email], [Login], [Password], [RoleID], [Enabled], [LastIP]) VALUES (7, N'Gjergj Martini', N'gj.martini@zonegroup.al', N'gjergji', N'862933d25ca9625eda9ea2d4a7ecb7b9ddf9c311', 1, 1, NULL)
GO
INSERT [dbo].[USER] ([ID], [Name], [Email], [Login], [Password], [RoleID], [Enabled], [LastIP]) VALUES (8, N'Ermal Aliraj', N'ermal.aliraj@gmail.com', N'ermal', N'bf4a40f31bdd17753fd294260943325e82a140e0', 1, 1, NULL)
GO
INSERT [dbo].[USER] ([ID], [Name], [Email], [Login], [Password], [RoleID], [Enabled], [LastIP]) VALUES (9, N'Holti Bitri', N'holtiony@gmail.com', N'holti', N'28251d9337bba76c66baf13f34fd564a5bddce85', 1, 1, NULL)
GO
INSERT [dbo].[USER] ([ID], [Name], [Email], [Login], [Password], [RoleID], [Enabled], [LastIP]) VALUES (10, N'Entiljano Shehaj', N'tili_shehaj@yahoo.it', N'tili', N'b4f3b802a0b881c06c24a087ea661ea4a68dcf07', 1, 1, NULL)
GO
INSERT [dbo].[USER] ([ID], [Name], [Email], [Login], [Password], [RoleID], [Enabled], [LastIP]) VALUES (12, N'Romina Nezha', N'r.nezha@zonegroup.al', N'romina', N'1c815754ee45f755330c769d7e7a916e9102ce39', 1, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[USER] OFF
GO
SET IDENTITY_INSERT [dbo].[BRAND] ON 

GO
INSERT [dbo].[BRAND] ([ID], [Name], [ShowName], [Description], [Contact], [Telephone], [Website], [Email], [Shop], [Address]) VALUES (16, N'Pepe Jeans', N'Pepe Jeans', N'Pepe Jeans', N'Simone Panfilo', N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BRAND] ([ID], [Name], [ShowName], [Description], [Contact], [Telephone], [Website], [Email], [Shop], [Address]) VALUES (17, N'Pin-Up Stars', N'Pin-Up Stars', N'Pin-Up Stars', N'Simone Panfilo', N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BRAND] ([ID], [Name], [ShowName], [Description], [Contact], [Telephone], [Website], [Email], [Shop], [Address]) VALUES (18, N'Valentino', N'Valentino', N'Valentino', N'Italia Fashion Biz', N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BRAND] ([ID], [Name], [ShowName], [Description], [Contact], [Telephone], [Website], [Email], [Shop], [Address]) VALUES (19, N'Gianfranco Ferre', N'Gianfranco Ferre', N'Gianfranco Ferre', N'Italian Fashion Biz', N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BRAND] ([ID], [Name], [ShowName], [Description], [Contact], [Telephone], [Website], [Email], [Shop], [Address]) VALUES (20, N'Fornarina', N'Fornarina', N'Brand Fornarina', N'Alda / Aida', N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BRAND] ([ID], [Name], [ShowName], [Description], [Contact], [Telephone], [Website], [Email], [Shop], [Address]) VALUES (21, N'Benetton & Playlife', N'Benetton & Playlife', N'Benetton & Playlife', N'Alda / Aida', N'', N'', N'', 1, N'Benetton Tirane')
GO
INSERT [dbo].[BRAND] ([ID], [Name], [ShowName], [Description], [Contact], [Telephone], [Website], [Email], [Shop], [Address]) VALUES (22, N'Mario Valentino & Krizia', N'Mario Valentino & Krizia', N'Valentino & Krizia', N'Alda / Aida', N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BRAND] ([ID], [Name], [ShowName], [Description], [Contact], [Telephone], [Website], [Email], [Shop], [Address]) VALUES (23, N'Boxeur Des Rues & Motivi', N'Boxeur Des Rues & Motivi', N'Boxeur Des Rues & Motivi', N'Alda / Aida', N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BRAND] ([ID], [Name], [ShowName], [Description], [Contact], [Telephone], [Website], [Email], [Shop], [Address]) VALUES (24, N'Calzedonia & Intimissimi', N'Calzedonia & Intimissimi', N'Calzedonia', N'Alda / Aida', N'', N'', N'', NULL, NULL)
GO
INSERT [dbo].[BRAND] ([ID], [Name], [ShowName], [Description], [Contact], [Telephone], [Website], [Email], [Shop], [Address]) VALUES (25, N'Diesel', N'Diesel', N'diesel', N'kontakt', N'', N'', N'', 0, N'')
GO
INSERT [dbo].[BRAND] ([ID], [Name], [ShowName], [Description], [Contact], [Telephone], [Website], [Email], [Shop], [Address]) VALUES (26, N'La Martina', N'La Martina', N'la martina', N'Adnan', N'', N'', N'', 0, N'')
GO
SET IDENTITY_INSERT [dbo].[BRAND] OFF
GO
SET IDENTITY_INSERT [dbo].[CAMPAIGN] ON 

GO
INSERT [dbo].[CAMPAIGN] ([ID], [BrandID], [Name], [Description], [StartDate], [EndDate], [DeliveryStartDate], [DeliveryEndDate], [Logo], [ImageHome], [ImageDetail], [ImageListHeader], [Active], [GenericFile], [Approved], [ApprovedBy]) VALUES (12, 16, N'Pepe Jeans', N'Pepe Jeans fustani', CAST(0x0000A06700000000 AS DateTime), CAST(0x0000A0950062E080 AS DateTime), CAST(0xE7350B00 AS Date), CAST(0xEE350B00 AS Date), N'8e57f2ab-eb75-4491-8143-1e23a61ffe64__logo_pepe_jeans.png', N'5d85735d-69b5-4252-8784-dbf935f7e60a__home_newsletter.jpg', N'4a1e16a3-a6d1-47bb-bc60-9d3cf3ccc574__pepe-jeans-london-campaign.jpg', N'6a602c52-1de9-49e6-aff8-26a200f2ef72__pepe-jeans-london-campaign_menu_image.jpg', 0, NULL, 1, 7)
GO
INSERT [dbo].[CAMPAIGN] ([ID], [BrandID], [Name], [Description], [StartDate], [EndDate], [DeliveryStartDate], [DeliveryEndDate], [Logo], [ImageHome], [ImageDetail], [ImageListHeader], [Active], [GenericFile], [Approved], [ApprovedBy]) VALUES (13, 17, N'Pin-Up Stars', N'Pin-Up Stars', CAST(0x0000A06700000000 AS DateTime), CAST(0x0000A0950062E080 AS DateTime), CAST(0xEE350B00 AS Date), CAST(0xDB350B00 AS Date), N'2eb9a1cb-5805-45a2-ada9-f5f8497c545e__logopinup.png', N'3bd1a01e-6b5f-42d7-86f1-4d74e26b4dc2__pinup_stars_home_newsletter.jpg', N'b4de81b4-af7c-4226-b9f9-0f2173caa1c7__pin-upstars_campaign OK.jpg', N'fbf2f903-8e71-4830-96d3-78e7864f2bf9__pin-upstars_campaign_menu_img.jpg', 0, NULL, 1, 7)
GO
INSERT [dbo].[CAMPAIGN] ([ID], [BrandID], [Name], [Description], [StartDate], [EndDate], [DeliveryStartDate], [DeliveryEndDate], [Logo], [ImageHome], [ImageDetail], [ImageListHeader], [Active], [GenericFile], [Approved], [ApprovedBy]) VALUES (15, 19, N'Gianfranco Ferre', N'Gianfranco Ferre', CAST(0x0000A0F200735B40 AS DateTime), CAST(0x0000A10A0062E080 AS DateTime), CAST(0x4D360B00 AS Date), CAST(0x65360B00 AS Date), N'1f153be2-c5a8-42ad-ab7b-2514d27d4e67__giafranco-ferre_logo.png', N'bebdd924-7b5b-4e92-a51e-2264ae9d34c0__gf_home_newsletter.jpg', N'16ed3b55-27e2-4d19-a89d-8379ce7ee371__gf_background_detail.jpg', N'f20c5e8c-6091-4463-a121-e98218c87dd7__gf_campaign_menu_img.jpg', 1, NULL, 1, 7)
GO
INSERT [dbo].[CAMPAIGN] ([ID], [BrandID], [Name], [Description], [StartDate], [EndDate], [DeliveryStartDate], [DeliveryEndDate], [Logo], [ImageHome], [ImageDetail], [ImageListHeader], [Active], [GenericFile], [Approved], [ApprovedBy]) VALUES (17, 18, N'Valentino', N'Valentino', CAST(0x0000A07100000000 AS DateTime), CAST(0x0000A0950062E080 AS DateTime), CAST(0xE7350B00 AS Date), CAST(0xEE350B00 AS Date), N'e1f06f32-8c33-446f-ba2d-1ea4f2c2084b__logo_Valentino.png', N'9886ec33-783e-4990-b959-5f38bc984fb2__valentino_home_newsletter_polo.jpg', N'e8448f68-ae26-4323-b061-3746f0afb6ac__campaign_valentino_polo_1024x768.jpg', N'373c2025-b096-43c8-b0e5-0f95386f46c1__img_menu_valentino_polo_1024x768.jpg', 0, NULL, 1, 1)
GO
INSERT [dbo].[CAMPAIGN] ([ID], [BrandID], [Name], [Description], [StartDate], [EndDate], [DeliveryStartDate], [DeliveryEndDate], [Logo], [ImageHome], [ImageDetail], [ImageListHeader], [Active], [GenericFile], [Approved], [ApprovedBy]) VALUES (18, 18, N'Valentino Second', N'Fushata e dyte Valentino me kemishat dhe polot', CAST(0x0000A0D400735B40 AS DateTime), CAST(0x0000A0EC0062E080 AS DateTime), CAST(0x2F360B00 AS Date), CAST(0x28360B00 AS Date), N'5f36b71c-5e0d-441a-96ca-87e9f2491320__logo_Valentino.png', N'154d251b-63ca-4e1f-a04a-81389a875007__valentino_home_newsletter.jpg', N'dc300118-7b59-4ff0-8b14-5051b1492e59__valentino_campaign.jpg', N'd198f469-81b1-4cdd-85a6-b13283b12ad8__valentino_campaign_menu_image.jpg', 0, NULL, 1, NULL)
GO
INSERT [dbo].[CAMPAIGN] ([ID], [BrandID], [Name], [Description], [StartDate], [EndDate], [DeliveryStartDate], [DeliveryEndDate], [Logo], [ImageHome], [ImageDetail], [ImageListHeader], [Active], [GenericFile], [Approved], [ApprovedBy]) VALUES (20, 17, N'Pin-Up Stars Second', N'Pin-Up Stars sales', CAST(0x0000A0950062E080 AS DateTime), CAST(0x0000A0D20062E080 AS DateTime), CAST(0xF0350B00 AS Date), CAST(0x2D360B00 AS Date), N'2eb9a1cb-5805-45a2-ada9-f5f8497c545e__logopinup.png', N'c630f8f7-ae36-40ec-af7c-62990d22ffb8__re_pinup_stars_home_newsletter.jpg', N'b4de81b4-af7c-4226-b9f9-0f2173caa1c7__pin-upstars_campaign OK.jpg', N'fbf2f903-8e71-4830-96d3-78e7864f2bf9__pin-upstars_campaign_menu_img.jpg', 0, NULL, 1, NULL)
GO
INSERT [dbo].[CAMPAIGN] ([ID], [BrandID], [Name], [Description], [StartDate], [EndDate], [DeliveryStartDate], [DeliveryEndDate], [Logo], [ImageHome], [ImageDetail], [ImageListHeader], [Active], [GenericFile], [Approved], [ApprovedBy]) VALUES (21, 16, N'Pepe Jeans Second', N'Pepe Jeans fustani', CAST(0x0000A0EB0062E080 AS DateTime), CAST(0x0000A0FE0062E080 AS DateTime), CAST(0x46360B00 AS Date), CAST(0x59360B00 AS Date), N'8e57f2ab-eb75-4491-8143-1e23a61ffe64__logo_pepe_jeans.png', N'55d559a0-a62b-4ace-bc3a-efa08e509968__re_home_newsletter.jpg', N'4a1e16a3-a6d1-47bb-bc60-9d3cf3ccc574__pepe-jeans-london-campaign.jpg', N'6a602c52-1de9-49e6-aff8-26a200f2ef72__pepe-jeans-london-campaign_menu_image.jpg', 0, NULL, 1, NULL)
GO
INSERT [dbo].[CAMPAIGN] ([ID], [BrandID], [Name], [Description], [StartDate], [EndDate], [DeliveryStartDate], [DeliveryEndDate], [Logo], [ImageHome], [ImageDetail], [ImageListHeader], [Active], [GenericFile], [Approved], [ApprovedBy]) VALUES (22, 20, N'Fornarina Summer Collection', N'Fornarina summer collection', CAST(0x0000A0E800735B40 AS DateTime), CAST(0x0000A1050062E080 AS DateTime), CAST(0x43360B00 AS Date), CAST(0x60360B00 AS Date), N'816e5d3c-69f1-49de-a732-7303bfcc7e11__fornarina_logo.png', N'9267758c-133e-4422-9ba2-57e8fffacdeb__fornarina_home_newsletter.jpg', N'343f7ae4-a464-4948-8f7a-4c2165132662__fornarina_campaign.jpg', N'988bb069-4abd-428e-ace7-992dc1aa309e__fornarina_campaign_menu_image.jpg', 1, NULL, 1, 1)
GO
INSERT [dbo].[CAMPAIGN] ([ID], [BrandID], [Name], [Description], [StartDate], [EndDate], [DeliveryStartDate], [DeliveryEndDate], [Logo], [ImageHome], [ImageDetail], [ImageListHeader], [Active], [GenericFile], [Approved], [ApprovedBy]) VALUES (23, 21, N'Benetton & Playlife Summer', N'Benetton & Playlife', CAST(0x0000A0D900735B40 AS DateTime), CAST(0x0000A14B0062E080 AS DateTime), CAST(0x34360B00 AS Date), CAST(0x4A360B00 AS Date), N'b2a40ff9-e43c-4897-b28a-8d0c620ff5f5__logo_benetton_playlife.png', N'f33200af-13c1-4571-8cdd-8e4341b177fa__newsletter_benetton_playlife.jpg', N'5826efd5-63c6-4f0e-9c73-befa408b64de__foto_1024x768.jpg', N'8a6c64dc-3534-4924-a2a0-469f362dd5aa__230x173.jpg', 1, NULL, 1, 1)
GO
INSERT [dbo].[CAMPAIGN] ([ID], [BrandID], [Name], [Description], [StartDate], [EndDate], [DeliveryStartDate], [DeliveryEndDate], [Logo], [ImageHome], [ImageDetail], [ImageListHeader], [Active], [GenericFile], [Approved], [ApprovedBy]) VALUES (24, 22, N'Mario Valentino & Krizia Aksesore', N'Valentino & Krizia', CAST(0x0000A0E100735B40 AS DateTime), CAST(0x0000A0F200735B40 AS DateTime), CAST(0x28360B00 AS Date), CAST(0x38360B00 AS Date), N'88e54c9e-7edc-465b-8613-ce0e0645822d__krizia_valentino_logo_230x100.png', N'7de6a3db-3351-4f15-b467-cd1e5d1e82d9__newsletter.jpg', N'0db2d875-d7c1-4671-b71e-8f97a5145e36__main_campaign.jpg', N'46f9c965-296f-4cd1-98c8-d997a3519937__main_campaign230.jpg', 0, NULL, 1, 1)
GO
INSERT [dbo].[CAMPAIGN] ([ID], [BrandID], [Name], [Description], [StartDate], [EndDate], [DeliveryStartDate], [DeliveryEndDate], [Logo], [ImageHome], [ImageDetail], [ImageListHeader], [Active], [GenericFile], [Approved], [ApprovedBy]) VALUES (25, 23, N'Boxeur Des Rues & Motivi', N'Boxeur Des Rues & Motivi', CAST(0x0000A0EB00735B40 AS DateTime), CAST(0x0000A10500735B40 AS DateTime), CAST(0x28360B00 AS Date), CAST(0x37360B00 AS Date), N'9e0b6457-a9d6-4ac9-aaa9-b101fac065a1__logo_230x100.png', N'9812a267-932a-4d03-a146-4771679e735e__foto_main_newsletter.jpg', N'11f4cbbf-4bb8-4f4b-9022-b0304c28c2f3__foto_1024x768.jpg', N'ebfe945a-4f3d-4d98-a3f1-47ba5176d8af__foto_230x173.jpg', 1, NULL, 1, 1)
GO
INSERT [dbo].[CAMPAIGN] ([ID], [BrandID], [Name], [Description], [StartDate], [EndDate], [DeliveryStartDate], [DeliveryEndDate], [Logo], [ImageHome], [ImageDetail], [ImageListHeader], [Active], [GenericFile], [Approved], [ApprovedBy]) VALUES (26, 24, N'Calzedonia Summer', N'Calzedonia Summer', CAST(0x0000A0ED00735B40 AS DateTime), CAST(0x0000A0FB0062E080 AS DateTime), CAST(0x48360B00 AS Date), CAST(0x56360B00 AS Date), N'87c0b599-3422-47e7-9ab0-dfbd8dd2f467__logo_calzedonia_230x100.png', N'e11bfd1b-daca-49d8-80bc-a844a13a68f1__foto_main_newsletter.jpg', N'99342420-98e7-49d8-b112-2e2271537541__foto_1024x768.jpg', N'8ebc9657-4fff-4a50-a7d7-9255e42bb5ed__foto_230x173.jpg', 0, NULL, 1, 1)
GO
INSERT [dbo].[CAMPAIGN] ([ID], [BrandID], [Name], [Description], [StartDate], [EndDate], [DeliveryStartDate], [DeliveryEndDate], [Logo], [ImageHome], [ImageDetail], [ImageListHeader], [Active], [GenericFile], [Approved], [ApprovedBy]) VALUES (27, 18, N'Valentino Third', N'Third publication with sales', CAST(0x0000A0F900735B40 AS DateTime), CAST(0x0000A1130062E080 AS DateTime), CAST(0x54360B00 AS Date), CAST(0x6E360B00 AS Date), N'5f36b71c-5e0d-441a-96ca-87e9f2491320__logo_Valentino.png', N'3a7d0c8b-2fa4-45de-8447-94bec313ffb9__valentino_home_newsletterspeciale.png', N'dc300118-7b59-4ff0-8b14-5051b1492e59__valentino_campaign.jpg', N'd198f469-81b1-4cdd-85a6-b13283b12ad8__valentino_campaign_menu_image.jpg', 1, NULL, 1, 1)
GO
INSERT [dbo].[CAMPAIGN] ([ID], [BrandID], [Name], [Description], [StartDate], [EndDate], [DeliveryStartDate], [DeliveryEndDate], [Logo], [ImageHome], [ImageDetail], [ImageListHeader], [Active], [GenericFile], [Approved], [ApprovedBy]) VALUES (31, 25, N'Diesel', N'diesel', CAST(0x0000A17500735B40 AS DateTime), CAST(0x0000A18A0062E080 AS DateTime), CAST(0xD0360B00 AS Date), CAST(0xDE360B00 AS Date), NULL, NULL, NULL, NULL, 0, NULL, 0, NULL)
GO
INSERT [dbo].[CAMPAIGN] ([ID], [BrandID], [Name], [Description], [StartDate], [EndDate], [DeliveryStartDate], [DeliveryEndDate], [Logo], [ImageHome], [ImageDetail], [ImageListHeader], [Active], [GenericFile], [Approved], [ApprovedBy]) VALUES (32, 26, N'La Martina', N'la martina', CAST(0x0000A17600735B40 AS DateTime), CAST(0x0000A17E0062E080 AS DateTime), CAST(0xD1360B00 AS Date), CAST(0xD9360B00 AS Date), NULL, NULL, NULL, NULL, 0, NULL, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[CAMPAIGN] OFF
GO
SET IDENTITY_INSERT [dbo].[D_ATTRIBUTE] ON 

GO
INSERT [dbo].[D_ATTRIBUTE] ([ID], [Name], [Description]) VALUES (15, N'Masa', N'Masa SML')
GO
INSERT [dbo].[D_ATTRIBUTE] ([ID], [Name], [Description]) VALUES (16, N'Masa Bikini', N'Masa bikini')
GO
INSERT [dbo].[D_ATTRIBUTE] ([ID], [Name], [Description]) VALUES (17, N'Masa Kemisha', N'')
GO
INSERT [dbo].[D_ATTRIBUTE] ([ID], [Name], [Description]) VALUES (18, N'Masa kepuce femra', N'masa kepuces per femra')
GO
INSERT [dbo].[D_ATTRIBUTE] ([ID], [Name], [Description]) VALUES (19, N'Masa pantallona femra', N'masa pantallona')
GO
INSERT [dbo].[D_ATTRIBUTE] ([ID], [Name], [Description]) VALUES (20, N'Mase Unike', N'mase unike')
GO
SET IDENTITY_INSERT [dbo].[D_ATTRIBUTE] OFF
GO
SET IDENTITY_INSERT [dbo].[D_ATTRIBUTE_VALUE] ON 

GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (51, 15, N'S', 1)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (52, 15, N'M', 2)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (53, 15, N'L', 3)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (54, 16, N'1', 1)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (55, 16, N'2', 2)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (56, 16, N'3', 3)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (57, 15, N'XL', 4)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (58, 17, N'37', 1)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (59, 17, N'38', 2)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (60, 17, N'39', 3)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (61, 17, N'40', 4)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (62, 17, N'41', 5)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (63, 17, N'42', 6)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (64, 17, N'43', 7)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (65, 18, N'35', 1)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (66, 18, N'36', 2)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (67, 18, N'37', 3)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (68, 18, N'38', 4)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (69, 18, N'39', 5)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (70, 18, N'40', 6)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (71, 19, N'25', 1)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (72, 19, N'26', 2)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (73, 19, N'27', 3)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (74, 19, N'28', 4)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (75, 19, N'29', 5)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (76, 19, N'30', 6)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (77, 15, N'XS', 0)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (78, 20, N'Mase unike', 1)
GO
INSERT [dbo].[D_ATTRIBUTE_VALUE] ([ID], [AttributeID], [Value], [ShowOrder]) VALUES (79, 15, N'XXL', 6)
GO
SET IDENTITY_INSERT [dbo].[D_ATTRIBUTE_VALUE] OFF
GO
SET IDENTITY_INSERT [dbo].[CATEGORY] ON 

GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (38, 12, N'Femra', N'Femra', NULL, 15, 1, N'Woman')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (39, 12, N'Fustane', N'Fustane', 38, 15, 1, N'Dresses')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (40, 13, N'Femra', N'Femra Pin-up Stars', NULL, 16, 1, N'Woman')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (41, 13, N'Rrobebanjo', N'Rrobebanjo Pin-up Stars', 40, 16, 1, N'Swimwear')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (46, 17, N'Meshkuj', N'Meshkuj', NULL, 15, 1, N'Man')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (47, 17, N'Polo', N'Polo', 46, 15, 1, N'Polo')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (48, 18, N'Meshkuj', N'Kategoria meshkuj', NULL, NULL, 1, N'Man')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (49, 18, N'Kemisha', N'Kemisha', 48, 17, 1, N'Shirts')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (50, 18, N'Polo', N'Polo', 48, 15, 2, N'Polo')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (51, 20, N'Femra', N'Femra Pin-up Stars', NULL, 16, 1, N'Woman')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (52, 20, N'Rrobebanjo', N'Rrobebanjo Pin-up Stars', 51, 16, 1, N'Swimwear')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (53, 21, N'Femra', N'Femra', NULL, 15, 1, N'Woman')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (54, 21, N'Fustane', N'Fustane', 53, 15, 1, N'Dresses')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (55, 22, N'Femra', N'kategori femra fornarina', NULL, NULL, 1, N'Woman')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (56, 22, N'Fustane', N'fustane', 55, NULL, 0, N'Dresses')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (57, 22, N'Kemisha', N'kemisha', 55, 15, 2, N'Shirts')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (58, 22, N'Funde, Minifunde & Shorts', N'funde', 55, 15, 3, N'Skirts & Shorts')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (59, 22, N'Top & Kanatiere', N'bust dhe kanatiere', 55, 15, 4, N'Singlet')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (60, 22, N'Pantallona & Jeans', N'Pantallona & Xhinse', 55, 15, 5, N'Pants & Jeans ')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (61, 22, N'Aksesore', N'Kapele, portofola, varese, vathe, byrzylyk', 55, NULL, 7, N'Accessories')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (62, 22, N'Kepuce', N'kepuce,atlete, balerina, mokasini', 55, NULL, 6, N'Shoes')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (64, 22, N'T-shirt', N'T-shirt', 55, NULL, 1, N'T-shirt')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (65, 23, N'Femra', N'Femra', NULL, NULL, 1, N'Woman')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (66, 23, N'Meshkuj', N'meshkuj', NULL, NULL, 2, N'Man')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (67, 23, N'T-shirt', N'T-shirt', 65, 15, 1, N'T-shirt')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (68, 23, N'Fustane', N'Fustane', 65, 15, 2, N'Dresses')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (70, 23, N'Funde', N'Funde', 65, 15, 3, N'Skirts')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (71, 23, N'Pantallona', N'Pantallona', 65, 15, 4, N'Shorts')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (72, 23, N'Kanatiere', N'kanatiere', 65, 15, 5, N'Singlets')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (73, 23, N'T-shirt', N'T-shirt', 66, 15, 1, N'T-shirt')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (74, 24, N'Femra', N'Femra', NULL, 20, 1, N'Woman')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (77, 25, N'Femra', N'Woman', NULL, 15, 1, N'Woman')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (79, 25, N'Tutina Boxeur Des Rues', N'Onesie', 77, 15, 1, N'Onesie')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (80, 25, N'Fustane te gjate Motivi', N'Futane te gjat', 77, 15, 2, N'Long dresses')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (81, 25, N'Pantallona te shkurtra Motivi', N'Pantallona te shkurtra
', 77, 19, 3, N'Shorts')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (82, 25, N'Kominoshe te zeza Motivi', N'Kominoshe te zeza', 77, 19, 4, N'Coveralls')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (83, 25, N'Kanatjere te zeza Motivi', N'Kanatjere', 77, 19, 5, N'Singlet')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (84, 25, N'Sandale Motivi', N'Sandale te sheshta', 77, 18, 6, N'Sandals')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (85, 25, N'Aksesore Motivi', N'Aksesore', 77, 20, 7, N'Motivi accessories')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (86, 25, N'Canta Motivi', N'Canta', 77, 20, 8, N'Motivi handbags')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (87, 26, N'Femra', N'Femra', NULL, NULL, 1, N'Woman')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (88, 26, N'Recipeta', N'Bra', 87, 16, 2, N'Bras')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (89, 26, N'Mbathje', N'Mbathje', 87, 16, 2, N'Drawers')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (90, 26, N'Kanatjere', N'Kanatjere', 87, 15, 3, N'Singlets')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (91, 26, N'Fustan', N'Fustan', 87, 15, 4, N'Dresses')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (92, 26, N'Jeleke', N'Jeleke', 87, 15, 5, N'Vests')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (93, 23, N'Bluza Dimerore', N'Bluza Dimerore', 65, 15, 2, N'Winter Shirts')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (94, 24, N'Pasqyra M. Valentino', N'Pasqyra M. Valentino', 74, 20, 1, N'M. Valentino Mirrors')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (95, 24, N'Portofol Krizia', N'Portofol Krizia', 74, 20, 2, N'Krizia Wallets')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (96, 15, N'Meshkuj', N'Meshkuj', NULL, 20, 1, N'Man')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (97, 15, N'Rripa', N'Rripa', 96, 20, 1, N'Belts')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (98, 27, N'Meshkuj', N'Kategoria meshkuj', NULL, NULL, 1, N'Man')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (99, 27, N'Kemisha', N'Kemisha', 98, 17, 1, N'Shirts')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (100, 27, N'Polo', N'Polo', 98, 15, 2, N'Polo')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (101, 31, N'Femra', N'woman', NULL, 15, 1, N'Woman')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (102, 31, N'Meshkuj', N'man', NULL, 15, 2, N'Man')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (103, 31, N'Bluza', N'hoody', 101, 15, 1, N'Hoody')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (104, 31, N'Bluza', N'Hoody', 102, 15, 1, N'Hoody')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (105, 31, N'Felpa', N'da', 102, 15, 2, N'Hoody2')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (106, 32, N'Meshkuj', N'man', NULL, 15, 1, N'Man')
GO
INSERT [dbo].[CATEGORY] ([ID], [CampaignID], [Name], [Description], [ParentID], [AttributeID], [Ordering], [NameEng]) VALUES (107, 32, N'Polo', N'Polo', 106, 15, 1, N'Polo')
GO
SET IDENTITY_INSERT [dbo].[CATEGORY] OFF
GO
SET IDENTITY_INSERT [dbo].[D_STATE] ON 

GO
INSERT [dbo].[D_STATE] ([ID], [Name], [Enabled]) VALUES (1, N'Shqiperi', 1)
GO
INSERT [dbo].[D_STATE] ([ID], [Name], [Enabled]) VALUES (2, N'Kosove', 0)
GO
INSERT [dbo].[D_STATE] ([ID], [Name], [Enabled]) VALUES (3, N'Maqedoni', 0)
GO
INSERT [dbo].[D_STATE] ([ID], [Name], [Enabled]) VALUES (4, N'Mal i Zi', 0)
GO
SET IDENTITY_INSERT [dbo].[D_STATE] OFF
GO
SET IDENTITY_INSERT [dbo].[D_CITY] ON 

GO
INSERT [dbo].[D_CITY] ([ID], [Name], [StateID]) VALUES (1, N'Tirane', 1)
GO
SET IDENTITY_INSERT [dbo].[D_CITY] OFF
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x3D360B00 AS Date), CAST(139.53 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x3E360B00 AS Date), CAST(139.53 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x3F360B00 AS Date), CAST(139.09 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x40360B00 AS Date), CAST(140.04 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x41360B00 AS Date), CAST(139.55 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x42360B00 AS Date), CAST(139.45 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x43360B00 AS Date), CAST(139.71 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x44360B00 AS Date), CAST(139.77 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x45360B00 AS Date), CAST(139.77 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x46360B00 AS Date), CAST(139.21 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x47360B00 AS Date), CAST(139.73 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x48360B00 AS Date), CAST(140.40 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x49360B00 AS Date), CAST(139.49 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x4A360B00 AS Date), CAST(139.18 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x4B360B00 AS Date), CAST(138.85 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x4C360B00 AS Date), CAST(138.85 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x4D360B00 AS Date), CAST(139.11 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x4E360B00 AS Date), CAST(139.47 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x4F360B00 AS Date), CAST(139.83 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x50360B00 AS Date), CAST(139.94 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x51360B00 AS Date), CAST(139.44 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x52360B00 AS Date), CAST(139.68 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x53360B00 AS Date), CAST(139.68 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x54360B00 AS Date), CAST(139.57 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x55360B00 AS Date), CAST(139.55 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x56360B00 AS Date), CAST(139.74 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x57360B00 AS Date), CAST(139.69 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x58360B00 AS Date), CAST(140.10 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x59360B00 AS Date), CAST(139.84 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x5A360B00 AS Date), CAST(139.84 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x5B360B00 AS Date), CAST(139.79 AS Decimal(18, 2)))
GO
INSERT [dbo].[CURRENCY] ([Date], [CurrencyRate]) VALUES (CAST(0x5C360B00 AS Date), CAST(139.91 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[D_ADDRESS_TYPE] ON 

GO
INSERT [dbo].[D_ADDRESS_TYPE] ([ID], [Name]) VALUES (1, N'Shtepi')
GO
INSERT [dbo].[D_ADDRESS_TYPE] ([ID], [Name]) VALUES (2, N'Zyre')
GO
INSERT [dbo].[D_ADDRESS_TYPE] ([ID], [Name]) VALUES (3, N'Tjeter')
GO
SET IDENTITY_INSERT [dbo].[D_ADDRESS_TYPE] OFF
GO
INSERT [dbo].[D_ORDER_STATUS] ([ID], [Name]) VALUES (1, N'I hapur')
GO
INSERT [dbo].[D_ORDER_STATUS] ([ID], [Name]) VALUES (2, N'Ne pritje')
GO
INSERT [dbo].[D_ORDER_STATUS] ([ID], [Name]) VALUES (3, N'Ne magazine')
GO
INSERT [dbo].[D_ORDER_STATUS] ([ID], [Name]) VALUES (4, N'Tek korrieri')
GO
INSERT [dbo].[D_ORDER_STATUS] ([ID], [Name]) VALUES (5, N'I dorezuar')
GO
INSERT [dbo].[D_ORDER_STATUS] ([ID], [Name]) VALUES (6, N'I Anulluar')
GO
SET IDENTITY_INSERT [dbo].[D_RETURN_MOTIVATION] ON 

GO
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (1, N'The item purchased is not what I had requested')
GO
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (2, N'I have not received all the products I had bought')
GO
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (3, N'My order arrived too late')
GO
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (4, N'The product is disappointing compared to how it appeared on the site')
GO
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (5, N'The size is too large / too small')
GO
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (6, N'The product is defective')
GO
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (7, N'The item arrived broken or in poor condition')
GO
INSERT [dbo].[D_RETURN_MOTIVATION] ([ID], [Name]) VALUES (8, N'Other')
GO
SET IDENTITY_INSERT [dbo].[D_RETURN_MOTIVATION] OFF
GO
