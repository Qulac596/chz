CREATE PROCEDURE dbo.chz_scanned_sgtins__add (@sgtins_list varchar(max), @chz_nakl_id int, @chz_nakl_item_id int) 
-- @chz_nakl_id и @sgtins_list об€зательные параметры, @chz_nakl_item_id опциональный
-- ѕолучаем список @sgtins_list, добавл€ем его в dbo.chz_scanned_sgtins
-- declare @sgtins_list varchar(max)='0123456789123456, 0123456789123457, 0123456789123458, 0123456789123459', @chz_nakl_id int=1, @chz_nakl_item_id int=1;
as Begin

declare @res table (rec_id int, rec_value varchar(300) not null, gtin varchar(14) not null, chz_nakl_item_id int);
declare @direct_accept bit;

-- «аполн€ем переменную @direct_accept
select @direct_accept = direct_accept from dbo.chz_nakls where chz_nakl_id = @chz_nakl_id;
set @direct_accept=1;

-- переформируем @sgtins_list из строки в таблицу
insert into @res (rec_id, rec_value, gtin)
select rec_id, ltrim(rec_value), cast(LEFT(ltrim(rec_value), 14) as bigint)
from dbo.StringToVarcharTable (@sgtins_list);

-- заполн€ем chz_nakl_item_id дл€ всех строк из @res
update r set r.chz_nakl_item_id = ni.chz_nakl_item_id
from @res r
  inner join dbo.chz_nakl_items ni on r.gtin = ni.gtin and ni.chz_nakl_id = @chz_nakl_id

-- при необходимости, добавл€ем записи в dbo.chz_nakl_items
insert into dbo.chz_nakl_items (chz_nakl_id, gtin, name)
select @chz_nakl_id, gtin, 'Ќовое лекарство gtin ' + max(LEFT(ltrim(rec_value), 14))
from @res r 
where r.chz_nakl_item_id is null
group by r.gtin

-- заполн€ем chz_nakl_item_id дл€ новых gtin
update r set r.chz_nakl_item_id = ni.chz_nakl_item_id
from @res r
  inner join dbo.chz_nakl_items ni on r.gtin = ni.gtin and ni.chz_nakl_id = @chz_nakl_id
where r.chz_nakl_item_id is null

-- добавл€ем недостающие записи в dbo.chz_scanned_sgtins
insert into dbo.chz_scanned_sgtins (chz_nakl_item_id, sgtin)
select r.chz_nakl_item_id, r.rec_value
from @res r
  left join dbo.chz_scanned_sgtins ss on r.chz_nakl_item_id = ss.chz_nakl_item_id and r.rec_value = ss.sgtin
where ss.sgtin is null;

-- ¬озвращаем список полученных sgtin, с указанием статуса
if @direct_accept=0 
Begin
  select rec_id, rec_value as sgtin, gtin, chz_nakl_item_id, 'Ќовый' as status, 'table__block-table-text' as style
    -- если sgtin не относитс€ к редактируемой сейчас строке накладной, возвращаем gtin_same_as_current=0
    , case when @chz_nakl_item_id Is Null then Null when r.chz_nakl_item_id=@chz_nakl_item_id then 1 else 0 end as gtin_same_as_current
  from @res r 
  order by rec_value;
  return;
End
if @direct_accept=1 
Begin
  select r.rec_id, r.rec_value as sgtin, r.gtin, r.chz_nakl_item_id
    , case when s.sgtin IS Null then 'Ћишний' when s.sgtin IS not Null then 'ѕроверен' end as status
    , case when s.sgtin IS Null then 'table__block-table-disagree' when s.sgtin IS not Null then 'table__block-table-coincide' end as style
    -- если sgtin не относитс€ к редактируемой сейчас строке накладной, возвращаем gtin_same_as_current=0
    , case when @chz_nakl_item_id Is Null then Null when r.chz_nakl_item_id=@chz_nakl_item_id then 1 else 0 end as gtin_same_as_current
  from @res r
    left join 
    (select s.chz_nakl_item_id, s.sgtin
    from dbo.chz_nakl_items ni
      inner join dbo.chz_sgtins s on ni.chz_nakl_item_id = s.chz_nakl_item_id
    where ni.chz_nakl_id = @chz_nakl_id
    ) s on r.chz_nakl_item_id = s.chz_nakl_item_id and r.rec_value = s.sgtin
  order by r.rec_value;
  return;
End
/*
select * from @res;
select * from dbo.chz_nakl_items

set identity_insert dbo.chz_companies on
  insert into dbo.chz_companies(chz_company_id, inn, name) values (1, '012345678912', 'Ќаша фирма')
  insert into dbo.chz_companies(chz_company_id, inn, name) values (2, '012345678912', '–ога копыта')
set identity_insert dbo.chz_companies off
set identity_insert dbo.chz_addresses on
  insert into dbo.chz_addresses(chz_company_id, chz_address_id, text) values (1, 1, 'Ќевский пр. д.1')
  insert into dbo.chz_addresses(chz_company_id, chz_address_id, text) values (2, 2, ' удыкина гора, строение 32')
set identity_insert dbo.chz_addresses off
set identity_insert dbo.chz_nakls on
  insert into dbo.chz_nakls(chz_nakl_id, operation_data, doc_num, doc_data, provider_chz_address_id, receiver_chz_address_id) 
  values (1, '2020-12-01', '012/7', '2020-12-03', 2, 1)
  insert into dbo.chz_nakls(chz_nakl_id, operation_data, doc_num, doc_data, provider_chz_address_id, receiver_chz_address_id) 
  values (2, '2020-12-03', '012/8', '2020-12-04', 2, 1)
set identity_insert dbo.chz_nakls off
*/
--grant execute on dbo.chz_scanned_sgtins__add to rl_chz_user

End