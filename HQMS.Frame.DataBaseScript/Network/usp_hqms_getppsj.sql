IF OBJECT_ID('usp_hqms_getppsj','P') IS NOT NULL
DROP PROC usp_hqms_getppsj
GO

CREATE PROC usp_hqms_getppsj
@Type            varchar(10),
@CategoryCode    varchar(50),
@CategoryName     varchar(50),
@LocalCode        varchar(50),
@LocalName        varchar(400),
@StandardCode     varchar(50),
@StandardName     varchar(400)
AS

SET NOCOUNT ON

if @Type='Match'
begin
	insert into dbo.THQMSMAPSET (CategoryID,CategoryName,Code_Standard,Value_Standard,Code_His,Value_His)
	values (@CategoryCode,@CategoryName,@StandardCode,@StandardName,@LocalCode,@LocalName)

	if @@ERROR=0
		select 'T'
	else
		select 'F','∆•≈‰ ß∞‹!'
end
else if @Type='UnMatch'
begin
	delete from dbo.THQMSMAPSET where CategoryID=@CategoryCode and Code_Standard=@StandardCode and Code_His=@LocalCode

	if @@ERROR=0
		select 'T'
	else
		select 'F','»°œ˚∆•≈‰ ß∞‹!'
end

return