IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID = OBJECT_ID('TSTANDARDMAIN') AND name = 'FHQMSMAP')
	ALTER TABLE TSTANDARDMAIN ADD FHQMSMAP BIT

UPDATE TSTANDARDMAIN SET FHQMSMAP = 1 WHERE FID = 1