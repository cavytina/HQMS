IF OBJECT_ID('THQMSDETAILSET','U') IS NOT NULL
DROP TABLE THQMSDETAILSET
GO

CREATE TABLE THQMSDETAILSET
(
	FID               BIGINT     IDENTITY(1,1),
	FREPORTDATESTR	  VARCHAR(255),
	FCODE             VARCHAR(50),
	FNAME             VARCHAR(255),
	A48               VARCHAR(50),       --סԺ��
	A11               VARCHAR(50),       --����
	A14               VARCHAR(50),       --���䣨�꣩
	B12               VARCHAR(50),       --��Ժʱ��
	B15               VARCHAR(50),       --��Ժʱ��
	B20               VARCHAR(50),       --ʵ��סԺ���죩
	C03C              VARCHAR(50),       --��Ժ��Ҫ��ϱ���
	C04N              VARCHAR(250),      --��Ժ��Ҫ�������
	C14x01C           VARCHAR(50),       --��Ҫ������������
	C15x01N           VARCHAR(250),      --��Ҫ������������
	C16x01            VARCHAR(50),       --��Ҫ������������
	C17x01            VARCHAR(50),       --��Ҫ������������
	C18x01            VARCHAR(50),       --��Ҫ������������
	C21x01C           VARCHAR(50),       --��Ҫ���������п����ϵȼ�
	C22x01C           VARCHAR(50),       --��Ҫ������������ʽ
	B13C              VARCHAR(50),       --��Ժ�Ʊ�
	B16C              VARCHAR(50),       --��Ժ�Ʊ�
	B34C              VARCHAR(10),       --��Ժ��ʽ 
	D01               VARCHAR(20)        --סԺ�ܷ���
)

GO
