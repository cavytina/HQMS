/*****************************************************
System_PathSetting                程序框架路径设置
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

/*****************************************************
System_LogSetting                  程序框架日志设置
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

/*****************************************************
System_DataBaseSetting              程序框架数据库设置
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

/*****************************************************
System_ModuleSetting                程序框架模块设置
******************************************************/

drop table if exists System_ModuleSetting;

CREATE TABLE System_ModuleSetting
(
    Code VARCHAR primary key,       --模块代码
    Name VARCHAR,                   --模块名称
    Content VARCHAR,                --模块引用路径
    Description VARCHAR,            --模块类型
    Rank INT,                       --模块启动顺序
    Flag BOOLEAN                    --是否启用
);


/*****************************************************
System_ServiceEventSetting           程序框架服务设置
******************************************************/

drop table if exists System_ServiceEventSetting;

CREATE TABLE System_ServiceEventSetting
(
    Code VARCHAR primary key,       --服务代码
    Name VARCHAR,                   --服务名称
    Content VARCHAR,                --保留字段
    Description VARCHAR,            --服务描述
    Rank INT,                       --排序
    Flag BOOLEAN                    --是否启用
);

/*****************************************************
System_MenuSetting           程序菜单设置
******************************************************/

drop table if exists System_MenuSetting;

CREATE TABLE System_MenuSetting
(
    Code VARCHAR primary key,       --菜单代码
    Name VARCHAR,                   --菜单名称
    ReferName VARCHAR,              --中文菜单名称
    Content VARCHAR,                --菜单路径
    Description VARCHAR,            --菜单类型
    SuperCode   VARCHAR,            --上级菜单代码
    SuperName   VARCHAR,            --上级菜单名称
    Rank INT,                       --排序
    Flag BOOLEAN                    --是否启用
);

/*****************************************************
HQMS_PerformanceAssessment_DictionarySetting         绩效考核基础数据设置
******************************************************/

drop table if exists HQMS_PerformanceAssessment_DictionarySetting;

CREATE TABLE HQMS_PerformanceAssessment_DictionarySetting
(
    Code VARCHAR primary key,       --菜单代码
    Name VARCHAR,                   --菜单名称
    Content VARCHAR,                --保留
    Description VARCHAR,            --保留
    CategoryCode  VARCHAR,          --类别代码
    CategoryName  VARCHAR,          --类别名称
    Rank INT,                       --排序
    Flag BOOLEAN                    --是否启用
);