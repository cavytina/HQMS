/*****************************************************
System_PathSetting  程序运行路径设置
******************************************************/

drop table if exists System_PathSetting;

CREATE TABLE System_PathSetting 
(
    Code VARCHAR primary key,      --路径代码
    Name VARCHAR,                  --路径名称
    Content VARCHAR,               --路径设置值
    Description VARCHAR,           --路径描述
    Rank INT,                      --排序
    Flag BOOLEAN                   --是否启用
);

INSERT INTO System_PathSetting (Code,Name,Content,Description,Rank,Flag)
VALUES ('01','ApplictionCatalogue','E:\Work\Code\HQMS\HQMS\bin\Debug\','程序运行目录',1,True)

INSERT INTO System_PathSetting (Code,Name,Content,Description,Rank,Flag)
VALUES ('02','NativeDataBaseFilePath','E:\Work\Code\HQMS\HQMS\bin\Debug\HQMS.db','本地数据库文件路径',2,True)
