CREATE DATABASE Archive 
ON
PRIMARY ( NAME = Arch1,
    FILENAME = 'C:\data\archdat1.mdf'),
FILEGROUP FileStreamGroup1 CONTAINS FILESTREAM ( NAME = Arch3,
    FILENAME = 'C:\data\filestream1')
LOG ON  ( NAME = Archlog1,
    FILENAME = 'C:\data\archlog1.ldf')
GO