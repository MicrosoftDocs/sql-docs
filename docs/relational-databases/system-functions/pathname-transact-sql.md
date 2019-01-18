---
title: "PathName (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/02/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "PathName_TSQL"
  - "PathName"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "PathName FILESTREAM [SQL Server]"
ms.assetid: 6b95ad90-6c82-4a23-9294-a2adb74934a3
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# PathName (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the path of a FILESTREAM binary large object (BLOB). The OpenSqlFilestream API uses this path to return a handle that an application can use to work with the BLOB data by using Win32 APIs. PathName is read-only.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
column_name.PathName ( @option [ , use_replica_computer_name ] )  
```  
  
## Arguments  
 *column_name*  
 Is the column name of a **varbinary(max)** FILESTREAM column. *column_name* must be a column name. It cannot be an expression nor the result of a CAST or CONVERT statement.  
  
 Requesting the PathName for a column of any other data type or for a **varbinary(max)** columnthat does not have the FILESTREAM storage attribute will cause a query compile-time error.  
  
 *@option*  
 An integer [expression](../../t-sql/language-elements/expressions-transact-sql.md) that defines how the server component of the path should be formatted. *@option* can be one of the following values. The default is 0.  
  
|Value|Description|  
|-----------|-----------------|  
|0|Returns the server name converted to BIOS format, for example: `\\SERVERNAME\MSSQLSERVER\v1\Archive\dbo\Records\Chart\A73F19F7-38EA-4AB0-BB89-E6C545DBD3F9`|  
|1|Returns the server name without conversion, for example: `\\ServerName\MSSQLSERVER\v1\Archive\dbo\Records\Chart\A73F1`|  
|2|Returns the complete server path, for example: `\\ServerName.MyDomain.com\MSSQLSERVER\v1\Archive\dbo\Records\Chart\A73F19F7-38EA-4AB0-BB89-E6C545DBD3F9`|  
  
 *use_replica_computer_name*  
 A bit value that defines how the server name should be returned in an Always On availability group.  
  
 When the database does not belong to an Always On availability group, then the value of this argument is ignored. The computer name is always used in the path.  
  
 When the database belongs to an Always On availability group, then the value of *use_replica_computer_name* has the following effect on the output of the **PathName** function:  
  
|Value|Description|  
|-----------|-----------------|  
|Not specified.|The function returns the virtual network name (VNN) in the path.|  
|0|The function returns the virtual network name (VNN) in the path.|  
|1|The function returns the computer name in the path.|  
  
## Return Type  
 **nvarchar(max)**  
  
## Return Value  
 The returned value is the fully qualified logical or NETBIOS path of the BLOB. PathName does not return an IP address. NULL is returned when the FILESTREAM BLOB has not been created.  
  
## Remarks  
 The ROWGUID column must be visible in any query that calls PathName.  
  
 A FILESTREAM BLOB can only be created by using [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
## Examples  
  
### A. Reading the path for a FILESTREAM BLOB  
 The following example assigns the `PathName` to an `nvarchar(max)` variable.  
  
```sql  
DECLARE @PathName nvarchar(max);  
SET @PathName = (  
    SELECT TOP 1 photo.PathName()  
    FROM dbo.Customer  
    WHERE LastName = 'CustomerName'  
    );  
```  
  
### B. Displaying the paths for FILESTREAM BLOBs in a table  
 The following example creates and displays the paths for three FILESTREAM BLOBs.  
  
```sql  
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
  
INSERT INTO TABLE1 VALUES  
 (1, NEWID(), 0x00)  
,(2, NEWID(), 0x00)  
,(3, NEWID(), 0x00);  
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
USE master;  
GO  
DROP DATABASE PathNameDB;  
```  
  
## See Also  
 [Binary Large Object &#40;Blob&#41; Data &#40;SQL Server&#41;](../../relational-databases/blob/binary-large-object-blob-data-sql-server.md)   
 [GET_FILESTREAM_TRANSACTION_CONTEXT &#40;Transact-SQL&#41;](../../t-sql/functions/get-filestream-transaction-context-transact-sql.md)   
 [Access FILESTREAM Data with OpenSqlFilestream](../../relational-databases/blob/access-filestream-data-with-opensqlfilestream.md)  
  
  
