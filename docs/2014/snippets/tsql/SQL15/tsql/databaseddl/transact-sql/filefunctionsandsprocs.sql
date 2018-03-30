-- File function examples

--<snippetFileFunction1>
USE AdventureWorks2012;
GO
SELECT FILEGROUPPROPERTY('PRIMARY', 'IsDefault') AS 'Default Filegroup';
GO
--</snippetFileFunction1>
--<snippetFileFunction2>
USE AdventureWorks2012;
GO
SELECT FILEGROUP_ID('PRIMARY') AS [Filegroup ID];
GO
--</snippetFileFunction2>
--<snippetFileFunction3>
USE AdventureWorks2012;
GO
SELECT FILEGROUP_NAME(1) AS [Filegroup Name];
GO
--</snippetFileFunction3>
--<snippetFileFunction4>
USE AdventureWorks2012;
GO
SELECT FILE_NAME(1) AS 'File Name 1', FILE_NAME(2) AS 'File Name 2';
GO
--</snippetFileFunction4>
--<snippetFileFunction5>
USE AdventureWorks2012;
GO
SELECT FILEPROPERTY('AdventureWorks2012_Data', 'IsPrimaryFile')AS [Primary File];
GO
--</snippetFileFunction5>
--<snippetFileFunction6>
USE AdventureWorks2012;
GO
SELECT FILE_ID('AdventureWorks2012_Data')AS 'File ID';
GO
--</snippetFileFunction6>
--<snippetFileFunction7>
USE AdventureWorks2012;
GO
SELECT FILE_IDEX('AdventureWorks2012_Data')AS 'File ID';
GO
--</snippetFileFunction7>
--<snippetFileFunction8>
USE AdventureWorks2012;
GO
SELECT FILE_IDEX((SELECT TOP(1)name FROM sys.database_files 
WHERE type = 1))AS 'File ID';
GO
--</snippetFileFunction8>
--<snippetFileFunction9>
SELECT FILE_IDEX((SELECT name FROM sys.master_files WHERE type = 4))
AS 'File_ID';
--</snippetFileFunction9>
--<snippetFileFunction10>
SELECT *
FROM fn_virtualfilestats(1, 1);
GO
--</snippetFileFunction10>
--<snippetFileFunction11>
SELECT *
FROM fn_virtualfilestats(DB_ID(N'AdventureWorks2012'), 2);
GO
--</snippetFileFunction11>
--<snippetFileFunction12>
SELECT *
FROM fn_virtualfilestats(NULL,NULL);
GO
--</snippetFileFunction12>
--<snippetFileFunction13>
SELECT * FROM sys.dm_io_virtual_file_stats(DB_ID(N'AdventureWorks2012'), 2);
GO
--</snippetFileFunction13>
--<snippetFileStoredProc1>
USE AdventureWorks2012;
GO
EXEC sp_helpfile;
GO
--</snippetFileStoredProc1>
--<snippetFileStoredProc2>
USE AdventureWorks2012;
GO
EXEC sp_helpfilegroup;
GO
--</snippetFileStoredProc2>
--<snippetFileStoredProc3>
USE AdventureWorks2012;
GO
EXEC sp_helpfilegroup 'PRIMARY';
GO
--</snippetFileStoredProc3>
