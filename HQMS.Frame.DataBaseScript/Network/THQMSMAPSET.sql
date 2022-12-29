IF OBJECT_ID('THQMSMAPSET','U') IS NOT NULL
DROP TABLE THQMSMAPSET
GO

CREATE TABLE THQMSMAPSET
(
    ID	             int identity (1,1),
    CategoryID	     varchar(50),
    CategoryName	 nvarchar(400),
    Code_Standard	 varchar(50),
    Value_Standard	 nvarchar(400),
    Code_His	     varchar(50),
    Value_His	     nvarchar(800)
)

GO