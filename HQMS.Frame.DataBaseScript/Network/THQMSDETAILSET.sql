IF OBJECT_ID('THQMSDETAILSET','U') IS NOT NULL
DROP TABLE THQMSDETAILSET
GO

CREATE TABLE THQMSDETAILSET
(
	FID               BIGINT     IDENTITY(1,1),
	FREPORTDATESTR	  VARCHAR(255),
	FCODE             VARCHAR(50),
	FNAME             VARCHAR(255),
	A48               VARCHAR(50),       --住院号
	A11               VARCHAR(50),       --姓名
	A14               VARCHAR(50),       --年龄（岁）
	B12               VARCHAR(50),       --入院时间
	B15               VARCHAR(50),       --出院时间
	B20               VARCHAR(50),       --实际住院（天）
	C03C              VARCHAR(50),       --出院主要诊断编码
	C04N              VARCHAR(250),      --出院主要诊断名称
	C14x01C           VARCHAR(50),       --主要手术操作编码
	C15x01N           VARCHAR(250),      --主要手术操作名称
	C16x01            VARCHAR(50),       --主要手术操作日期
	C17x01            VARCHAR(50),       --主要手术操作级别
	C18x01            VARCHAR(50),       --主要手术操作术者
	C21x01C           VARCHAR(50),       --主要手术操作切口愈合等级
	C22x01C           VARCHAR(50),       --主要手术操作麻醉方式
	B13C              VARCHAR(50),       --入院科别
	B16C              VARCHAR(50),       --出院科别
	B34C              VARCHAR(10),       --离院方式 
	D01               VARCHAR(20)        --住院总费用
)

GO
