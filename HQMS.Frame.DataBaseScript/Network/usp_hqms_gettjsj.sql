IF OBJECT_ID('usp_hqms_gettjsj','P') IS NOT NULL
DROP PROC usp_hqms_gettjsj
GO

CREATE PROC usp_hqms_gettjsj
@ksrq        VARCHAR(20),
@jsrq        VARCHAR(20),
@category    varchar(50)=''
AS

BEGIN

SET NOCOUNT ON

DECLARE @freportdatestr VARCHAR(100)
SELECT @freportdatestr=dbo.fn_Convertdatestr(@ksrq,@jsrq)

IF NOT EXISTS (SELECT 1 FROM dbo.THQMSSET (NOLOCK) WHERE FREPORTDATESTR = @freportdatestr)
BEGIN
	SELECT 'F','���¼�Ч����δ���ɣ������ɺ�����!'
	RETURN
END

SELECT  * INTO    #THQMSSET FROM    dbo.THQMSSET a (NOLOCK)  WHERE   FREPORTDATESTR = @freportdatestr

CREATE TABLE #hqms_masterinfo
(
	code       VARCHAR(10),
	name       VARCHAR(50),
	value      VARCHAR(100),
	category   varchar(50),
	isextend   BIT
)

CREATE TABLE #hqms_detailinfo
(
	FREPORTDATESTR	VARCHAR(10),
	A48             VARCHAR(50),
	A11             VARCHAR(50)
)
--���ɻ�����Ϣ
IF @category=''
BEGIN

	--��Ժ�������˴���
	INSERT INTO #hqms_masterinfo(code,name,value,category,isextend)
	SELECT  '01','��Ժ�������˴���',CONVERT(VARCHAR(10), COUNT(1)),'CYHZZRC',1  FROM #THQMSSET

	--��Ժ��������������
	INSERT INTO #hqms_masterinfo(code,name,value,category,isextend)
	SELECT  '02','��Ժ��������������',CONVERT(VARCHAR(10), COUNT(1)),'CYHZZSSRC',1
	FROM    #THQMSSET a                 
	WHERE  ( A14 > 1 OR ISNULL(A14, 0) = 0	AND A16 > 28 )
		AND ( EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C14x01C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x01C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x02C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x03C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x04C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x05C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x06C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x07C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x08C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x09C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x10C=b.FOPCODE AND b.FOPLB IN ('����','��������')) )
END

--������ϸ��Ϣ
IF @category='CYHZZRC'
BEGIN
	INSERT INTO #hqms_detailinfo (FREPORTDATESTR,A48,A11)
	SELECT FREPORTDATESTR,A48,A11 FROM #THQMSSET
END
ELSE IF @category='CYHZZSSRC'
BEGIN
	INSERT INTO #hqms_detailinfo (FREPORTDATESTR,A48,A11)
	SELECT FREPORTDATESTR,A48,A11 	
	FROM    #THQMSSET a                 
	WHERE  ( A14 > 1 OR ISNULL(A14, 0) = 0	AND A16 > 28 )
		AND ( EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C14x01C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x01C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x02C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x03C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x04C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x05C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x06C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x07C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x08C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x09C=b.FOPCODE AND b.FOPLB IN ('����','��������'))
		OR  EXISTS (SELECT 1 FROM dbo.TOPERATE b (NOLOCK) WHERE a.C35x10C=b.FOPCODE AND b.FOPLB IN ('����','��������')) )
END

--��ʾ��Ϣ
IF @category=''
	SELECT code,name,value,category,isextend FROM #hqms_masterinfo
ELSE
	SELECT FREPORTDATESTR,A48,A11 FROM #hqms_detailinfo

END

GO
