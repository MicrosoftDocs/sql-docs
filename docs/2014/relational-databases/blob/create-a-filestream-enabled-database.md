---
title: "Create a FILESTREAM-Enabled Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FILESTREAM [SQL Server], FILESTREAM-enabled databases"
ms.assetid: 0fc16356-76f7-44b8-a58b-f0b7c43694ec
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Create a FILESTREAM-Enabled Database
  This topic shows how to create a database that supports FILESTREAM. Because FILESTREAM uses a special type of filegroup, when you create the database, you must specify the CONTAINS FILESTREAM clause for at least one filegroup.  
  
 A FILESTREAM filegroup can contain more than one file. For a code example that demonstrates how to create a FILESTREAM filegroup that contains multiple files, see [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](/sql/t-sql/statements/create-database-sql-server-transact-sql).  
  
### To create a FILESTREAM-enabled database  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], click **New Query** to display the Query Editor.  
  
2.  Copy the [!INCLUDE[tsql](../../includes/tsql-md.md)] code creates a FILESTREAM-enabled database called Archive.  
  
    > [!NOTE]  
    >  For this script, the directory C:\Data must exist.  
  
3.  To build the database, click **Execute**.  
  
## Example  
 The following code example creates a database that is named `Archive`. The database contains three filegroups: `PRIMARY`, `Arch1`, and `FileStreamGroup1`. `PRIMARY` and `Arch1` are regular filegroups that cannot contain FILESTREAM data. `FileStreamGroup1` is the `FILESTREAM` filegroup.  
  
```tsql  
CREATE DATABASE Archive   
ON  
PRIMARY ( NAME = Arch1,  
    FILENAME = 'c:\data\archdat1.mdf'),  
FILEGROUP FileStreamGroup1 CONTAINS FILESTREAM( NAME = Arch3,  
    FILENAME = 'c:\data\filestream1')  
LOG ON  ( NAME = Archlog1,  
    FILENAME = 'c:\data\archlog1.ldf')  
GO  
```  
  
 For a `FILESTREAM` filegroup, `FILENAME` refers to a path. The path up to the last folder must exist, and the last folder must not exist. In this example, `c:\data` must exist. However, the `filestream1` subfolder cannot exist when you execute the `CREATE DATABASE` statement. For more information about the syntax, see [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](/sql/t-sql/statements/create-database-sql-server-transact-sql).  
  
 After you run the previous example, a filestream.hdr file and an $FSLOG folder appears in the c:\Data\filestream1 folder. The filestream.hdr file is a header file for the FILESTREAM container.  
  
> [!IMPORTANT]  
>  The filestream.hdr file is an important system file. It contains FILESTREAM header information. Do not remove or modify this file.  
  
 For existing databases, you can use the [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql) statement to add a FILESTREAM filegroup.  
  
## See Also  
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](/sql/t-sql/statements/create-database-sql-server-transact-sql)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql)  
  
  
