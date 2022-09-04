/*****************************************************
System_DataBaseSetting
******************************************************/

drop table if exists System_DataBaseSetting;

CREATE TABLE System_DataBaseSetting 
(
    Code VARCHAR primary key, 
    Name VARCHAR, 
    Content VARCHAR, 
    Description VARCHAR, 
    Rank INT, 
    Flag BOOLEAN
);

INSERT INTO System_DataBaseSetting (Code,Name,Content,Description,Rank,Flag)
VALUES ('01','Native','2b2b865988ae519bf4f89acfc286b0fabdb416d908e087a4082c230debf31fb37b57605125d472b290d1fba06a2c658bc1a8d200124810a4e802be427179ff89df85fdc671670e4a6c09ae3154399dd171b2c7f657eb7684e27d8955f231832e',
        '本地数据库',1,1);

INSERT INTO System_DataBaseSetting (Code,Name,Content,Description,Rank,Flag)
VALUES ('02','BAGLDB','2b2b865988ae519bf4f89acfc286b0fabdb416d908e087a4082c230debf31fb37b57605125d472b290d1fba06a2c658bc1a8d200124810a4e802be427179ff89df85fdc671670e4a6c09ae3154399dd171b2c7f657eb7684e27d8955f231832e',
        '病案管理数据库',2,1);