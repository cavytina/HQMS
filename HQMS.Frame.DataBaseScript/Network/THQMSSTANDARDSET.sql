IF OBJECT_ID('THQMSSTANDARDSET','U') IS NOT NULL
DROP TABLE THQMSSTANDARDSET
GO

CREATE TABLE THQMSSTANDARDSET
(
    ID	             int identity (1,1),
    CategoryID	     varchar(50),
    CategoryName	 nvarchar(400),
	Code			 VARCHAR(50),
	Value		     nvarchar(400)
)

GO