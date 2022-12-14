IF OBJECT_ID('usp_hqms_gettjsj','P') IS NOT NULL
DROP PROC usp_hqms_gettjsj
GO

CREATE PROC usp_hqms_gettjsj
@ksrq        VARCHAR(20),
@jsrq        VARCHAR(20),
@ishz        BIT,             --是否汇总查询  1是0否      
@category    varchar(50)=''
AS

BEGIN

SET NOCOUNT ON

DECLARE @freportdatestr VARCHAR(100)
SELECT @freportdatestr=dbo.fn_Convertdatestr(@ksrq,@jsrq)

IF NOT EXISTS (SELECT 1 FROM dbo.THQMSSET (NOLOCK) WHERE FREPORTDATESTR = @freportdatestr)
BEGIN
	SELECT 'F','当月绩效数据未生成，请生成后重试!'
	RETURN
END

IF @ishz=0
BEGIN
	IF NOT EXISTS (SELECT 1 FROM THQMSMASTERSET (NOLOCK) WHERE FREPORTDATESTR = @freportdatestr)
	BEGIN
		SELECT 'F','当月汇总数据未生成，请生成后重试!'
		RETURN
	END
END

SELECT  * INTO    #THQMSSET FROM    dbo.THQMSSET a (NOLOCK)  WHERE   FREPORTDATESTR = @freportdatestr

