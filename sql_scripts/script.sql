use g82

--компании
CREATE TABLE [dbo].[chz_company](
	[chz_company_id] [int] IDENTITY(1,1) NOT NULL constraint PK_egisz_chz_company Primary Key Clustered  ,
	[system_subj_id] [varchar](50) NULL,
	[inn] [varchar](20) NULL,
	[kpp] [varchar](20) NULL,
	[org_name] [varchar](500) NULL,
	[ogrn] [varchar](20) NULL,
	[address] [varchar](max) NULL)

--типы контрактов
CREATE TABLE [dbo].[chz_contract_type](
	[chz_contract_type_id] [int] IDENTITY(1,1) NOT NULL constraint PK_chz_contract_type Primary Key Clustered,
	[contract_type_name] [nvarchar](max) NOT NULL,
	[code] [int] NOT NULL)

--входные документы
CREATE TABLE [dbo].[chz_income_documents](
	[id] [int] IDENTITY(1,1) NOT NULL constraint PK_chz_income_documents Primary Key Clustered,
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
	[complete_date] [datetime2](7) NULL
	)

  --лог службы
CREATE TABLE [dbo].[chz_log](
	[chz_log_id] [int] IDENTITY(1,1) NOT NULL constraint PK_chz_log Primary Key Clustered,
	[rec_created_login] [varchar](50) NULL,
	[rec_created_time] [datetime] NULL,
	[service_name] [varchar](100) NOT NULL,
	[msg] [varchar](max) NULL,
	[msg_type] [varchar](max) NULL
	)

	-- накладные
CREATE TABLE [dbo].[chz_nakl](
	[chz_nakl_id] [int] IDENTITY(1,1) NOT NULL constraint PK_chz_nakl Primary Key Clustered,
	[chz_supplier_id] [int] NULL,
	[chz_nakl_status_id] [int] NOT NULL,
	[chz_recipient_id] [int] NULL,
	[nakl_send_date] [datetime] NULL,
	[nakl_recv_date] [datetime] NULL,
	[chz_contract_type_id] [int] NOT NULL,
)

  -- статусы накладных
CREATE TABLE [dbo].[chz_nakl__list_of_status](
	[chz_nakl_status_id] [int] NOT NULL constraint PK_nakl__list_of_status Primary Key Clustered,
	[chz_nakl_status_name] [varchar](100) NOT NULL,
	[status_back_color] [int] NULL,
	[status_back_color_hoover] [int] NULL
	)

	--элементы накладных
CREATE TABLE [dbo].[chz_nakl_items](
	[chz_nakl_item_id] [int] IDENTITY(1,1) NOT NULL constraint PK_chz_nakl_items Primary Key Clustered,
	[gtin] [nvarchar](max) NOT NULL,
	[sgtin] [nvarchar](max) NOT NULL,
	[prod_name] [nvarchar](max) NULL,
	[sell_name] [nvarchar](max) NULL,
	[full_prod_name] [nvarchar](max) NULL,
	[pack1_desc] [nvarchar](max) NULL,
	[prod_d_name] [nvarchar](max) NULL,
	[prod_form_name] [nvarchar](max) NULL,
	[chz_nakl_id] [int] NOT NULL
	)

	--stins
	CREATE TABLE [dbo].[chz_sgtins](
	[sgtin_id] [int] NOT NULL constraint PK_chz_sgtins Primary Key Clustered,
	[sgtin] [nvarchar](max) NOT NULL,
	[chz_nakl_item_id] [int] NOT NULL,
	[is_scanned][bit] Not Null
	)

	-- исходящие документы
CREATE TABLE [dbo].[chz_outcome_documents](
	[id] [int] IDENTITY(1,1) NOT NULL constraint PK_chz_outcome_documents Primary Key Clustered,
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
	[complete_date] [datetime2](7) NULL
	)

	--запросы
