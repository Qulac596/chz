USE [g82]
GO
SET IDENTITY_INSERT [dbo].[chz_nakl_statuses] ON 
GO
INSERT [dbo].[chz_nakl_statuses] ([chz_nakl_status_id], [name], [pic_url]) VALUES (1, N'Новая', N'/image.png')
GO
INSERT [dbo].[chz_nakl_statuses] ([chz_nakl_status_id], [name], [pic_url]) VALUES (2, N'Приемка', N'/image.png')
GO
INSERT [dbo].[chz_nakl_statuses] ([chz_nakl_status_id], [name], [pic_url]) VALUES (3, N'Отправка', N'/image.png')
GO
INSERT [dbo].[chz_nakl_statuses] ([chz_nakl_status_id], [name], [pic_url]) VALUES (4, N'Ошибка', N'/image.png')
GO
SET IDENTITY_INSERT [dbo].[chz_nakl_statuses] OFF
GO
SET IDENTITY_INSERT [dbo].[chz_source_types] ON 
GO
INSERT [dbo].[chz_source_types] ([chz_source_type_id], [name], [is_default]) VALUES (1, N'собственные средства', 1)
GO
INSERT [dbo].[chz_source_types] ([chz_source_type_id], [name], [is_default]) VALUES (2, N'средства федерального бюджета', 0)
GO
INSERT [dbo].[chz_source_types] ([chz_source_type_id], [name], [is_default]) VALUES (3, N'средства регионального бюджета', 0)
GO
SET IDENTITY_INSERT [dbo].[chz_source_types] OFF
GO
SET IDENTITY_INSERT [dbo].[chz_receive_types] ON 
GO
INSERT [dbo].[chz_receive_types] ([chz_receive_type_id], [name], [is_default]) VALUES (1, N'поступление', NULL)
GO
INSERT [dbo].[chz_receive_types] ([chz_receive_type_id], [name], [is_default]) VALUES (2, N'возврат от покупателя', NULL)
GO
SET IDENTITY_INSERT [dbo].[chz_receive_types] OFF
GO
SET IDENTITY_INSERT [dbo].[chz_turnover_types] ON 
GO
INSERT [dbo].[chz_turnover_types] ([chz_turnover_type_id], [name], [is_default]) VALUES (1, N'продажа', NULL)
GO
INSERT [dbo].[chz_turnover_types] ([chz_turnover_type_id], [name], [is_default]) VALUES (2, N'возврат', NULL)
GO
SET IDENTITY_INSERT [dbo].[chz_turnover_types] OFF
GO
SET IDENTITY_INSERT [dbo].[chz_contract_types] ON 
GO
INSERT [dbo].[chz_contract_types] ([chz_contract_type_id], [name], [is_default]) VALUES (1, N'купля продажа', 1)
GO
INSERT [dbo].[chz_contract_types] ([chz_contract_type_id], [name], [is_default]) VALUES (2, N'комиссия', 0)
GO
INSERT [dbo].[chz_contract_types] ([chz_contract_type_id], [name], [is_default]) VALUES (3, N'агентский договор', 0)
GO
INSERT [dbo].[chz_contract_types] ([chz_contract_type_id], [name], [is_default]) VALUES (4, N'передача на безвозмездной основе', 0)
GO
INSERT [dbo].[chz_contract_types] ([chz_contract_type_id], [name], [is_default]) VALUES (5, N'возврат контрактному производителю', 0)
GO
INSERT [dbo].[chz_contract_types] ([chz_contract_type_id], [name], [is_default]) VALUES (6, N'государственное лекарственное обеспечение', 0)
GO
INSERT [dbo].[chz_contract_types] ([chz_contract_type_id], [name], [is_default]) VALUES (7, N'договор консигнации', 0)
GO
INSERT [dbo].[chz_contract_types] ([chz_contract_type_id], [name], [is_default]) VALUES (8, N'собственные средства', 0)
GO
SET IDENTITY_INSERT [dbo].[chz_contract_types] OFF
GO
SET IDENTITY_INSERT [dbo].[chz_companys_statuses] ON 
GO
INSERT [dbo].[chz_companys_statuses] ([chz_company_status_id], [name], [color]) VALUES (1, N'Ok', N'#008000   ')
GO
INSERT [dbo].[chz_companys_statuses] ([chz_company_status_id], [name], [color]) VALUES (3, N'Поиск в ЧЗ', N'#ffff00
 ')
GO
INSERT [dbo].[chz_companys_statuses] ([chz_company_status_id], [name], [color]) VALUES (4, N'Ошибка', N'#ff0000   ')
GO
SET IDENTITY_INSERT [dbo].[chz_companys_statuses] OFF
GO
SET IDENTITY_INSERT [dbo].[chz_companys] ON 
GO
INSERT [dbo].[chz_companys] ([chz_company_id], [system_subj_id], [inn], [kpp], [org_name], [ogrn], [chz_company_status_id]) VALUES (1, N'34405959484', N'4455664', N'5556664', N'ОАО Тяпс Ляпс Энд Корпорейдет', N'345566', 1)
GO
INSERT [dbo].[chz_companys] ([chz_company_id], [system_subj_id], [inn], [kpp], [org_name], [ogrn], [chz_company_status_id]) VALUES (2, N'345566676', N'45567788', N'345677', N'ОАО Все бесплатно', N'3445566', 1)
GO
INSERT [dbo].[chz_companys] ([chz_company_id], [system_subj_id], [inn], [kpp], [org_name], [ogrn], [chz_company_status_id]) VALUES (4, N'345666', N'4456777', N'3444567', N'Колхоз Сто лет без урожая', N'345672', 1)
GO
INSERT [dbo].[chz_companys] ([chz_company_id], [system_subj_id], [inn], [kpp], [org_name], [ogrn], [chz_company_status_id]) VALUES (5, N'3456', N'34567', N'4567', N'ОАО Мартышкин труд', N'45667', 1)
GO
INSERT [dbo].[chz_companys] ([chz_company_id], [system_subj_id], [inn], [kpp], [org_name], [ogrn], [chz_company_status_id]) VALUES (6, NULL, N'34567', NULL, NULL, NULL, 3)
GO
INSERT [dbo].[chz_companys] ([chz_company_id], [system_subj_id], [inn], [kpp], [org_name], [ogrn], [chz_company_status_id]) VALUES (7, NULL, N'34567', NULL, NULL, NULL, 3)
GO
INSERT [dbo].[chz_companys] ([chz_company_id], [system_subj_id], [inn], [kpp], [org_name], [ogrn], [chz_company_status_id]) VALUES (8, NULL, N'34567', NULL, NULL, NULL, 3)
GO
INSERT [dbo].[chz_companys] ([chz_company_id], [system_subj_id], [inn], [kpp], [org_name], [ogrn], [chz_company_status_id]) VALUES (9, NULL, N'34567', NULL, NULL, NULL, 3)
GO
INSERT [dbo].[chz_companys] ([chz_company_id], [system_subj_id], [inn], [kpp], [org_name], [ogrn], [chz_company_status_id]) VALUES (10, NULL, N'34567', NULL, NULL, NULL, 3)
GO
INSERT [dbo].[chz_companys] ([chz_company_id], [system_subj_id], [inn], [kpp], [org_name], [ogrn], [chz_company_status_id]) VALUES (11, NULL, N'34567', NULL, NULL, NULL, 3)
GO
INSERT [dbo].[chz_companys] ([chz_company_id], [system_subj_id], [inn], [kpp], [org_name], [ogrn], [chz_company_status_id]) VALUES (12, NULL, N'34567', NULL, NULL, NULL, 3)
GO
SET IDENTITY_INSERT [dbo].[chz_companys] OFF
GO
SET IDENTITY_INSERT [dbo].[chz_addresses] ON 
GO
INSERT [dbo].[chz_addresses] ([chz_address_id], [sys_id], [text], [chz_company_id]) VALUES (1, N'45667555', N'Кудыкина гора', 1)
GO
INSERT [dbo].[chz_addresses] ([chz_address_id], [sys_id], [text], [chz_company_id]) VALUES (2, N'456784455', N'Гадюкино', 4)
GO
INSERT [dbo].[chz_addresses] ([chz_address_id], [sys_id], [text], [chz_company_id]) VALUES (3, N'4567883', N'За Мкад', 2)
GO
INSERT [dbo].[chz_addresses] ([chz_address_id], [sys_id], [text], [chz_company_id]) VALUES (6, N'348976', N'Зажопинск', 5)
GO
SET IDENTITY_INSERT [dbo].[chz_addresses] OFF
GO
SET IDENTITY_INSERT [dbo].[chz_nakls] ON 
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (1, 1, 2, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'56775', CAST(N'2020-12-12T00:00:00.000' AS DateTime), 1, 1, 1, N'456778', 3, 1, NULL, CAST(N'2020-12-30T16:28:25.127' AS DateTime), 1, NULL)
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (3, 1, 3, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'567755', CAST(N'2020-12-12T00:00:00.000' AS DateTime), 2, 2, 3, N'34566', 1, 0, 3, NULL, NULL, NULL)
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (4, 1, NULL, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'2929929992', CAST(N'2020-12-12T00:00:00.000' AS DateTime), NULL, 1, 1, N's99939923', 1, 0, 2, NULL, NULL, 1)
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (6, 1, NULL, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'2929929992', CAST(N'2020-12-12T00:00:00.000' AS DateTime), NULL, 1, 1, N's99939923', 1, 0, 2, NULL, NULL, 1)
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (8, 1, NULL, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'2929929992', CAST(N'2020-12-12T00:00:00.000' AS DateTime), NULL, 1, 1, N's99939923', 1, 0, 2, NULL, NULL, 1)
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (9, 1, NULL, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'2929929992', CAST(N'2020-12-12T00:00:00.000' AS DateTime), NULL, 1, 1, N's99939923', 1, 0, 2, NULL, NULL, 1)
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (10, 1, NULL, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'2929929992', CAST(N'2020-12-12T00:00:00.000' AS DateTime), NULL, 1, 1, N's99939923', 1, 0, 2, NULL, NULL, 1)
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (11, 1, NULL, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'2929929992', CAST(N'2020-12-12T00:00:00.000' AS DateTime), NULL, 1, 1, N's99939923', 1, 0, 2, NULL, NULL, 1)
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (12, 1, NULL, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'2929929992', CAST(N'2020-12-12T00:00:00.000' AS DateTime), NULL, 1, 1, N's99939923', 1, 0, 2, NULL, NULL, 1)
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (13, 1, NULL, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'2929929992', CAST(N'2020-12-12T00:00:00.000' AS DateTime), NULL, 1, 1, N's99939923', 1, 0, 2, NULL, NULL, 1)
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (14, 1, NULL, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'2929929992', CAST(N'2020-12-12T00:00:00.000' AS DateTime), NULL, 1, 1, N's99939923', 1, 0, 2, NULL, NULL, 1)
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (15, 1, NULL, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'2929929992', CAST(N'2020-12-12T00:00:00.000' AS DateTime), NULL, 1, 1, N's99939923', 1, 0, 2, NULL, NULL, 1)
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (18, 1, NULL, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'2929929992', CAST(N'2020-12-12T00:00:00.000' AS DateTime), NULL, 1, 1, N's99939923', 1, 0, 2, NULL, NULL, 1)
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (19, 1, NULL, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'2929929992', CAST(N'2020-12-12T00:00:00.000' AS DateTime), NULL, 1, 1, N's99939923', 1, 0, 2, NULL, NULL, 1)
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (20, 1, NULL, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'2929929992', CAST(N'2020-12-12T00:00:00.000' AS DateTime), NULL, 1, 1, N's99939923', 1, 0, 2, NULL, NULL, 1)
GO
INSERT [dbo].[chz_nakls] ([chz_nakl_id], [subject_id], [receiver_id], [operation_date], [doc_num], [doc_date], [turnover_type], [source], [contract_type], [contract_num], [chz_nakl_status_id], [is_direct], [shipper_id], [date_time_finish], [is_trust_accept], [receive_type]) VALUES (21, 1, NULL, CAST(N'2020-12-12T00:00:00.000' AS DateTime), N'2929929992', CAST(N'2020-12-12T00:00:00.000' AS DateTime), NULL, 1, 1, N's99939923', 1, 0, 2, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[chz_nakls] OFF
GO
SET IDENTITY_INSERT [dbo].[chz_gtin_statuses] ON 
GO
INSERT [dbo].[chz_gtin_statuses] ([gtin_status_id], [name], [color]) VALUES (1, N'Еще не проверили', N'#ffff00')
GO
INSERT [dbo].[chz_gtin_statuses] ([gtin_status_id], [name], [color]) VALUES (2, N'Проверили, данные не совпадают', N'#ff0000')
GO
INSERT [dbo].[chz_gtin_statuses] ([gtin_status_id], [name], [color]) VALUES (3, N'Проверили, данные совпадают', N'#008000')
GO
SET IDENTITY_INSERT [dbo].[chz_gtin_statuses] OFF
GO
SET IDENTITY_INSERT [dbo].[chz_nakls_items] ON 
GO
INSERT [dbo].[chz_nakls_items] ([chz_nakl_item_id], [gtin], [prod_sell_name], [chz_nakl_id], [gtin_status_id], [const], [vat_value]) VALUES (1, N'111111', N'Таблетки', 1, 3, NULL, NULL)
GO
INSERT [dbo].[chz_nakls_items] ([chz_nakl_item_id], [gtin], [prod_sell_name], [chz_nakl_id], [gtin_status_id], [const], [vat_value]) VALUES (3, N'1122233', N'Боярышник', 1, 3, 0.0000, 13.0000)
GO
INSERT [dbo].[chz_nakls_items] ([chz_nakl_item_id], [gtin], [prod_sell_name], [chz_nakl_id], [gtin_status_id], [const], [vat_value]) VALUES (4, N'2332233', N'Лекарство', 3, 2, NULL, NULL)
GO
INSERT [dbo].[chz_nakls_items] ([chz_nakl_item_id], [gtin], [prod_sell_name], [chz_nakl_id], [gtin_status_id], [const], [vat_value]) VALUES (6, NULL, N'Таблетки', 1, 3, 23.0000, 13.0000)
GO
INSERT [dbo].[chz_nakls_items] ([chz_nakl_item_id], [gtin], [prod_sell_name], [chz_nakl_id], [gtin_status_id], [const], [vat_value]) VALUES (8, NULL, N'Таблетки', 1, 3, 23.0000, 13.0000)
GO
INSERT [dbo].[chz_nakls_items] ([chz_nakl_item_id], [gtin], [prod_sell_name], [chz_nakl_id], [gtin_status_id], [const], [vat_value]) VALUES (9, NULL, N'Таблетки', 1, 3, 23.0000, 13.0000)
GO
INSERT [dbo].[chz_nakls_items] ([chz_nakl_item_id], [gtin], [prod_sell_name], [chz_nakl_id], [gtin_status_id], [const], [vat_value]) VALUES (10, NULL, N'Таблетки', 1, 3, 23.0000, 13.0000)
GO
INSERT [dbo].[chz_nakls_items] ([chz_nakl_item_id], [gtin], [prod_sell_name], [chz_nakl_id], [gtin_status_id], [const], [vat_value]) VALUES (11, NULL, N'Таблетки', 1, 3, 23.0000, 13.0000)
GO
INSERT [dbo].[chz_nakls_items] ([chz_nakl_item_id], [gtin], [prod_sell_name], [chz_nakl_id], [gtin_status_id], [const], [vat_value]) VALUES (12, NULL, N'Таблетки', 1, 3, 23.0000, 13.0000)
GO
INSERT [dbo].[chz_nakls_items] ([chz_nakl_item_id], [gtin], [prod_sell_name], [chz_nakl_id], [gtin_status_id], [const], [vat_value]) VALUES (13, NULL, N'Таблетки', 1, 3, 23.0000, 13.0000)
GO
INSERT [dbo].[chz_nakls_items] ([chz_nakl_item_id], [gtin], [prod_sell_name], [chz_nakl_id], [gtin_status_id], [const], [vat_value]) VALUES (14, NULL, N'Таблетки', 1, 3, 23.0000, 13.0000)
GO
INSERT [dbo].[chz_nakls_items] ([chz_nakl_item_id], [gtin], [prod_sell_name], [chz_nakl_id], [gtin_status_id], [const], [vat_value]) VALUES (16, NULL, N'Таблетки', 1, 3, 0.0000, 13.0000)
GO
INSERT [dbo].[chz_nakls_items] ([chz_nakl_item_id], [gtin], [prod_sell_name], [chz_nakl_id], [gtin_status_id], [const], [vat_value]) VALUES (17, NULL, N'Таблетки', 1, 3, 0.0000, 13.0000)
GO
INSERT [dbo].[chz_nakls_items] ([chz_nakl_item_id], [gtin], [prod_sell_name], [chz_nakl_id], [gtin_status_id], [const], [vat_value]) VALUES (18, NULL, N'Таблетки', 1, 3, 0.0000, 13.0000)
GO
INSERT [dbo].[chz_nakls_items] ([chz_nakl_item_id], [gtin], [prod_sell_name], [chz_nakl_id], [gtin_status_id], [const], [vat_value]) VALUES (19, NULL, N'Таблетки', 1, NULL, 0.0000, 13.0000)
GO
SET IDENTITY_INSERT [dbo].[chz_nakls_items] OFF
GO
SET IDENTITY_INSERT [dbo].[chz_sgtins] ON 
GO
INSERT [dbo].[chz_sgtins] ([chz_sgtin_id], [chz_nakl_item_id], [is_scened], [cost], [vat_value], [sgtin]) VALUES (1, 1, 0, 10.0000, 5.0000, N'566555')
GO
INSERT [dbo].[chz_sgtins] ([chz_sgtin_id], [chz_nakl_item_id], [is_scened], [cost], [vat_value], [sgtin]) VALUES (3, 3, 0, 10.0000, 2.0000, N'655566')
GO
INSERT [dbo].[chz_sgtins] ([chz_sgtin_id], [chz_nakl_item_id], [is_scened], [cost], [vat_value], [sgtin]) VALUES (4, 3, 0, 1000.0000, 5.0000, N'556655')
GO
INSERT [dbo].[chz_sgtins] ([chz_sgtin_id], [chz_nakl_item_id], [is_scened], [cost], [vat_value], [sgtin]) VALUES (5, 3, 0, 134455.0000, 567.0000, N'556655')
GO
INSERT [dbo].[chz_sgtins] ([chz_sgtin_id], [chz_nakl_item_id], [is_scened], [cost], [vat_value], [sgtin]) VALUES (7, 4, 0, 455666.0000, 4566.0000, N'56677')
GO
INSERT [dbo].[chz_sgtins] ([chz_sgtin_id], [chz_nakl_item_id], [is_scened], [cost], [vat_value], [sgtin]) VALUES (8, 4, 0, 566445.0000, 677555.0000, N'5667')
GO
SET IDENTITY_INSERT [dbo].[chz_sgtins] OFF
GO
SET IDENTITY_INSERT [dbo].[chz_users] ON 
GO
INSERT [dbo].[chz_users] ([chz_user_id], [login], [password], [access_token], [chz_company_id], [is_lock], [first_name], [last_name], [date_time]) VALUES (1, N'admin', N'pas', N'111111', 1, 0, NULL, NULL, CAST(N'2020-12-30T15:32:01.897' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[chz_users] OFF
GO
SET IDENTITY_INSERT [dbo].[chz_nds_values] ON 
GO
INSERT [dbo].[chz_nds_values] ([chz_nds_value_id], [value], [is_default]) VALUES (2, 0, 1)
GO
INSERT [dbo].[chz_nds_values] ([chz_nds_value_id], [value], [is_default]) VALUES (3, 10, 0)
GO
INSERT [dbo].[chz_nds_values] ([chz_nds_value_id], [value], [is_default]) VALUES (4, 18, 0)
GO
INSERT [dbo].[chz_nds_values] ([chz_nds_value_id], [value], [is_default]) VALUES (5, 20, 0)
GO
SET IDENTITY_INSERT [dbo].[chz_nds_values] OFF
GO