IF NOT EXISTS (SELECT 1 FROM THQMSDETAILSET WHERE   FREPORTDATESTR = @freportdatestr )
BEGIN
	--生成明细
	--出院患者总人次数
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT FREPORTDATESTR,'CYHZZRC','出院患者总人次数',
			A48,A11,A14,B12,B15,B20,
				C03C,C04N,
				C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
				B13C,B16C,B34C,
				CONVERT(VARCHAR(20),D01)
				FROM #THQMSSET

	--出院患者总手术人数
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'CYHZZSSRC','出院患者总手术人数',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a                 
	WHERE  ( A14 > 1 OR ISNULL(A14, 0) = 0	AND A16 > 28 )
		AND ( EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C14x01C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x01C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x02C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x03C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x04C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x05C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x06C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x07C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x08C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x09C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x10C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗')) )

	--出院患者总手术死亡人数
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'CYHZZSSSWRC','出院患者总手术死亡人数',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a                 
	WHERE  ( A14 > 1 OR ISNULL(A14, 0) = 0	AND A16 > 28 )
	AND a.B34C = 5
	AND ( EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C14x01C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
	OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x01C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
	OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x02C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
	OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x03C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
	OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x04C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
	OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x05C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
	OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x06C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
	OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x07C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
	OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x08C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
	OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x09C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
	OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x10C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗')) )

	--出院微创手术人数
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'CYWCSSRC','出院微创手术人数',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a                 
	WHERE  ( A14 > 1 OR ISNULL(A14, 0) = 0	AND A16 > 28 )
		AND ( EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C14x01C=b.FOPCODE AND b.fgjwcbz='是')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x01C=b.FOPCODE AND b.fgjwcbz='是')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x02C=b.FOPCODE AND b.fgjwcbz='是')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x03C=b.FOPCODE AND b.fgjwcbz='是')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x04C=b.FOPCODE AND b.fgjwcbz='是')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x05C=b.FOPCODE AND b.fgjwcbz='是')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x06C=b.FOPCODE AND b.fgjwcbz='是')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x07C=b.FOPCODE AND b.fgjwcbz='是')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x08C=b.FOPCODE AND b.fgjwcbz='是')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x09C=b.FOPCODE AND b.fgjwcbz='是')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x10C=b.FOPCODE AND b.fgjwcbz='是') )

	--出院日间手术人数
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'CYRJSSRC','出院日间手术人数',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a                 
	WHERE  ( A14 > 1 OR ISNULL(A14, 0) = 0	AND A16 > 28 )
	AND  ( EXISTS (SELECT 1 FROM dbo.TSTANDARDMX b (NOLOCK) WHERE a.C14x01C=b.FZJC AND b.FCODE='GBRJSSXX')
	OR EXISTS (SELECT 1 FROM dbo.TSTANDARDMX b (NOLOCK) WHERE a.C35x01C=b.FZJC AND b.FCODE='GBRJSSXX')
	OR EXISTS (SELECT 1 FROM dbo.TSTANDARDMX b (NOLOCK) WHERE a.C35x02C=b.FZJC AND b.FCODE='GBRJSSXX')
	OR EXISTS (SELECT 1 FROM dbo.TSTANDARDMX b (NOLOCK) WHERE a.C35x03C=b.FZJC AND b.FCODE='GBRJSSXX')
	OR EXISTS (SELECT 1 FROM dbo.TSTANDARDMX b (NOLOCK) WHERE a.C35x04C=b.FZJC AND b.FCODE='GBRJSSXX')
	OR EXISTS (SELECT 1 FROM dbo.TSTANDARDMX b (NOLOCK) WHERE a.C35x05C=b.FZJC AND b.FCODE='GBRJSSXX')
	OR EXISTS (SELECT 1 FROM dbo.TSTANDARDMX b (NOLOCK) WHERE a.C35x06C=b.FZJC AND b.FCODE='GBRJSSXX')
	OR EXISTS (SELECT 1 FROM dbo.TSTANDARDMX b (NOLOCK) WHERE a.C35x07C=b.FZJC AND b.FCODE='GBRJSSXX')
	OR EXISTS (SELECT 1 FROM dbo.TSTANDARDMX b (NOLOCK) WHERE a.C35x08C=b.FZJC AND b.FCODE='GBRJSSXX')
	OR EXISTS (SELECT 1 FROM dbo.TSTANDARDMX b (NOLOCK) WHERE a.C35x09C=b.FZJC AND b.FCODE='GBRJSSXX')
	OR EXISTS (SELECT 1 FROM dbo.TSTANDARDMX b (NOLOCK) WHERE a.C35x10C=b.FZJC AND b.FCODE='GBRJSSXX') )

	--出院四级手术人数
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'CYSJSSRC','出院四级手术人数',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a                 
	WHERE  ( A14 > 1 OR ISNULL(A14, 0) = 0	AND A16 > 28 )
		AND ( EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C14x01C=b.FOPCODE AND b.FOPJB='四级')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x01C=b.FOPCODE AND b.FOPJB='四级')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x02C=b.FOPCODE AND b.FOPJB='四级')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x03C=b.FOPCODE AND b.FOPJB='四级')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x04C=b.FOPCODE AND b.FOPJB='四级')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x05C=b.FOPCODE AND b.FOPJB='四级')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x06C=b.FOPCODE AND b.FOPJB='四级')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x07C=b.FOPCODE AND b.FOPJB='四级')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x08C=b.FOPCODE AND b.FOPJB='四级')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x09C=b.FOPCODE AND b.FOPJB='四级')
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x10C=b.FOPCODE AND b.FOPJB='四级') )

	--出院择期手术总人数
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'CYZQSSRC','出院择期手术总人数(含妊娠)',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a 
	WHERE   B11C!=1
	AND ( A14 > 1 OR ISNULL(A14, 0) = 0	AND A16 > 28 )
		AND ( EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C14x01C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x01C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x02C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x03C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x04C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x05C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x06C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x07C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x08C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x09C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x10C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗')) )

	--择期手术患者并发症发生人次数
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'CYZQSSBFZRC','出院择期手术患者并发症发生人次数',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a 
	WHERE  B11C!=1
	AND ( A14 > 1 OR ISNULL(A14, 0) = 0	AND A16 > 28 )	
		AND ( EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C14x01C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x01C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x02C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x03C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x04C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x05C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x06C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x07C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x08C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x09C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x10C=b.FOPCODE AND b.FOPLB IN ('手术','介入治疗')) )
	AND (   EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C03C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C05C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x01C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x01C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x02C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x02C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x03C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x03C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x04C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x04C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x05C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x05C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x06C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x06C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x07C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x07C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x08C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x08C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x09C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x09C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x10C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x10C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x11C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x11C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x12C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x12C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x13C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x13C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x14C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x14C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x15C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x15C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x16C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x16C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x17C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x17C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x18C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x18C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x19C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x19C=4)
		OR  EXISTS (SELECT 1 FROM dbo.TSTANDARDMX c (NOLOCK) WHERE a.C06x20C=c.FZJC AND c.FCODE='GBRJSSXX' AND a.C08x20C=4) )

	--I类切口手术总人数
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'CYYLQKSSRC','出院I类切口手术总人数',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a 
	WHERE   ( A14 > 1 OR ISNULL(A14, 0) = 0	AND A16 > 28 )
	AND (   EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C14x01C=b.FOPCODE AND b.FOPLB = '手术' AND a.C21x01C IN (1,2,3,10))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x01C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x01C IN (1,2,3,10))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x02C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x02C IN (1,2,3,10))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x03C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x03C IN (1,2,3,10))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x04C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x04C IN (1,2,3,10))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x05C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x05C IN (1,2,3,10))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x06C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x06C IN (1,2,3,10))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x07C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x07C IN (1,2,3,10))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x08C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x08C IN (1,2,3,10))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x09C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x09C IN (1,2,3,10))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x10C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x10C IN (1,2,3,10)) )

	--I类切口手术总感染人数
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'CYYLQKSSZGRRC','出院I类切口手术总感染人数',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a 
	WHERE   ( A14 > 1 OR ISNULL(A14, 0) = 0	AND A16 > 28 )
	AND (   EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C14x01C=b.FOPCODE AND b.FOPLB = '手术' AND a.C21x01C = 3)
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x01C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x01C = 3)
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x02C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x02C = 3)
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x03C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x03C = 3)
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x04C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x04C = 3)
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x05C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x05C = 3)
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x06C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x06C = 3)
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x07C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x07C = 3)
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x08C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x08C = 3)
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x09C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x09C = 3)
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x10C=b.FOPCODE AND b.FOPLB = '手术' AND a.C42x10C = 3) ) 

	--急性心肌梗死
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'JXXJGS','急性心肌梗死',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a 
	WHERE   ( (LEFT(C03C,3)='I21') OR (LEFT(C06x01C,3)='I21') )

	--心力衰竭
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'XLSJ','心力衰竭',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a 
	WHERE  ( (LEFT(C03C,5)='I11.0' OR LEFT(C03C,5)='I13.0' OR LEFT(C03C,5)='I13.2' OR LEFT(C03C,3)='I50')
				OR (LEFT(C06x01C,5)='I11.0' OR LEFT(C06x01C,5)='I13.0' OR LEFT(C06x01C,5)='I13.2' OR LEFT(C06x01C,3)='I50') )

	--肺炎(成人)
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'FYCR','肺炎(成人)',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a 
	WHERE  ( (LEFT(C03C,3)='J13' OR LEFT(C03C,3)='J14' OR LEFT(C03C,3)='J15' OR LEFT(C03C,3)='J18')
						OR (LEFT(C06x01C,3)='J13' OR LEFT(C06x01C,3)='J14' OR LEFT(C06x01C,3)='J15' OR LEFT(C06x01C,3)='J18') )
					AND A14 >=18

	--肺炎(儿童)
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'FYET','肺炎(儿童)',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a 
	WHERE   ( (LEFT(C03C,3)='J13' OR LEFT(C03C,3)='J14' OR LEFT(C03C,3)='J15' OR LEFT(C03C,3)='J18')
				OR (LEFT(C06x01C,3)='J13' OR LEFT(C06x01C,3)='J14' OR LEFT(C06x01C,3)='J15' OR LEFT(C06x01C,3)='J18') )
			AND A14 >1 
			AND A14 < 18

	--脑梗死
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'NGS','脑梗死',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a 
	WHERE ( (LEFT(C03C,3)='I63'
				AND NOT (LEFT(C03C,7)='I63.301' OR LEFT(C03C,7)='I63.302' OR LEFT(C03C,7)='I63.401' OR LEFT(C03C,7)='I63.801' OR LEFT(C03C,7)='I63.802'))
			OR (LEFT(C06x01C,3)='I63'
				AND NOT ( LEFT(C06x01C,7)='I63.301' OR LEFT(C06x01C,7)='I63.302' OR LEFT(C06x01C,7)='I63.401' OR LEFT(C06x01C,7)='I63.801' OR LEFT(C06x01C,7)='I63.802')) )

	--髋关节置换术
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'KGJZHS','髋关节置换术',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a 
	WHERE   ( (LEFT(C14x01C,5)='81.51') OR (LEFT(C14x01C,5)='81.52') )

	--膝关节置换术
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'XGJZHS','膝关节置换术',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a 
	WHERE   LEFT(C14x01C,5)='81.54'

	--冠状动脉旁路移植术
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'GZDMPLYZS','冠状动脉旁路移植术',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a 
	WHERE   LEFT(C14x01C,4)='36.1'

	--剖宫产
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'PGC','剖宫产',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a 
	WHERE   ( LEFT(C14x01C,4)='74.0' OR LEFT(C14x01C,4)='74.1' OR LEFT(C14x01C,4)='74.2' )

	--慢性阻塞性肺疾病
	INSERT INTO THQMSDETAILSET  (FREPORTDATESTR,FCODE,FNAME,A48,A11,A14,B12,B15,B20,C03C,C04N,C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,B13C,B16C,B34C,D01)
	SELECT  FREPORTDATESTR,'MXZSXFJB','慢性阻塞性肺疾病',
		A48,A11,A14,B12,B15,B20,
			C03C,C04N,
			C14x01C,C15x01N,C16x01,C17x01,C18x01,C21x01C,C22x01C,
			B13C,B16C,B34C,
			CONVERT(VARCHAR(20),D01)
	FROM    #THQMSSET a 
	WHERE   ( (LEFT(C03C,5)='J44.0' OR LEFT(C03C,5)='J44.1' OR LEFT(C03C,5)='J44.9')
			OR (LEFT(C06x01C,5)='J44.0' OR LEFT(C06x01C,5)='J44.1' OR LEFT(C06x01C,5)='J44.9') )
