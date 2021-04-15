
CREATE TABLE [dbo].[chz_addresses] (
    [chz_address_id] INT            IDENTITY (1, 1) NOT NULL,
    [sys_id]         NVARCHAR (MAX) NOT NULL,
    [text]           NVARCHAR (MAX) NOT NULL,
    [chz_company_id] INT            NOT NULL,
    CONSTRAINT [PK_chz_addresses] PRIMARY KEY CLUSTERED ([chz_address_id] ASC),
    CONSTRAINT [FK_chz_addresses_chz_companys] FOREIGN KEY ([chz_company_id]) REFERENCES [dbo].[chz_companys] ([chz_company_id])
);


CREATE TABLE [dbo].[chz_companys] (
    [chz_company_id]        INT            IDENTITY (1, 1) NOT NULL,
    [system_subj_id]        NVARCHAR (MAX) NULL,
    [inn]                   NVARCHAR (MAX) NULL,
    [kpp]                   NVARCHAR (MAX) NULL,
    [org_name]              NVARCHAR (MAX) NULL,
    [ogrn]                  NVARCHAR (MAX) NULL,
    [chz_company_status_id] INT            NULL,
    CONSTRAINT [PK_chz_companys] PRIMARY KEY CLUSTERED ([chz_company_id] ASC),
    CONSTRAINT [FK_chz_companys_chz_companys_statuses] FOREIGN KEY ([chz_company_status_id]) REFERENCES [dbo].[chz_companys_statuses] ([chz_company_status_id])
);

CREATE TABLE [dbo].[chz_companys_statuses] (
    [chz_company_status_id] INT            IDENTITY (1, 1) NOT NULL,
    [name]                  NVARCHAR (MAX) NOT NULL,
    [color]                 NCHAR (10)     NOT NULL,
    CONSTRAINT [PK_chz_companys_statuses] PRIMARY KEY CLUSTERED ([chz_company_status_id] ASC)
);

CREATE TABLE [dbo].[chz_contract_types] (
    [chz_contract_type_id] INT            IDENTITY (1, 1) NOT NULL,
    [name]                 NVARCHAR (MAX) NOT NULL,
    [is_default]           BIT            NULL,
    CONSTRAINT [PK_chz_contract_types] PRIMARY KEY CLUSTERED ([chz_contract_type_id] ASC)
);

CREATE TABLE [dbo].[chz_gtin_statuses] (
    [gtin_status_id] INT            IDENTITY (1, 1) NOT NULL,
    [name]           NVARCHAR (MAX) NOT NULL,
    [color]          NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_chz_gtin_statuses] PRIMARY KEY CLUSTERED ([gtin_status_id] ASC)
);

CREATE TABLE [dbo].[chz_nakl_statuses] (
    [chz_nakl_status_id] INT            IDENTITY (1, 1) NOT NULL,
    [name]               NVARCHAR (MAX) NOT NULL,
    [pic_url]            NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_chz_nakl_statuses] PRIMARY KEY CLUSTERED ([chz_nakl_status_id] ASC)
);

CREATE TABLE [dbo].[chz_nakls] (
    [chz_nakl_id]        INT            IDENTITY (1, 1) NOT NULL,
    [subject_id]         INT            NOT NULL,
    [receiver_id]        INT            NULL,
    [operation_date]     DATETIME       NOT NULL,
    [doc_num]            NVARCHAR (MAX) NOT NULL,
    [doc_date]           DATETIME       NOT NULL,
    [turnover_type]      INT            NULL,
    [source]             INT            NOT NULL,
    [contract_type]      INT            NOT NULL,
    [contract_num]       NVARCHAR (MAX) NULL,
    [chz_nakl_status_id] INT            NOT NULL,
    [is_direct]          BIT            NOT NULL,
    [shipper_id]         INT            NULL,
    [date_time_finish]   DATETIME       NULL,
    [is_trust_accept]    BIT            NULL,
    [receive_type]       INT            NULL,
    CONSTRAINT [PK_chz_nakls] PRIMARY KEY CLUSTERED ([chz_nakl_id] ASC),
    CONSTRAINT [FK_chz_nakls_chz_contract_types] FOREIGN KEY ([contract_type]) REFERENCES [dbo].[chz_contract_types] ([chz_contract_type_id]),
    CONSTRAINT [FK_chz_nakls_chz_nakl_statuses] FOREIGN KEY ([chz_nakl_status_id]) REFERENCES [dbo].[chz_nakl_statuses] ([chz_nakl_status_id]),
    CONSTRAINT [FK_chz_nakls_chz_addresses] FOREIGN KEY ([subject_id]) REFERENCES [dbo].[chz_addresses] ([chz_address_id]),
    CONSTRAINT [FK_chz_nakls_chz_addresses1] FOREIGN KEY ([receiver_id]) REFERENCES [dbo].[chz_addresses] ([chz_address_id]),
    CONSTRAINT [FK_chz_nakls_chz_addresses2] FOREIGN KEY ([shipper_id]) REFERENCES [dbo].[chz_addresses] ([chz_address_id]),
    CONSTRAINT [FK_chz_nakls_chz_receive_types] FOREIGN KEY ([receive_type]) REFERENCES [dbo].[chz_receive_types] ([chz_receive_type_id]),
    CONSTRAINT [FK_chz_nakls_chz_turnover_types] FOREIGN KEY ([turnover_type]) REFERENCES [dbo].[chz_turnover_types] ([chz_turnover_type_id]),
    CONSTRAINT [FK_chz_nakls_chz_source_types] FOREIGN KEY ([source]) REFERENCES [dbo].[chz_source_types] ([chz_source_type_id])
);

