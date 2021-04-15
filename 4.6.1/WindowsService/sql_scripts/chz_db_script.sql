CREATE TABLE [dbo].[chz_income_documents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[request_id] [nvarchar](max) NULL,
	[document_id] [nvarchar](max) NULL,
	[date] [datetime] NULL,
	[processed_date] [datetime] NULL,
	[sender] [nvarchar](max) NULL,
	[receiver] [nvarchar](max) NULL,
	[sys_id] [nvarchar](max) NULL,
	[doc_type] [int] NULL,
	[mdlp_ducument_status] [nvarchar](max) NULL,
	[file_uploadtype] [int] NULL,
	[version] [nvarchar](max) NULL,
	[sender_sys_id] [nvarchar](max) NULL,
	[content] [nvarchar](max) NULL,
	[link] [nvarchar](max) NULL,
	[service_document_status] [nvarchar](max) NULL,
	[error_type] [nvarchar](max) NULL,
	[error_message] [nvarchar](max) NULL,
	[http_status] [nvarchar](max) NULL,
	[error_response] [nvarchar](max) NULL,
	[guid] [nvarchar](max) NULL,
	[complete_date] [datetime2](7) NULL,
 CONSTRAINT [PK_dbo.IncomeDocuments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[chz_income_documents] ADD  DEFAULT ('New') FOR [service_document_status]
GO

CREATE TABLE [dbo].[chz_log](
	[chz_log_id] [int] IDENTITY(1,1) NOT NULL,
	[rec_created_login] [varchar](50) NULL,
	[rec_created_time] [datetime] NULL,
	[service_name] [varchar](100) NOT NULL,
	[msg] [varchar](max) NULL,
	[msg_type] [varchar](max) NULL,
 CONSTRAINT [PK_chz_log] PRIMARY KEY CLUSTERED 
(
	[chz_log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[chz_log] ADD  CONSTRAINT [DF_chz_log_rec_created_login]  DEFAULT (suser_sname()) FOR [rec_created_login]
GO

ALTER TABLE [dbo].[chz_log] ADD  CONSTRAINT [DF_chz_log_rec_created_time]  DEFAULT (getdate()) FOR [rec_created_time]
GO

CREATE TABLE [dbo].[chz_outcome_documents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[is_cansel] [nvarchar](max) NOT NULL,
	[guid] [nvarchar](max) NULL,
	[document_id] [nvarchar](max) NULL,
	[request_id] [nvarchar](max) NULL,
	[content] [nvarchar](max) NULL,
	[base_64_content] [nvarchar](max) NULL,
	[signatyre] [nvarchar](max) NULL,
	[mdlp_ducument_status] [nvarchar](max) NULL,
	[attempt_count] [int] NOT NULL,
	[ticket_link] [nvarchar](max) NULL,
	[ticket] [nvarchar](max) NULL,
	[service_document_status] [nvarchar](max) NULL,
	[error_message] [nvarchar](max) NULL,
	[error_type] [nvarchar](max) NULL,
	[http_status] [nvarchar](50) NULL,
	[error_response] [nvarchar](max) NULL,
	[complete_date] [datetime2](7) NULL,
 CONSTRAINT [PK_outcome_document] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[chz_outcome_documents] ADD  DEFAULT ('False') FOR [is_cansel]
GO

ALTER TABLE [dbo].[chz_outcome_documents] ADD  CONSTRAINT [DF_outcome_document_document_service_status]  DEFAULT ('New') FOR [service_document_status]
GO

CREATE TABLE [dbo].[chz_request](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[url] [varchar](200) NOT NULL,
	[request_json] [varchar](max) NULL,
	[method] [varchar](20) NULL,
	[answer_json] [varchar](max) NULL,
	[start_after] [datetime] NULL,
	[service_request_status] [nvarchar](max) NOT NULL,
	[error_type] [nvarchar](max) NULL,
	[error_message] [nvarchar](max) NULL,
	[http_status_code] [nvarchar](max) NULL,
	[rec_created_login] [varchar](50) NULL,
	[rec_created_time] [datetime] NULL,
	[last_change_login] [varchar](50) NULL,
	[last_change_time] [datetime] NULL,
	[error_response] [nvarchar](max) NULL,
	[guid] [nvarchar](max) NULL,
	[complete_date] [datetime2](7) NULL,
 CONSTRAINT [PK_chz_request] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[chz_request] ADD  CONSTRAINT [DF_chz_request_status]  DEFAULT ('New') FOR [service_request_status]
GO

ALTER TABLE [dbo].[chz_request] ADD  CONSTRAINT [DF_chz_request_rec_created_login]  DEFAULT (suser_sname()) FOR [rec_created_login]
GO

ALTER TABLE [dbo].[chz_request] ADD  CONSTRAINT [DF_chz_request_rec_created_time]  DEFAULT (getdate()) FOR [rec_created_time]
GO