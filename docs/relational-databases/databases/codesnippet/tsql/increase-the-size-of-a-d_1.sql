USE master;
GO
ALTER DATABASE AdventureWorks2022 
MODIFY FILE
    (NAME = test1dat3,
    SIZE = 20MB);
GO