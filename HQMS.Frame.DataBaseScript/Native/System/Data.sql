/*****************************************************
System_AuthorizationSetting      程序授权设置
******************************************************/
INSERT INTO System_AuthorizationSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '314595A6-08F8-4047-A365-2684537DC92A','LicenseKey','3E09638219CEBC19','程序验证授权码',1,True
WHERE NOT EXISTS (SELECT 1 FROM System_AuthorizationSetting WHERE name='LicenseKey')

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
SELECT '01','Native','ygXZxSoEX3Yqbt9Xpc0g7iLxlWF6rOfl2jFgtFHlWo9caQv222cOHcrVH/7WnVTlM2JLoRYHykgK74DnDPk+XmjopQyMmPP+3s8vz+8F7yWgcXCTGqm0PV6uSm8v8ruVlYUY7enpAGWsUxtaxCIfHA==',
        '本地',1,True
WHERE NOT EXISTS (SELECT 1 FROM System_DataBaseSetting WHERE Code='01')

INSERT INTO System_DataBaseSetting (Code,Name,Content,Description,Rank,Flag)
SELECT '02','BAGLDB','0/E0Mtl32slVw8LaAD3yPkisBX0b9SmzNmg/Ii3iYSJAuwv/UDWzoHWE3qP81uspvl3K6r8tymujeg8UK/a7Zw==',
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
HQMS_PerformanceAssessment_DictionarySetting         绩效考核字典数据设置
******************************************************/
INSERT INTO HQMS_PerformanceAssessment_DictionarySetting (Code,Name,Content,Description,CategoryCode,CategoryName,Rank,Flag)
SELECT '0101','DataMapping','数据匹配','','01','菜单设置',1,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_DictionarySetting WHERE Code='0101')

INSERT INTO HQMS_PerformanceAssessment_DictionarySetting (Code,Name,Content,Description,CategoryCode,CategoryName,Rank,Flag)
SELECT '0102','DataQuerying','数据查询','','01','菜单设置', 2,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_DictionarySetting WHERE Code='0102')

INSERT INTO HQMS_PerformanceAssessment_DictionarySetting (Code,Name,Content,Description,CategoryCode,CategoryName,Rank,Flag)
SELECT '0103','DataExporting','数据导出','','01','菜单设置', 3,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_DictionarySetting WHERE Code='0103')

INSERT INTO HQMS_PerformanceAssessment_DictionarySetting (Code,Name,Content,Description,CategoryCode,CategoryName,Rank,Flag)
SELECT '0201','January','一月','','02','月份设置',1,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_DictionarySetting WHERE Code='0201')

INSERT INTO HQMS_PerformanceAssessment_DictionarySetting (Code,Name,Content,Description,CategoryCode,CategoryName,Rank,Flag)
SELECT '0202','February','二月','','02','月份设置', 2,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_DictionarySetting WHERE Code='0202')

INSERT INTO HQMS_PerformanceAssessment_DictionarySetting (Code,Name,Content,Description,CategoryCode,CategoryName,Rank,Flag)
SELECT '0203','March','三月','','02','月份设置', 3,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_DictionarySetting WHERE Code='0203')

INSERT INTO HQMS_PerformanceAssessment_DictionarySetting (Code,Name,Content,Description,CategoryCode,CategoryName,Rank,Flag)
SELECT '0204','April','四月','','02','月份设置', 4,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_DictionarySetting WHERE Code='0204')

INSERT INTO HQMS_PerformanceAssessment_DictionarySetting (Code,Name,Content,Description,CategoryCode,CategoryName,Rank,Flag)
SELECT '0205','May','五月','','02','月份设置', 5,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_DictionarySetting WHERE Code='0205')

INSERT INTO HQMS_PerformanceAssessment_DictionarySetting (Code,Name,Content,Description,CategoryCode,CategoryName,Rank,Flag)
SELECT '0206','June','六月','','02','月份设置', 6,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_DictionarySetting WHERE Code='0206')

INSERT INTO HQMS_PerformanceAssessment_DictionarySetting (Code,Name,Content,Description,CategoryCode,CategoryName,Rank,Flag)
SELECT '0207','July','七月','','02','月份设置', 7,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_DictionarySetting WHERE Code='0207')

INSERT INTO HQMS_PerformanceAssessment_DictionarySetting (Code,Name,Content,Description,CategoryCode,CategoryName,Rank,Flag)
SELECT '0208','August','八月','','02','月份设置', 8,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_DictionarySetting WHERE Code='0208')

INSERT INTO HQMS_PerformanceAssessment_DictionarySetting (Code,Name,Content,Description,CategoryCode,CategoryName,Rank,Flag)
SELECT '0209','September','九月','','02','月份设置', 9,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_DictionarySetting WHERE Code='0209')

INSERT INTO HQMS_PerformanceAssessment_DictionarySetting (Code,Name,Content,Description,CategoryCode,CategoryName,Rank,Flag)
SELECT '0210','October','十月','','02','月份设置', 10,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_DictionarySetting WHERE Code='0210')

INSERT INTO HQMS_PerformanceAssessment_DictionarySetting (Code,Name,Content,Description,CategoryCode,CategoryName,Rank,Flag)
SELECT '0211','November','十一月','','02','月份设置', 11,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_DictionarySetting WHERE Code='0211')

INSERT INTO HQMS_PerformanceAssessment_DictionarySetting (Code,Name,Content,Description,CategoryCode,CategoryName,Rank,Flag)
SELECT '0212','December','十二月','','02','月份设置', 12,True
WHERE NOT EXISTS (SELECT 1 FROM HQMS_PerformanceAssessment_DictionarySetting WHERE Code='0212')