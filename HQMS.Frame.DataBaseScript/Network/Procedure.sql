if OBJECT_ID('USP_HQMS_MatchCategoryInfo','P') is not null
drop proc USP_HQMS_MatchCategoryInfo
go

create proc USP_HQMS_MatchCategoryInfo
@Type             varchar(10),
@CategoryCode     varchar(50),
@CategoryName     varchar(50),
@LocalCode        varchar(50),
@LocalName        varchar(400),
@StandardCode     varchar(50),
@StandardName     varchar(400)
as

set nocount on

if @Type='Match'
begin
	insert into dbo.T_Map_Mapping (CategoryID,CategoryName,Code_Standard,Value_Standard,Code_His,Value_His)
	values (@CategoryCode,@CategoryName,@StandardCode,@StandardName,@LocalCode,@LocalName)

	if @@ERROR=0
		select 'T','匹配成功!'
	else
		select 'F','匹配失败!'
end
else if @Type='UnMatch'
begin
	delete from dbo.T_Map_Mapping where CategoryID=@CategoryCode and Code_Standard=@StandardCode and Code_His=@LocalCode

	if @@ERROR=0
		select 'T','取消匹配成功!'
	else
		select 'F','取消匹配失败!'
end

return