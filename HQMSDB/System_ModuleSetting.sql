/*****************************************************
System_ModuleSetting
******************************************************/

drop table if exists System_ModuleSetting;

CREATE TABLE System_ModuleSetting 
(
    Code VARCHAR primary key, 
    Name VARCHAR, 
    Content VARCHAR, 
    Description VARCHAR, 
    Rank INT, 
    Flag BOOLEAN
);

INSERT INTO System_ModuleSetting (Code,Name,Content,Description,Rank,Flag)
VALUES ('01','ModuleLauncherModule','HQMS.Frame.ModuleLauncher.dll',
        'HQMS.Frame.ModuleLauncher.ModuleLauncherModule, HQMS.Frame.ModuleLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
        	1,True);

INSERT INTO System_ModuleSetting (Code,Name,Content,Description,Rank,Flag)
VALUES ('02','ServiceLauncherModule','HQMS.Frame.ServiceLauncher.dll',
        'HQMS.Frame.ServiceLauncher.ServiceLauncherModule, HQMS.Frame.ServiceLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
        	2,True);