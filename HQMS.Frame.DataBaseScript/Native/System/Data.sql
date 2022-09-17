/*****************************************************
System_PathSetting      程序路径设置
******************************************************/
INSERT INTO System_PathSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '01','ApplictionCatalogue','E:\Work\Code\HQMS\HQMS\bin\Debug\','程序运行目录',1,True
WHERE NOT EXISTS (SELECT 1 FROM System_PathSetting WHERE Code='01')

INSERT INTO System_PathSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '02','NativeDataBaseFilePath','E:\Work\Code\HQMS\HQMS\bin\Debug\HQMS.db','本地数据库文件路径',2,True
WHERE NOT EXISTS (SELECT 1 FROM System_PathSetting WHERE Code='02')

/*****************************************************
System_LogSetting      程序日志设置
******************************************************/
INSERT INTO System_LogSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '01','TextLog','E:\Work\Code\HQMS\HQMS\bin\Debug\HQMS.txt','文本日志',1,True
WHERE NOT EXISTS (SELECT 1 FROM System_LogSetting WHERE Code='01')

/*****************************************************
System_DataBaseSetting   程序数据库设置
******************************************************/
INSERT INTO System_DataBaseSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '01','Native','9e1be3076bcd383eddf7ff153f1eb6504157ba8cb94ac7a3c08c3bd54a530cb814083846ef00090aa604ff4af8e15a685e0a6c447877846424d1459b0668807a',
        '本地',1,True
WHERE NOT EXISTS (SELECT 1 FROM System_DataBaseSetting WHERE Code='01')

INSERT INTO System_DataBaseSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '02','BAGLDB','2b2b865988ae519bf4f89acfc286b0fabdb416d908e087a4082c230debf31fb37b57605125d472b290d1fba06a2c658bc1a8d200124810a4e802be427179ff89df85fdc671670e4a6c09ae3154399dd171b2c7f657eb7684e27d8955f231832e',
        '病案管理',2,True
WHERE NOT EXISTS (SELECT 1 FROM System_DataBaseSetting WHERE Code='02')

/*****************************************************
System_ModuleSetting      程序框架模块设置
******************************************************/
INSERT INTO System_ModuleSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '01','ServiceModule','HQMS.Frame.Service.dll',
        'HQMS.Frame.Service.ServiceModule, HQMS.Frame.Service, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
        	1,True
WHERE NOT EXISTS (SELECT 1 FROM System_ModuleSetting WHERE Code='01')

INSERT INTO System_ModuleSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '02','LoginModule','HQMS.Frame.Control.Login.dll',
        'HQMS.Frame.Control.Login.LoginModule, HQMS.Frame.Control.Login, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
        	2,True
WHERE NOT EXISTS (SELECT 1 FROM System_ModuleSetting WHERE Code='02')