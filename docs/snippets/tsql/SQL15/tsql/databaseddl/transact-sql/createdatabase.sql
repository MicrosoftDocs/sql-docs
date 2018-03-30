--<snippetCreateDatabase1>
USE master;
GO
CREATE DATABASE mytest;
GO
-- Verify the database files and sizes
SELECT name, size, size*1.0/128 AS [Size in MBs] 
FROM sys.master_files
WHERE name = N'mytest';
GO
--</snippetCreateDatabase1>
--<snippetCreateDatabase2>
USE master;
GO
IF DB_ID (N'Sales') IS NOT NULL
DROP DATABASE Sales;
GO
CREATE DATABASE Sales
ON 
( NAME = Sales_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\saledat.mdf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 5 )
LOG ON
( NAME = Sales_log,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\salelog.ldf',
    SIZE = 5MB,
    MAXSIZE = 25MB,
    FILEGROWTH = 5MB );
GO
--</snippetCreateDatabase2>
--<snippetCreateDatabase3>
USE master;
GO
IF DB_ID (N'Archive') IS NOT NULL
DROP DATABASE Archive;
GO

CREATE DATABASE Archive 
ON
PRIMARY  
    (NAME = Arch1,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\archdat1.mdf',
    SIZE = 100MB,
    MAXSIZE = 200,
    FILEGROWTH = 20),
    ( NAME = Arch2,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\archdat2.ndf',
    SIZE = 100MB,
    MAXSIZE = 200,
    FILEGROWTH = 20),
    ( NAME = Arch3,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\archdat3.ndf',
    SIZE = 100MB,
    MAXSIZE = 200,
    FILEGROWTH = 20)
LOG ON 
   (NAME = Archlog1,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\archlog1.ldf',
    SIZE = 100MB,
    MAXSIZE = 200,
    FILEGROWTH = 20),
   (NAME = Archlog2,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\archlog2.ldf',
    SIZE = 100MB,
    MAXSIZE = 200,
    FILEGROWTH = 20);
GO
--</snippetCreateDatabase3>
--<snippetCreateDatabase4>
USE master;
GO
IF DB_ID (N'Sales') IS NOT NULL
DROP DATABASE Sales;
GO
CREATE DATABASE  Sales
ON PRIMARY
( NAME = SPri1_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SPri1dat.mdf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 15% ),
( NAME = SPri2_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SPri2dt.ndf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 15% ),
FILEGROUP SalesGroup1
( NAME = SGrp1Fi1_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SG1Fi1dt.ndf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 5 ),
( NAME = SGrp1Fi2_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SG1Fi2dt.ndf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 5 ),
FILEGROUP SalesGroup2
( NAME = SGrp2Fi1_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SG2Fi1dt.ndf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 5 ),
( NAME = SGrp2Fi2_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SG2Fi2dt.ndf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 5 )
LOG ON
( NAME = Sales_log,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\salelog.ldf',
    SIZE = 5MB,
    MAXSIZE = 25MB,
    FILEGROWTH = 5MB );

GO
--</snippetCreateDatabase4>

--<snippetCreateDatabase5>
USE master;
GO
sp_detach_db Archive;
GO
-- CREATE DATABASE FOR ATTACH statement
CREATE DATABASE Archive
      ON (FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\archdat1.mdf')
      FOR ATTACH;
GO
--</snippetCreateDatabase5>
--<snippetCreateDatabase6>
USE master;
GO
CREATE DATABASE sales_snapshot0600 ON
    ( NAME = SPri1_dat, FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SPri1dat_0600.ss'),
    ( NAME = SPri2_dat, FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SPri2dt_0600.ss'),
    ( NAME = SGrp1Fi1_dat, FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SG1Fi1dt_0600.ss'),
    ( NAME = SGrp1Fi2_dat, FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SG1Fi2dt_0600.ss'),
    ( NAME = SGrp2Fi1_dat, FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SG2Fi1dt_0600.ss'),
    ( NAME = SGrp2Fi2_dat, FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SG2Fi2dt_0600.ss')
AS SNAPSHOT OF Sales;
GO
--</snippetCreateDatabase6>
--<snippetCreateDatabase7>
USE master;
GO
IF DB_ID (N'MyOptionsTest') IS NOT NULL
DROP DATABASE MyOptionsTest;
GO
CREATE DATABASE MyOptionsTest
COLLATE French_CI_AI
WITH TRUSTWORTHY ON, DB_CHAINING ON;
GO
--Verifying collation and option settings.
SELECT name, collation_name, is_trustworthy_on, is_db_chaining_on
FROM sys.databases
WHERE name = N'MyOptionsTest';
GO
--</snippetCreateDatabase7>
