---
title: "MSSQLSERVER_2596"
description: "MSSQLSERVER_2596"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "2596 (Database Engine error)"
---
# MSSQLSERVER_2596
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|2596|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_DATABASE_IN_READ_ONLY_MODE|  
|Message Text|The repair statement was not processed. The database cannot be in read-only mode.|  
  
## Explanation  
This message indicates that the database is in read-only mode. Repairs are not possible when a database is in read-only mode.  
  
## User Action  
Set the database to read-write by using ALTER DATABASE, and then re-run the DBCC command.  
  
## See Also  
[ALTER DATABASE &#40;Transact-SQL&#41;](~/t-sql/statements/alter-database-transact-sql-set-options.md)  
  
