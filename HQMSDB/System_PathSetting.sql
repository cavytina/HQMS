/*****************************************************
System_PathSetting
******************************************************/

drop table if exists System_PathSetting;

CREATE TABLE System_PathSetting 
(
    Code VARCHAR primary key, 
    Name VARCHAR, 
    Content VARCHAR, 
    Description VARCHAR, 
    Rank INT, 
    Flag BOOLEAN
);