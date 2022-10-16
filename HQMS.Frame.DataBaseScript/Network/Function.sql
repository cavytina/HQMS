if OBJECT_ID('TF_HQMS_LoadCategoryInfo','TF') is not null
drop function TF_HQMS_LoadCategoryInfo
go

create function TF_HQMS_LoadCategoryInfo
(
	@Type            varchar(10),
	@CategoryCode    varchar(50)
)
returns @ret table
(
	CategoryCode     varchar(50),
	CategoryName     varchar(100),
	LocalCode        varchar(50),
	LocalName        varchar(400),
	StandardCode     varchar(50),
	StandardName     varchar(400)
)
as

begin

if @Type='Category'
begin
	insert into @ret (CategoryCode,CategoryName)
	select distinct FCODE,FDESCRIBE from dbo.TSTANDARDMAIN (nolock)
end
else if @Type='Local'
begin
	insert into @ret (CategoryCode,CategoryName,LocalCode,LocalName)
	select a.FCODE,a.FDESCRIBE,b.FBH,b.FMC from TSTANDARDMAIN a (nolock)
		inner join TSTANDARDMX b (nolock) ON a.FCODE=b.FCODE
	where a.FCODE = @CategoryCode
	and not exists (select 1 from T_Map_Mapping x (nolock) where b.FCODE=x.CategoryID AND b.FBH=x.Code_His)
end
else if @Type='Standard'
begin
	insert into @ret (CategoryCode,CategoryName,StandardCode,StandardName)
	select a.CategoryID,a.CategoryName,a.Code,a.Value from T_Map_Standard a (nolock)
	where a.CategoryID = @CategoryCode
	and not exists (select 1 from T_Map_Mapping x (nolock) where a.CategoryID=x.CategoryID and a.Code=x.Code_Standard)
end
else if @Type='Matched'
begin
	insert into @ret (CategoryCode,CategoryName,LocalCode,LocalName,StandardCode,StandardName)
	select CategoryID,CategoryName,Code_His,Value_His,Code_Standard,Value_Standard from T_Map_Mapping (nolock)
	where CategoryID = @CategoryCode
end

return

end
go