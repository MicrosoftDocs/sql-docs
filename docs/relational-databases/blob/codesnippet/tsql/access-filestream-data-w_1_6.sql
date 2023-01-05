USE [master];
GO

-- Create database with FILESTREAM
CREATE DATABASE [FileStreamTest] CONTAINMENT = NONE ON PRIMARY (
    NAME = N'FileStreamTest'
    , FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\FileStreamTest.mdf'
    , SIZE = 204800 KB
    , MAXSIZE = UNLIMITED
    , FILEGROWTH = 65536 KB
    )
    , FILEGROUP [FileStreamFG] CONTAINS FILESTREAM DEFAULT(NAME = N'FileStreamTestFStream', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\FileStreamTestFStream', MAXSIZE = UNLIMITED) LOG ON (
    NAME = N'FileStreamTest_log'
    , FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\FileStreamTest_log.ldf'
    , SIZE = 270336 KB
    , MAXSIZE = 2048 GB
    , FILEGROWTH = 65536 KB
    )
    WITH CATALOG_COLLATION = DATABASE_DEFAULT;
GO

USE [FileStreamTest];
GO

CREATE TABLE FSTiffs (
    Guid UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL UNIQUE DEFAULT NEWSEQUENTIALID()
    , DocumentID INT NOT NULL
    , DocumentType VARCHAR(10) NOT NULL
    , FileContent VARBINARY(MAX) FILESTREAM NOT NULL
    , DateInserted DATETIME
);

-- Which database and files use FILESTREAM 
SELECT db_name(database_id) dbname
    , name AS file_name
    , physical_name
    , type_desc
    , *
FROM sys.master_files
WHERE type_desc = 'FILESTREAM';

-- Which tables in the database have FILESTREAM enabled
USE [FileStreamTest]
GO

SELECT *
FROM sys.tables
WHERE filestream_data_space_id IS NOT NULL;

--insert a TIFF file
INSERT INTO FSTiffs (
    DocumentID
    , DocumentType
    , FileContent
    , DateInserted
    )
SELECT 101
    , '.tiff'
    , *
    , GETDATE()
FROM OPENROWSET(BULK N'C:\Temp\Sample1.tiff', SINGLE_BLOB) rs;

-- Select data from FILESTREAM table
SELECT *
FROM FSTiffs;

-- Update a document
UPDATE FSTiffs
SET FileContent = (
        SELECT *
        FROM OPENROWSET(BULK N'C:\Temp\Sample2.tiff', SINGLE_BLOB) AS rs
        )
WHERE DocumentID = 101;

-- Delete a document
DELETE FSTiffs
WHERE DocumentID = 101;

--clean up any delete files
EXEC sp_filestream_force_garbage_collection @dbname = N'FileStreamTest'
    , @filename = N'FileStreamTestFStream';