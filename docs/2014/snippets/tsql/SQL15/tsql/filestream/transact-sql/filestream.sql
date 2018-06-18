--<snippetFS_Enable>
EXEC sp_filestream_configure 
    @enable_level = 3;
--</snippetFS_Enable>


--<snippetFS_CreateDB>
CREATE DATABASE Archive 
ON
PRIMARY ( NAME = Arch1,
    FILENAME = 'c:\data\archdat1.mdf'),
FILEGROUP FileStreamGroup1 CONTAINS FILESTREAM( NAME = Arch3,
    FILENAME = 'c:\data\filestream1')
LOG ON  ( NAME = Archlog1,
    FILENAME = 'c:\data\archlog1.ldf')
GO
--</snippetFS_CreateDB>


--<snippetFS_CreateTable>
CREATE TABLE Archive.dbo.Records
(
	[Id] [uniqueidentifier] ROWGUIDCOL NOT NULL UNIQUE, 
	[SerialNumber] INTEGER UNIQUE,
	[Chart] VARBINARY(MAX) FILESTREAM NULL
)
GO
--</snippetFS_CreateTable>


--<snippetFS_InsertNULL>
INSERT INTO Archive.dbo.Records
    VALUES (newid (), 1, NULL);
GO
--</snippetFS_InsertNULL>

--<snippetFS_InsertZero>
INSERT INTO Archive.dbo.Records
    VALUES (newid (), 2, 
      CAST ('' as varbinary(max)));
GO
--</snippetFS_InsertZero>

--<snippetFS_InsertData>
INSERT INTO Archive.dbo.Records
    VALUES (newid (), 3, 
      CAST ('Seismic Data' as varbinary(max)));
GO
--</snippetFS_InsertData>

--<snippetFS_UpdateData>
UPDATE Archive.dbo.Records
SET [Chart] = CAST('Xray 1' as varbinary(max))
WHERE [SerialNumber] = 2;
--</snippetFS_UpdateData>



--<snippetFS_DeleteData>
DELETE Archive.dbo.Records
WHERE SerialNumber = 1;
GO
--</snippetFS_DeleteData>

--<snippetFS_PathName>
DECLARE @filePath varchar(max)

SELECT @filePath = Chart.PathName()
FROM Archive.dbo.Records
WHERE SerialNumber = 3

PRINT @filepath
--</snippetFS_PathName>

--<snippetFS_GET_TRANSACTION_CONTEXT>
DECLARE @txContext varbinary(max)

BEGIN TRANSACTION
SELECT @txContext = GET_FILESTREAM_TRANSACTION_CONTEXT()
PRINT @txContext
COMMIT
--</snippetFS_GET_TRANSACTION_CONTEXT>

--<snippetPathNameExampleA>
DECLARE @PathName nvarchar(max)
SET @PathName = (
    SELECT TOP 1 photo.PathName()
    FROM dbo.Customer
    WHERE LastName = 'CustomerName'
    );
--</snippetPathNameExampleA>


--<snippetPathNameExampleB>
-- Create a FILESTREAM-enabled database.
-- The c:\data directory must exist.
CREATE DATABASE PathNameDB
ON
PRIMARY ( NAME = ArchX1,
    FILENAME = 'c:\data\archdatP1.mdf'),
FILEGROUP FileStreamGroup1 CONTAINS FILESTREAM( NAME = ArchX3,
    FILENAME = 'c:\data\filestreamP1')
LOG ON  ( NAME = ArchlogX1,
    FILENAME = 'c:\data\archlogP1.ldf');
GO

USE PathNameDB;
GO

-- Create a table, add some records, and
-- create the associated FILESTREAM
-- BLOB files.

CREATE TABLE TABLE1
    (
        ID int,
        RowGuidColumn UNIQUEIDENTIFIER
                      NOT NULL UNIQUE ROWGUIDCOL,
        FILESTREAMColumn varbinary(MAX) FILESTREAM
    );
GO

INSERT INTO TABLE1 VALUES(1, NEWID(), 0x00);
INSERT INTO TABLE1 VALUES(2, NEWID(), 0x00);
INSERT INTO TABLE1 VALUES(3, NEWID(), 0x00);
GO

SELECT FILESTREAMColumn.PathName() AS 'PathName' FROM TABLE1;

--Results
--PathName
------------------------------------------------------------------------------------------------------------
--\\SERVER\MSSQLSERVER\v1\PathNameExampleDB\dbo\TABLE1\FILESTREAMColumn\DD67C792-916E-4A76-8C8A-4A85DC5DB908
--\\SERVER\MSSQLSERVER\v1\PathNameExampleDB\dbo\TABLE1\FILESTREAMColumn\2907122B-2560-4CB9-86DC-FBE7ABA1843B
--\\SERVER\MSSQLSERVER\v1\PathNameExampleDB\dbo\TABLE1\FILESTREAMColumn\922BE0E0-CAB9-4403-90BF-945BD258E4BC
--
--(3 row(s) affected)
GO

--Drop the database to clean up.
USE MASTER
GO
DROP DATABASE PathNameDB
--</snippetPathNameExampleB>