/*****************************************************
System_DataBaseSetting   程序数据库设置
******************************************************/

drop table if exists System_DataBaseSetting;

CREATE TABLE System_DataBaseSetting 
(
    Code VARCHAR primary key,      --数据库代码
    Name VARCHAR,                  --数据库名称
    Content VARCHAR,               --数据库连接字符串设置(密文)
    Description VARCHAR,           --数据库描述
    Rank INT,                      --排序
    Flag BOOLEAN                   --是否启用
);

INSERT INTO System_DataBaseSetting (Code,Name,Content,Description,Rank,Flag)
VALUES ('01','Native','9e1be3076bcd383eddf7ff153f1eb6504157ba8cb94ac7a3c08c3bd54a530cb814083846ef00090aa604ff4af8e15a685e0a6c447877846424d1459b0668807a',
        '本地数据库',1,True);

INSERT INTO System_DataBaseSetting (Code,Name,Content,Description,Rank,Flag)
VALUES ('02','BAGLDB','2b2b865988ae519bf4f89acfc286b0fabdb416d908e087a4082c230debf31fb37b57605125d472b290d1fba06a2c658bc1a8d200124810a4e802be427179ff89df85fdc671670e4a6c09ae3154399dd171b2c7f657eb7684e27d8955f231832e',
        '病案管理数据库',2,True);
