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

INSERT INTO System_ModuleSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '03','MainHeaderModule','HQMS.Frame.Control.MainHeader.dll',
        'HQMS.Frame.Control.MainHeader.MainHeaderModule, HQMS.Frame.Control.MainHeader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
        	3,True
WHERE NOT EXISTS (SELECT 1 FROM System_ModuleSetting WHERE Code='03')

INSERT INTO System_ModuleSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '04','MainLeftDrawerModule','HQMS.Frame.Control.MainLeftDrawer.dll',
        'HQMS.Frame.Control.MainLeftDrawer.MainLeftDrawerModule, HQMS.Frame.Control.MainLeftDrawer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
        	4,True
WHERE NOT EXISTS (SELECT 1 FROM System_ModuleSetting WHERE Code='04')

/*****************************************************
System_ServiceEventSetting           程序框架服务设置
******************************************************/
INSERT INTO System_ServiceEventSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '01','AccountAuthenticationService','','用户认证服务',1,True
WHERE NOT EXISTS (SELECT 1 FROM System_ServiceEventSetting WHERE Code='01')

INSERT INTO System_ServiceEventSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '02','ApplicationStatusService','','程序状态服务',2,True
WHERE NOT EXISTS (SELECT 1 FROM System_ServiceEventSetting WHERE Code='02')

INSERT INTO System_ServiceEventSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '03','MenuListService','','菜单清单服务',3,True
WHERE NOT EXISTS (SELECT 1 FROM System_ServiceEventSetting WHERE Code='03')

INSERT INTO System_ServiceEventSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '04','MenuItemService','','菜单项目服务',4,True
WHERE NOT EXISTS (SELECT 1 FROM System_ServiceEventSetting WHERE Code='04')

/*****************************************************
System_MenuSetting                    程序菜单设置
******************************************************/
INSERT INTO System_MenuSetting (Code,Name, ReferName,Content,Description,SuperCode,SuperName,Rank,Flag)
SELECT '01','ApplictionConfiguration','系统设置','','','','', 1,True
WHERE NOT EXISTS (SELECT 1 FROM System_MenuSetting WHERE Code='01')

INSERT INTO System_MenuSetting (Code,Name,ReferName,Content,Description,SuperCode,SuperName,Rank,Flag)
SELECT '0101','MenuConfigurationModule','菜单设置','HQMS.Control.Extension.MenuConfiguration.dll',
        'HQMS.Control.Extension.MenuConfiguration.MenuConfigurationModule, HQMS.Control.Extension.MenuConfiguration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
        '01','系统设置',1,True
WHERE NOT EXISTS (SELECT 1 FROM System_MenuSetting WHERE Code='0101')

INSERT INTO System_MenuSetting (Code,Name, ReferName,Content,Description,SuperCode,SuperName,Rank,Flag)
SELECT '02','PerformanceAssessment','绩效考核','','','','', 2,True
WHERE NOT EXISTS (SELECT 1 FROM System_MenuSetting WHERE Code='02')

INSERT INTO System_MenuSetting (Code,Name,ReferName,Content,Description,SuperCode,SuperName,Rank,Flag)
SELECT '0201','PerformanceAssessmentConfigurationModule','绩效考核设置','HQMS.Control.Extension.PerformanceAssessmentConfiguration.dll',
        'HQMS.Control.Extension.PerformanceAssessmentConfiguration.PerformanceAssessmentConfigurationModule, HQMS.Control.Extension.PerformanceAssessmentConfiguration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
        '02','绩效考核',1,True
WHERE NOT EXISTS (SELECT 1 FROM System_MenuSetting WHERE Code='0201')

INSERT INTO System_MenuSetting (Code,Name,ReferName,Content,Description,SuperCode,SuperName,Rank,Flag)
SELECT '0202','PerformanceAssessmentModule','绩效考核','HQMS.Control.Extension.PerformanceAssessment.dll',
        'HQMS.Control.Extension.PerformanceAssessment.PerformanceAssessmentModule, HQMS.Control.Extension.PerformanceAssessment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
        '02','绩效考核',2,True
WHERE NOT EXISTS (SELECT 1 FROM System_MenuSetting WHERE Code='0202')

/*****************************************************
HQMS_PerformanceAssessment_MenuSetting         绩效考核菜单设置
******************************************************/
INSERT INTO HQMS_PerformanceAssessment_MenuSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '01','DataMapping','数据匹配','', 1,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_MenuSetting WHERE Code='01')

INSERT INTO HQMS_PerformanceAssessment_MenuSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '02','DataQuerying','数据查询','', 2,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_MenuSetting WHERE Code='02')

INSERT INTO HQMS_PerformanceAssessment_MenuSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '03','DataExporting','数据导出','', 3,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_MenuSetting WHERE Code='03')