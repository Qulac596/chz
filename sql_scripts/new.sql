USE [NewDb]
GO
/****** Object:  Table [dbo].[chz_addresses]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_addresses](
	[chz_address_id] [int] IDENTITY(1,1) NOT NULL,
	[sys_id] [nvarchar](max) NULL,
	[text] [nvarchar](max) NULL,
	[chz_company_id] [int] NULL,
 CONSTRAINT [PK_dbo.chz_addresses] PRIMARY KEY CLUSTERED 
(
	[chz_address_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chz_companies]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_companies](
	[chz_company_id] [int] IDENTITY(1,1) NOT NULL,
	[inn] [nvarchar](max) NULL,
	[name] [nvarchar](max) NULL,
	[sys_id] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.chz_companies] PRIMARY KEY CLUSTERED 
(
	[chz_company_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chz_contract_types]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_contract_types](
	[chz_contract_type_id] [int] IDENTITY(1,1) NOT NULL,
	[value] [nvarchar](max) NOT NULL,
	[is_default] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.chz_contract_types] PRIMARY KEY CLUSTERED 
(
	[chz_contract_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chz_nakl_item_statuses]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_nakl_item_statuses](
	[chz_nakl_item_status_id] [int] IDENTITY(1,1) NOT NULL,
	[value] [nvarchar](max) NOT NULL,
	[style] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_chz_nakl_item_statuses] PRIMARY KEY CLUSTERED 
(
	[chz_nakl_item_status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chz_nakl_items]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_nakl_items](
	[chz_nakl_item_id] [int] IDENTITY(1,1) NOT NULL,
	[gtin] [nvarchar](max) NOT NULL,
	[cost] [decimal](18, 2) NULL,
	[vat_value] [decimal](18, 2) NULL,
	[chz_nakl_id] [int] NULL,
	[name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.chz_nakl_items] PRIMARY KEY CLUSTERED 
(
	[chz_nakl_item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chz_nakl_statuses]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_nakl_statuses](
	[chz_nakl_status_id] [int] IDENTITY(1,1) NOT NULL,
	[value] [nvarchar](max) NOT NULL,
	[class_name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.chz_nakl_statuses] PRIMARY KEY CLUSTERED 
(
	[chz_nakl_status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chz_nakls]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_nakls](
	[chz_nakl_id] [int] IDENTITY(1,1) NOT NULL,
	[operation_data] [datetime] NULL,
	[doc_num] [nvarchar](max) NULL,
	[doc_data] [datetime] NULL,
	[contract_num] [nvarchar](max) NULL,
	[chz_nakls__list_of_accept_type_id] [int] NULL,
	[chz_contract_type_id] [int] NULL,
	[chz_nakl_status_id] [int] NULL,
	[provider_chz_address_id] [int] NULL,
	[receiver_chz_address_id] [int] NULL,
	[chz_source_type_id] [int] NULL,
	[chz_turnover_tupe_id] [int] NULL,
	[chz_receive_type_id] [int] NULL,
	[is_trust] [bit] NULL,
 CONSTRAINT [PK_dbo.chz_nakls] PRIMARY KEY CLUSTERED 
(
	[chz_nakl_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chz_nakls__list_of_accept_types]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_nakls__list_of_accept_types](
	[chz_nakls__list_of_accept_type_id] [int] IDENTITY(1,1) NOT NULL,
	[value] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.chz_accept_types] PRIMARY KEY CLUSTERED 
(
	[chz_nakls__list_of_accept_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chz_nakls__list_of_nds_values]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_nakls__list_of_nds_values](
	[chz_nds_value_id] [int] IDENTITY(1,1) NOT NULL,
	[value] [int] NULL,
	[is_default] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.chz_nds_values] PRIMARY KEY CLUSTERED 
(
	[chz_nds_value_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chz_receive_types]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_receive_types](
	[chz_receive_type_id] [int] IDENTITY(1,1) NOT NULL,
	[value] [nvarchar](max) NULL,
	[is_default] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.chz_receive_types] PRIMARY KEY CLUSTERED 
(
	[chz_receive_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chz_scaned_sgtin_statuses]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_scaned_sgtin_statuses](
	[chz_scaned_sgtin_status_id] [int] IDENTITY(1,1) NOT NULL,
	[value] [nvarchar](max) NOT NULL,
	[style] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_chz_scaned_sgtin_statuses] PRIMARY KEY CLUSTERED 
(
	[chz_scaned_sgtin_status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chz_scanned_sgtins]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_scanned_sgtins](
	[chz_scanned_sgtin_id] [int] IDENTITY(1,1) NOT NULL,
	[value] [nvarchar](max) NULL,
	[chz_nakl_item_id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.chz_scanned_sgtins] PRIMARY KEY CLUSTERED 
(
	[chz_scanned_sgtin_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chz_sgtins]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_sgtins](
	[chz_sgtin_id] [int] IDENTITY(1,1) NOT NULL,
	[sgtin] [nvarchar](max) NULL,
	[chz_nakl_item_id] [int] NULL,
 CONSTRAINT [PK_dbo.chz_sgtins] PRIMARY KEY CLUSTERED 
(
	[chz_sgtin_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chz_source_types]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_source_types](
	[chz_source_type_id] [int] IDENTITY(1,1) NOT NULL,
	[value] [nvarchar](max) NULL,
	[is_default] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.chz_source_types] PRIMARY KEY CLUSTERED 
(
	[chz_source_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chz_turnover_types]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_turnover_types](
	[chz_turnover_tupe_id] [int] IDENTITY(1,1) NOT NULL,
	[value] [nvarchar](max) NULL,
	[is_default] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.chz_turnover_types] PRIMARY KEY CLUSTERED 
(
	[chz_turnover_tupe_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chz_users]    Script Date: 19.01.2021 14:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chz_users](
	[chz_user_id] [int] IDENTITY(1,1) NOT NULL,
	[login] [nvarchar](max) NULL,
	[password] [nvarchar](max) NULL,
	[fio] [nvarchar](max) NULL,
	[access_tokent] [nvarchar](max) NULL,
	[chz_company_id] [int] NULL,
 CONSTRAINT [PK_dbo.chz_users] PRIMARY KEY CLUSTERED 
(
	[chz_user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[chz_addresses]  WITH CHECK ADD  CONSTRAINT [FK_chz_addresses_chz_companies] FOREIGN KEY([chz_company_id])
REFERENCES [dbo].[chz_companies] ([chz_company_id])
GO
ALTER TABLE [dbo].[chz_addresses] CHECK CONSTRAINT [FK_chz_addresses_chz_companies]
GO
ALTER TABLE [dbo].[chz_nakl_items]  WITH CHECK ADD  CONSTRAINT [FK_chz_nakl_items_chz_nakls] FOREIGN KEY([chz_nakl_id])
REFERENCES [dbo].[chz_nakls] ([chz_nakl_id])
GO
ALTER TABLE [dbo].[chz_nakl_items] CHECK CONSTRAINT [FK_chz_nakl_items_chz_nakls]
GO
ALTER TABLE [dbo].[chz_nakls]  WITH CHECK ADD  CONSTRAINT [FK_chz_nakls_chz_accept_types] FOREIGN KEY([chz_nakls__list_of_accept_type_id])
REFERENCES [dbo].[chz_nakls__list_of_accept_types] ([chz_nakls__list_of_accept_type_id])
GO
ALTER TABLE [dbo].[chz_nakls] CHECK CONSTRAINT [FK_chz_nakls_chz_accept_types]
GO
ALTER TABLE [dbo].[chz_nakls]  WITH CHECK ADD  CONSTRAINT [FK_chz_nakls_chz_addresses] FOREIGN KEY([provider_chz_address_id])
REFERENCES [dbo].[chz_addresses] ([chz_address_id])
GO
ALTER TABLE [dbo].[chz_nakls] CHECK CONSTRAINT [FK_chz_nakls_chz_addresses]
GO
ALTER TABLE [dbo].[chz_nakls]  WITH CHECK ADD  CONSTRAINT [FK_chz_nakls_chz_addresses1] FOREIGN KEY([receiver_chz_address_id])
REFERENCES [dbo].[chz_addresses] ([chz_address_id])
GO
ALTER TABLE [dbo].[chz_nakls] CHECK CONSTRAINT [FK_chz_nakls_chz_addresses1]
GO
ALTER TABLE [dbo].[chz_nakls]  WITH CHECK ADD  CONSTRAINT [FK_chz_nakls_chz_contract_types] FOREIGN KEY([chz_contract_type_id])
REFERENCES [dbo].[chz_contract_types] ([chz_contract_type_id])
GO
ALTER TABLE [dbo].[chz_nakls] CHECK CONSTRAINT [FK_chz_nakls_chz_contract_types]
GO
ALTER TABLE [dbo].[chz_nakls]  WITH CHECK ADD  CONSTRAINT [FK_chz_nakls_chz_nakl_statuses] FOREIGN KEY([chz_nakl_status_id])
REFERENCES [dbo].[chz_nakl_statuses] ([chz_nakl_status_id])
GO
ALTER TABLE [dbo].[chz_nakls] CHECK CONSTRAINT [FK_chz_nakls_chz_nakl_statuses]
GO
ALTER TABLE [dbo].[chz_nakls]  WITH CHECK ADD  CONSTRAINT [FK_chz_nakls_chz_receive_types] FOREIGN KEY([chz_receive_type_id])
REFERENCES [dbo].[chz_receive_types] ([chz_receive_type_id])
GO
ALTER TABLE [dbo].[chz_nakls] CHECK CONSTRAINT [FK_chz_nakls_chz_receive_types]
GO
ALTER TABLE [dbo].[chz_nakls]  WITH CHECK ADD  CONSTRAINT [FK_chz_nakls_chz_source_types] FOREIGN KEY([chz_source_type_id])
REFERENCES [dbo].[chz_source_types] ([chz_source_type_id])
GO
ALTER TABLE [dbo].[chz_nakls] CHECK CONSTRAINT [FK_chz_nakls_chz_source_types]
GO
ALTER TABLE [dbo].[chz_nakls]  WITH CHECK ADD  CONSTRAINT [FK_chz_nakls_chz_turnover_types] FOREIGN KEY([chz_turnover_tupe_id])
REFERENCES [dbo].[chz_turnover_types] ([chz_turnover_tupe_id])
GO
ALTER TABLE [dbo].[chz_nakls] CHECK CONSTRAINT [FK_chz_nakls_chz_turnover_types]
GO
ALTER TABLE [dbo].[chz_scanned_sgtins]  WITH CHECK ADD  CONSTRAINT [FK_chz_scanned_sgtins_chz_nakl_items] FOREIGN KEY([chz_nakl_item_id])
REFERENCES [dbo].[chz_nakl_items] ([chz_nakl_item_id])
GO
ALTER TABLE [dbo].[chz_scanned_sgtins] CHECK CONSTRAINT [FK_chz_scanned_sgtins_chz_nakl_items]
GO
ALTER TABLE [dbo].[chz_sgtins]  WITH CHECK ADD  CONSTRAINT [FK_chz_sgtins_chz_nakl_items] FOREIGN KEY([chz_nakl_item_id])
REFERENCES [dbo].[chz_nakl_items] ([chz_nakl_item_id])
GO
ALTER TABLE [dbo].[chz_sgtins] CHECK CONSTRAINT [FK_chz_sgtins_chz_nakl_items]
GO
ALTER TABLE [dbo].[chz_users]  WITH CHECK ADD  CONSTRAINT [FK_chz_users_chz_companies] FOREIGN KEY([chz_company_id])
REFERENCES [dbo].[chz_companies] ([chz_company_id])
GO
ALTER TABLE [dbo].[chz_users] CHECK CONSTRAINT [FK_chz_users_chz_companies]
GO
