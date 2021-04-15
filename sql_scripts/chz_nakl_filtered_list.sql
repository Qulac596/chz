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