CREATE TABLE [dbo].[chz_request](
	[id] [int] IDENTITY(1,1) NOT NULL constraint PK_chz_request Primary Key Clustered,
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
	[complete_date] [datetime2](7) NULL
	)

	Alter Table dbo.chz_nakl Add Constraint FK_chz_supplier_id FOREIGN KEY (chz_supplier_id) REFERENCES chz_company(chz_company_id)
	Alter Table dbo.chz_nakl Add Constraint FK_chz_recipient_id FOREIGN KEY(chz_recipient_id) REFERENCES chz_company(chz_company_id)
	Alter Table dbo.chz_nakl Add Constraint FK_chz_nakl_status_id FOREIGN KEY(chz_nakl_status_id) REFERENCES chz_nakl__list_of_status(chz_nakl_status_id)
	Alter Table dbo.chz_nakl Add Constraint FK_chz_contract_type_id FOREIGN KEY(chz_contract_type_id)REFERENCES chz_contract_type(chz_contract_type_id)

	Alter Table chz_sgtins Add Constraint FK_chz_nakl_item_id FOREIGN KEY (chz_nakl_item_id) REFERENCES chz_nakl_items(chz_nakl_item_id)
	Alter Table chz_nakl_items Add Constraint FK_chz_nakl_id FOREIGN KEY (chz_nakl_id) REFERENCES chz_nakl(chz_nakl_id)

	Go
--выбирает накладные по фильтру
 Create PROCEDURE [dbo].[chz_nakl_filtered_list]
  (@year int, @month int, @chz_nakl_status_id int, @chz_supplier_id int, @chz_recipient_id int)
AS BEGIN
set dateformat ymd;
Set Nocount On;
declare @sql nvarchar(2000), @d1 datetime, @d2 datetime;

-- 221 [Администрирование - Протоколировать SQL выражения запускаемых отчетов (отладка)]
  if exists(select const_id from dbo.constants where const_id=221 and (const_val='Да' or const_val=system_user)) Begin
    set @sql='set dateformat ymd; exec dbo.chz_nakl_filtered_list '
        + case when @year is null then 'Null' else cast(@year as varchar) end
        + ','+case when @month is null then 'Null' else cast(@month as varchar) end
        + ','+case when @chz_nakl_status_id is null then 'Null' else cast(@chz_nakl_status_id as varchar) end
        + ','+case when @chz_supplier_id is null then 'Null' else cast(@chz_supplier_id as varchar) end
        + ','+case when @chz_recipient_id is null then 'Null' else cast(@chz_recipient_id as varchar) end;
    insert into debug(val) values(@sql);
    End;
-- =======================================================================================
if @year is not null and @month is not null 
Begin
  set @d1 = CONVERT(datetime, cast(@year as varchar(4)) + '-' + CAST(@month as varchar(2)) + '-01');
  set @d2 = dateadd(dd, -1, DATEADD(mm, 1, @d1));
End
if @year is not null and @month is null 
Begin
  set @d1 = CONVERT(datetime, cast(@year as varchar(4)) + '-01-01');
  set @d2 = dateadd(dd, -1, DATEADD(yy, 1, @d1));
End

if @d1 is not null
Begin
  select * 
  from dbo.chz_nakl
  where nakl_send_date between @d1 and @d2
    and (chz_nakl_status_id = @chz_nakl_status_id OR @chz_nakl_status_id Is Null)
    and (chz_supplier_id = @chz_supplier_id OR @chz_supplier_id Is Null)
    and (chz_recipient_id = @chz_recipient_id OR @chz_recipient_id Is Null);
  return;
End

if @d1 is null and @month is not null
Begin
  select * 
  from dbo.chz_nakl
  where month(nakl_send_date) = @month
    and (chz_nakl_status_id = @chz_nakl_status_id OR @chz_nakl_status_id Is Null)
    and (chz_supplier_id = @chz_supplier_id OR @chz_supplier_id Is Null)
    and (chz_recipient_id = @chz_recipient_id OR @chz_recipient_id Is Null);
  return;
End

select * 
from dbo.chz_nakl
where 
  (chz_nakl_status_id = @chz_nakl_status_id OR @chz_nakl_status_id Is Null)
  and (chz_supplier_id = @chz_supplier_id OR @chz_supplier_id Is Null)
  and (chz_recipient_id = @chz_recipient_id OR @chz_recipient_id Is Null);

-- grant execute on dbo.chz_nakl_filtered_list to my_admin
END