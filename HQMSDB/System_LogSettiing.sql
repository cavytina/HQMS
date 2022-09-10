/*****************************************************
System_LogSetting  程序日志设置
******************************************************/

drop table if exists System_LogSetting;

CREATE TABLE System_LogSetting 
(
    Code VARCHAR primary key,      --日志代码
    Name VARCHAR,                  --日志名称
    Content VARCHAR,               --日志路径设置值
    Description VARCHAR,           --日志描述
    Rank INT,                      --排序
    Flag BOOLEAN                   --是否启用
);

SELECT * from System_LogSetting
INSERT INTO System_LogSetting (Code,Name,Content,Description,Rank,Flag) VALUES ('01','TextLog','E:\Work\Code\HQMS\HQMS\bin\Debug\HQMS.txt','文本日志',1,True)