CREATE TABLE [dbo].[chz_nakls_items] (
    [chz_nakl_item_id] INT            IDENTITY (1, 1) NOT NULL,
    [gtin]             NVARCHAR (MAX) NULL,
    [prod_sell_name]   NVARCHAR (MAX) NOT NULL,
    [chz_nakl_id]      INT            NOT NULL,
    [gtin_status_id]   INT            NULL,
    [const]            MONEY          NULL,
    [vat_value]        MONEY          NULL,
    CONSTRAINT [PK_chz_gtins] PRIMARY KEY CLUSTERED ([chz_nakl_item_id] ASC),
    CONSTRAINT [FK_chz_gtins_chz_nakls] FOREIGN KEY ([chz_nakl_id]) REFERENCES [dbo].[chz_nakls] ([chz_nakl_id]),
    CONSTRAINT [FK_chz_gtins_chz_gtin_statuses] FOREIGN KEY ([gtin_status_id]) REFERENCES [dbo].[chz_gtin_statuses] ([gtin_status_id])
);

CREATE TABLE [dbo].[chz_nds_values] (
    [chz_nds_value_id] INT IDENTITY (1, 1) NOT NULL,
    [value]            INT NOT NULL,
    [is_default]       BIT NOT NULL,
    CONSTRAINT [PK_chz_nds_values] PRIMARY KEY CLUSTERED ([chz_nds_value_id] ASC)
);

CREATE TABLE [dbo].[chz_receive_types] (
    [chz_receive_type_id] INT            IDENTITY (1, 1) NOT NULL,
    [name]                NVARCHAR (MAX) NOT NULL,
    [is_default]          BIT            NULL,
    CONSTRAINT [PK_chz_receive_types] PRIMARY KEY CLUSTERED ([chz_receive_type_id] ASC)
);

CREATE TABLE [dbo].[chz_scanned_sgtins] (
    [chz_scanned_sgtin_id] INT            IDENTITY (1, 1) NOT NULL,
    [sgtin]                NVARCHAR (MAX) NOT NULL,
    [chz_nakl_item_id]     INT            NOT NULL,
    CONSTRAINT [PK_chz_scanned_sgtins] PRIMARY KEY CLUSTERED ([chz_scanned_sgtin_id] ASC),
    CONSTRAINT [FK_chz_scanned_sgtins_chz_gtins] FOREIGN KEY ([chz_nakl_item_id]) REFERENCES [dbo].[chz_nakls_items] ([chz_nakl_item_id])
);

CREATE TABLE [dbo].[chz_sgtins] (
    [chz_sgtin_id]     INT            IDENTITY (1, 1) NOT NULL,
    [chz_nakl_item_id] INT            NOT NULL,
    [is_scened]        BIT            NOT NULL,
    [cost]             MONEY          NOT NULL,
    [vat_value]        MONEY          NOT NULL,
    [sgtin]            NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_chz_sgtins] PRIMARY KEY CLUSTERED ([chz_sgtin_id] ASC),
    CONSTRAINT [FK_chz_sgtins_chz_gtins] FOREIGN KEY ([chz_nakl_item_id]) REFERENCES [dbo].[chz_nakls_items] ([chz_nakl_item_id])
);

CREATE TABLE [dbo].[chz_source_types] (
    [chz_source_type_id] INT            IDENTITY (1, 1) NOT NULL,
    [name]               NVARCHAR (MAX) NOT NULL,
    [is_default]         BIT            NULL,
    CONSTRAINT [PK_chz_source_types] PRIMARY KEY CLUSTERED ([chz_source_type_id] ASC)
);

CREATE TABLE [dbo].[chz_turnover_types] (
    [chz_turnover_type_id] INT            IDENTITY (1, 1) NOT NULL,
    [name]                 NVARCHAR (MAX) NOT NULL,
    [is_default]           BIT            NULL,
    CONSTRAINT [PK_chz_turnover_types] PRIMARY KEY CLUSTERED ([chz_turnover_type_id] ASC)
);

CREATE TABLE [dbo].[chz_users] (
    [chz_user_id]    INT            IDENTITY (1, 1) NOT NULL,
    [login]          NVARCHAR (MAX) NOT NULL,
    [password]       NVARCHAR (MAX) NOT NULL,
    [access_token]   NVARCHAR (MAX) NULL,
    [chz_company_id] INT            NOT NULL,
    [is_lock]        BIT            NULL,
    [first_name]     NVARCHAR (MAX) NULL,
    [last_name]      NVARCHAR (MAX) NULL,
    [date_time]      DATETIME       NULL,
    CONSTRAINT [FK_chz_users_chz_companys] FOREIGN KEY ([chz_company_id]) REFERENCES [dbo].[chz_companys] ([chz_company_id])
);


