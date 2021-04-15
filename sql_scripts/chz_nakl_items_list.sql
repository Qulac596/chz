ALTER PROCEDURE dbo.chz_nakl_items_list (@nakl_id int)
-- Возвращает список строк для формы ЧЗ "Одна накладная"
AS BEGIN
-- для тестирования раскомментить:
-- declare @nakl_id int=9; 

  declare @res table (chz_nakl_item_id int, q_sgtins int, q_scanned_sgtins int)

  insert into @res (chz_nakl_item_id)
  select chz_nakl_item_id
  from dbo.chz_nakl_items 
  where chz_nakl_id=@nakl_id

  update r set r.q_sgtins = s.q_sgtins, r.q_scanned_sgtins = scs.q_scanned_sgtins
  from @res r
    inner join
    (select r.chz_nakl_item_id, SUM(1) as q_scanned_sgtins
    from @res r 
      inner join dbo.chz_scanned_sgtins s on r.chz_nakl_item_id = s.chz_nakl_item_id 
    group by r.chz_nakl_item_id) as scs on r.chz_nakl_item_id = scs.chz_nakl_item_id 
    inner join
    (select r.chz_nakl_item_id, SUM(1) as q_sgtins
    from @res r 
      inner join dbo.chz_sgtins s on r.chz_nakl_item_id = s.chz_nakl_item_id 
    group by r.chz_nakl_item_id) as s on r.chz_nakl_item_id = s.chz_nakl_item_id;

  select ni.*, r.q_sgtins, r.q_scanned_sgtins
  from @res r 
    inner join dbo.chz_nakl_items ni on r.chz_nakl_item_id = ni.chz_nakl_item_id;
End