ALTER PROCEDURE dbo.chz_nakl_items_sgtins_list (@item_id int)
-- Возвращает список строк для формы ЧЗ "сканирование"
AS BEGIN
-- для тестирования раскомментить:
-- declare @item_id int=9; 
declare @direct_accept bit;

  select @direct_accept = direct_accept
  from dbo.chz_nakl_items ni
    inner join dbo.chz_nakls n on ni.chz_nakl_id = n.chz_nakl_id
  where ni.chz_nakl_item_id = @item_id;
  
  -- Обратный акцепт - накладная создается клиникой 
  if @direct_accept=0
  Begin
    select chz_scanned_sgtin_id, Null as chz_sgtin_id, sgtin, chz_nakl_item_id
      , 'Новый' as status
    from dbo.chz_scanned_sgtins;
    
    Return;
  End;
  
  -- Прямой акцепт
  select ss.chz_scanned_sgtin_id, s.chz_sgtin_id, ISNULL(ss.sgtin, s.sgtin) as sgtin, @item_id as chz_nakl_item_id
    , case when ss.chz_scanned_sgtin_id Is Null and s.chz_sgtin_id is not Null then 'Не проверен'
      when ss.chz_scanned_sgtin_id Is Not Null and s.chz_sgtin_id is not Null then 'Совпадает'
      when ss.chz_scanned_sgtin_id Is Not Null and s.chz_sgtin_id is Null then 'Лишний'
      else 'Ошибка' End as status
  from dbo.chz_scanned_sgtins ss
    full outer join dbo.chz_sgtins s on ss.sgtin = s.sgtin
  where ss.chz_nakl_item_id = @item_id OR s.chz_nakl_item_id = @item_id;
  

  
  --declare @res table (chz_nakl_item_id int, q_sgtins int, q_scanned_sgtins int)

  --insert into @res (chz_nakl_item_id)
  --select chz_nakl_item_id
  --from dbo.chz_nakl_items 
  --where chz_nakl_id=@nakl_id

  --update r set r.q_sgtins = s.q_sgtins, r.q_scanned_sgtins = scs.q_scanned_sgtins
  --from @res r
  --  inner join
  --  (select r.chz_nakl_item_id, SUM(1) as q_scanned_sgtins
  --  from @res r 
  --    inner join dbo.chz_scanned_sgtins s on r.chz_nakl_item_id = s.chz_nakl_item_id 
  --  group by r.chz_nakl_item_id) as scs on r.chz_nakl_item_id = scs.chz_nakl_item_id 
  --  inner join
  --  (select r.chz_nakl_item_id, SUM(1) as q_sgtins
  --  from @res r 
  --    inner join dbo.chz_sgtins s on r.chz_nakl_item_id = s.chz_nakl_item_id 
  --  group by r.chz_nakl_item_id) as s on r.chz_nakl_item_id = s.chz_nakl_item_id;

  --select ni.*, r.q_sgtins, r.q_scanned_sgtins
  --from @res r 
  --  inner join dbo.chz_nakl_items ni on r.chz_nakl_item_id = ni.chz_nakl_item_id;
End