END

IF NOT EXISTS (SELECT 1 FROM dbo.THQMSMASTERSET WHERE FREPORTDATESTR = @freportdatestr )
BEGIN
	--生成汇总
	--患者总人次数汇总
	INSERT INTO dbo.THQMSMASTERSET (FREPORTDATESTR,FCODE,FNAME,FCONTENT,FEXTEND,FPX)
	SELECT FREPORTDATESTR,FCODE,
			CASE WHEN FCODE IN ('CYHZZRC','CYHZZSSRC','CYHZZSSSWRC','CYWCSSRC','CYRJSSRC',
						'CYSJSSRC','CYZQSSRC','CYZQSSBFZRC','CYYLQKSSRC','CYYLQKSSZGRRC') THEN  FNAME
						 ELSE FNAME+'收治总例数' END,
			 CONVERT(VARCHAR(10), COUNT(1)),1,
		CASE WHEN FCODE='CYHZZRC' THEN 1
			  WHEN FCODE='CYHZZSSRC' THEN 2
			   WHEN FCODE='CYHZZSSSWRC' THEN 3
				WHEN FCODE='CYWCSSRC' THEN 4
				 WHEN FCODE='CYRJSSRC' THEN 5
				  WHEN FCODE='CYSJSSRC' THEN 6
				   WHEN FCODE='CYZQSSRC' THEN 7
				    WHEN FCODE='CYZQSSBFZRC' THEN 8
					 WHEN FCODE='CYYLQKSSRC' THEN 9
					  WHEN FCODE='CYYLQKSSZGRRC' THEN 10
					   WHEN FCODE='JXXJGS' THEN 11
					    WHEN FCODE='XLSJ' THEN 17
						 WHEN FCODE='FYCR' THEN 23
						  WHEN FCODE='FYET' THEN 29
						   WHEN FCODE='NGS' THEN 35
						    WHEN FCODE='KGJZHS' THEN 41
							 WHEN FCODE='XGJZHS' THEN 47
							  WHEN FCODE='GZDMPLYZS' THEN 53
							   WHEN FCODE='PGC' THEN 59
							    WHEN FCODE='MXZSXFJB' THEN 65
								ELSE 0 END
	FROM dbo.THQMSDETAILSET
	WHERE FREPORTDATESTR = @freportdatestr
	GROUP BY FREPORTDATESTR,FCODE,FNAME

	--病种分项汇总
	INSERT INTO dbo.THQMSMASTERSET (FREPORTDATESTR,FCODE,FNAME,FCONTENT,FEXTEND,FPX)
	SELECT FREPORTDATESTR,FCODE,FNAME+'出院患者占用总床日数',CONVERT(VARCHAR(10), SUM(CONVERT(INT,B20))),0,
			CASE  WHEN FCODE='JXXJGS' THEN 12
				   WHEN FCODE='XLSJ' THEN 18
					WHEN FCODE='FYCR' THEN 24
					 WHEN FCODE='FYET' THEN 30
					  WHEN FCODE='NGS' THEN 36
					   WHEN FCODE='KGJZHS' THEN 42
						WHEN FCODE='XGJZHS' THEN 48
						 WHEN FCODE='GZDMPLYZS' THEN 54
						  WHEN FCODE='PGC' THEN 60
						   WHEN FCODE='MXZSXFJB' THEN 66
							ELSE 0 END
	FROM dbo.THQMSDETAILSET
	WHERE FREPORTDATESTR = @freportdatestr
	AND FCODE IN ('JXXJGS','XLSJ','FYCR','FYET','NGS',
					 'KGJZHS','XGJZHS','GZDMPLYZS','PGC','MXZSXFJB')
	GROUP BY FREPORTDATESTR,FCODE,FNAME

	INSERT INTO dbo.THQMSMASTERSET (FREPORTDATESTR,FCODE,FNAME,FCONTENT,FEXTEND,FPX)
	SELECT FREPORTDATESTR,FCODE,FNAME+'平均住院日',CONVERT(VARCHAR(10), CONVERT(NUMERIC(9,2) , AVG(CONVERT(NUMERIC(10,2),B20)))),0,
			CASE WHEN FCODE='JXXJGS' THEN 13
				  WHEN FCODE='XLSJ' THEN 19
				   WHEN FCODE='FYCR' THEN 25
					WHEN FCODE='FYET' THEN 31
					 WHEN FCODE='NGS' THEN 37
					  WHEN FCODE='KGJZHS' THEN 43
					   WHEN FCODE='XGJZHS' THEN 49
						WHEN FCODE='GZDMPLYZS' THEN 55
						 WHEN FCODE='PGC' THEN 61
						  WHEN FCODE='MXZSXFJB' THEN 67
						   ELSE 0 END
	FROM dbo.THQMSDETAILSET
	WHERE FREPORTDATESTR = @freportdatestr
	AND FCODE IN ('JXXJGS','XLSJ','FYCR','FYET','NGS',
					 'KGJZHS','XGJZHS','GZDMPLYZS','PGC','MXZSXFJB')
	GROUP BY FREPORTDATESTR,FCODE,FNAME

	INSERT INTO dbo.THQMSMASTERSET (FREPORTDATESTR,FCODE,FNAME,FCONTENT,FEXTEND,FPX)
	SELECT FREPORTDATESTR,FCODE,FNAME+'总住院费用',CONVERT(VARCHAR(50), SUM((CONVERT(NUMERIC(10,2),D01)))),0,
			CASE WHEN FCODE='JXXJGS' THEN 14
				  WHEN FCODE='XLSJ' THEN 20
				   WHEN FCODE='FYCR' THEN 26
					WHEN FCODE='FYET' THEN 32
					 WHEN FCODE='NGS' THEN 38
					  WHEN FCODE='KGJZHS' THEN 44
					   WHEN FCODE='XGJZHS' THEN 50
						WHEN FCODE='GZDMPLYZS' THEN 56
						 WHEN FCODE='PGC' THEN 62
						  WHEN FCODE='MXZSXFJB' THEN 68
						   ELSE 0 END
	FROM dbo.THQMSDETAILSET
	WHERE FREPORTDATESTR = @freportdatestr
	AND FCODE IN ('JXXJGS','XLSJ','FYCR','FYET','NGS',
					 'KGJZHS','XGJZHS','GZDMPLYZS','PGC','MXZSXFJB')
	GROUP BY FREPORTDATESTR,FCODE,FNAME

	INSERT INTO dbo.THQMSMASTERSET (FREPORTDATESTR,FCODE,FNAME,FCONTENT,FEXTEND,FPX)
	SELECT FREPORTDATESTR,FCODE,FNAME+'次均费用',CONVERT(VARCHAR(50), CONVERT(NUMERIC(9,2) , AVG(CONVERT(NUMERIC(10,2),D01)))),0,
			CASE WHEN FCODE='JXXJGS' THEN 15
				  WHEN FCODE='XLSJ' THEN 21
				   WHEN FCODE='FYCR' THEN 27
					WHEN FCODE='FYET' THEN 33
					 WHEN FCODE='NGS' THEN 39
					  WHEN FCODE='KGJZHS' THEN 45
					   WHEN FCODE='XGJZHS' THEN 51
						WHEN FCODE='GZDMPLYZS' THEN 57
						 WHEN FCODE='PGC' THEN 63
						  WHEN FCODE='MXZSXFJB' THEN 69
						   ELSE 0 END
	FROM dbo.THQMSDETAILSET
	WHERE FREPORTDATESTR = @freportdatestr
	AND FCODE IN ('JXXJGS','XLSJ','FYCR','FYET','NGS',
					 'KGJZHS','XGJZHS','GZDMPLYZS','PGC','MXZSXFJB')
	GROUP BY FREPORTDATESTR,FCODE,FNAME

	INSERT INTO dbo.THQMSMASTERSET (FREPORTDATESTR,FCODE,FNAME,FCONTENT,FEXTEND,FPX)
	SELECT FREPORTDATESTR,FCODE,FNAME+'死亡人数',CONVERT(VARCHAR(10),SUM(CASE WHEN B34C=5 THEN 1 ELSE 0 END)),0,
			CASE WHEN FCODE='JXXJGS' THEN 16
				  WHEN FCODE='XLSJ' THEN 22
				   WHEN FCODE='FYCR' THEN 28
					WHEN FCODE='FYET' THEN 34
					 WHEN FCODE='NGS' THEN 40
					  WHEN FCODE='KGJZHS' THEN 46
					   WHEN FCODE='XGJZHS' THEN 52
						WHEN FCODE='GZDMPLYZS' THEN 58
						 WHEN FCODE='PGC' THEN 64
						  WHEN FCODE='MXZSXFJB' THEN 70
						   ELSE 0 END
	FROM dbo.THQMSDETAILSET
	WHERE FREPORTDATESTR = @freportdatestr
	AND FCODE IN ('JXXJGS','XLSJ','FYCR','FYET','NGS',
					 'KGJZHS','XGJZHS','GZDMPLYZS','PGC','MXZSXFJB')
	GROUP BY FREPORTDATESTR,FCODE,FNAME
END

--显示信息
IF @ishz=1
	SELECT FREPORTDATESTR,FCODE,FNAME,FCONTENT,FEXTEND FROM THQMSMASTERSET (NOLOCK)
	WHERE FREPORTDATESTR = @freportdatestr
	ORDER BY FPX
ELSE
	SELECT a.FREPORTDATESTR ,a.A48 ,a.A11 ,a.A14 ,
			a.B12 ,a.B15 , a.B20,
			 a.C03C ,a.C04N ,
			  a.C14x01C ,a.C15x01N ,a.C16x01 ,a.C17x01 ,
			   a.C18x01 ,a.C21x01C ,
				a.C22x01C ,a.B13C ,a.B16C ,a.B34C , a.D01				 
	FROM THQMSDETAILSET a (NOLOCK)
	WHERE FREPORTDATESTR = @freportdatestr AND FCODE=@category
END

GO
