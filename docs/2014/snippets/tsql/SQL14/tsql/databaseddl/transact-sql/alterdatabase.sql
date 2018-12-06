--<snippetAlterDatabase1>
USE master;
GO
ALTER DATABASE AdventureWorks2012 
ADD FILE 
(
    NAME = Test1dat2,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\t1dat2.ndf',
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
);
GO
--</snippetAlterDatabase1>
--<snippetAlterDatabase2>
USE master
GO
ALTER DATABASE AdventureWorks2012
ADD FILEGROUP Test1FG1;
GO
ALTER DATABASE AdventureWorks2012 
ADD FILE 
(
    NAME = test1dat3,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\t1dat3.ndf',
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
),
(
    NAME = test1dat4,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\t1dat4.ndf',
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
)
TO FILEGROUP Test1FG1;
GO
--</snippetAlterDatabase2>

--<snippetAlterDatabase3>
USE master;
GO
ALTER DATABASE AdventureWorks2012 
ADD LOG FILE 
(
    NAME = test1log2,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\test2log.ldf',
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
),
(
    NAME = test1log3,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\test3log.ldf',
    SIZE = 5MB,
    MAXSIZE = 100MB,
    FILEGROWTH = 5MB
);
GO
--</snippetAlterDatabase3>
--<snippetAlterDatabase4>
USE master;
GO
ALTER DATABASE AdventureWorks2012
REMOVE FILE test1dat4;
GO
--</snippetAlterDatabase4>
--<snippetAlterDatabase5>
USE master;
GO
ALTER DATABASE AdventureWorks2012 
MODIFY FILE
    (NAME = test1dat3,
    SIZE = 20MB);
GO
--</snippetAlterDatabase5>
--<snippetAlterDatabase6>
USE master;
GO
ALTER DATABASE AdventureWorks2012 
MODIFY FILEGROUP Test1FG1 DEFAULT;
GO
ALTER DATABASE AdventureWorks2012 
MODIFY FILEGROUP [PRIMARY] DEFAULT;
GO
--</snippetAlterDatabase6>
--<snippetAlterDatabase7>
USE master;
GO
ALTER DATABASE AdventureWorks2012 
SET RECOVERY FULL, PAGE_VERIFY CHECKSUM;
GO
--</snippetAlterDatabase7>
--<snippetAlterDatabase8>
USE master;
GO
ALTER DATABASE AdventureWorks2012
SET SINGLE_USER
WITH ROLLBACK IMMEDIATE;
GO
ALTER DATABASE AdventureWorks2012
SET READ_ONLY;
GO
ALTER DATABASE AdventureWorks2012
SET MULTI_USER;
GO
--</snippetAlterDatabase8>


--<snippetAlterDatabase9>
USE AdventureWorks2012;
GO
-- Check the state of the snapshot_isolation_framework
-- in the database.
SELECT name, snapshot_isolation_state,
     snapshot_isolation_state_desc AS description
FROM sys.databases
WHERE name = N'AdventureWorks2012';
GO
USE master;
GO
ALTER DATABASE AdventureWorks2012
    SET ALLOW_SNAPSHOT_ISOLATION ON;
GO
-- Check again.
SELECT name, snapshot_isolation_state,
     snapshot_isolation_state_desc AS description
FROM sys.databases
WHERE name = N'AdventureWorks2012';
GO
--</snippetAlterDatabase9>
