---
title: "Move Database Files | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "disaster recovery [SQL Server], moving database files"
  - "files [SQL Server], moving"
  - "data files [SQL Server], moving"
  - "file moving [SQL Server]"
  - "moving full-text catalogs"
  - "moving databases"
  - "full-text catalogs [SQL Server], moving"
  - "moving database files"
  - "scheduled disk maintenance [SQL Server]"
  - "moving files"
  - "relocating database files"
  - "planned database relocations [SQL Server]"
  - "databases [SQL Server], moving"
ms.assetid: 89f01b10-5fae-4ed8-b0fb-a4b9f540fd28
author: stevestein
ms.author: sstein
manager: craigg
---
# Move Database Files
  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can move system and user databases by specifying the new file location in the FILENAME clause of the [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql) statement. Data, log, and full-text catalog files can be moved in this way. This may be useful in the following situations:  
  
-   Failure recovery. For example, the database is in suspect mode or has shut down, because of a hardware failure.  
  
-   Planned relocation.  
  
-   Relocation for scheduled disk maintenance.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Move User Databases](move-user-databases.md)|Describes the procedures for moving user database files and full-text catalog files to a new location.|  
|[Move System Databases](system-databases.md)|Describes the procedures for moving system database files to a new location.|  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql)   
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](/sql/t-sql/statements/create-database-sql-server-transact-sql)   
 [Database Detach and Attach &#40;SQL Server&#41;](database-detach-and-attach-sql-server.md)  
  
  
