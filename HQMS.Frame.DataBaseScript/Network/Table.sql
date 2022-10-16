if OBJECT_ID('T_Map_Mapping','U') is not NULL
drop table T_Map_Mapping
go

create table T_Map_Mapping
(
    ID	             int identity (1,1),
    CategoryID	     varchar(50),
    CategoryName	 nvarchar(400),
    Code_Standard	 varchar(50),
    Value_Standard	 nvarchar(400),
    Code_His	     varchar(50),
    Value_His	     nvarchar(800)
)
go

insert into T_Map_Mapping (CategoryID,CategoryName,Code_Standard,Value_Standard,Code_His,Value_His)
select CategoryID,CategoryName,Code_Standard,Value_Standard,Code_His,Value_His from T_Map_Mapping_221016
