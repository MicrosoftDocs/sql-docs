---
title: "sp_attach_db (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/01/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_attach_db_TSQL"
  - "sp_attach_db"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_attach_db"
ms.assetid: 59bc993e-7913-4091-89cb-d2871cffda95
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_attach_db (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Attaches a database to a server.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you use CREATE DATABASE *database_name* FOR ATTACH instead. For more information, see [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md).  
  
> [!NOTE]  
>  To rebuild multiple log files when one or more have a new location, use CREATE DATABASE *database_name* FOR ATTACH_REBUILD_LOG.  
  
> [!IMPORTANT]  
>  We recommend that you do not attach or restore databases from unknown or untrusted sources. Such databases could contain malicious code that might execute unintended [!INCLUDE[tsql](../../includes/tsql-md.md)] code or cause errors by modifying the schema or the physical database structure. Before you use a database from an unknown or untrusted source, run [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) on the database on a nonproduction server and also examine the code, such as stored procedures or other user-defined code, in the database.  
  
## Syntax  
  
```  
  
sp_attach_db [ @dbname= ] 'dbname'  
    , [ @filename1= ] 'filename_n' [ ,...16 ]   
```  
  
## Arguments  
`[ @dbname = ] 'dbnam_ '`
 Is the name of the database to be attached to the server. The name must be unique. *dbname* is **sysname**, with a default of NULL.  
  
`[ @filename1 = ] 'filename_n'`
 Is the physical name, including path, of a database file. *filename_n* is **nvarchar(260)**, with a default of NULL. Up to 16 file names can be specified. The parameter names start at **@filename1** and increment to **@filename16**. The file name list must include at least the primary file. The primary file contains the system tables that point to other files in the database. The list must also include any files that were moved after the database was detached.  
  
> [!NOTE]  
>  This argument maps to the FILENAME parameter of the CREATE DATABASE statement. For more information, see [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md).  
>   
>  When you attach a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database that contains full-text catalog files onto a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] server instance, the catalog files are attached from their previous location along with the other database files, the same as in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. For more information, see [Upgrade Full-Text Search](../../relational-databases/search/upgrade-full-text-search.md).  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 The **sp_attach_db** stored procedure should only be executed on databases that were previously detached from the database server by using an explicit **sp_detach_db** operation or on copied databases. If you have to specify more than 16 files, use CREATE DATABASE *database_name* FOR ATTACH or CREATE DATABASE *database_name* FOR_ATTACH_REBUILD_LOG. For more information, see [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md).  
  
 Any unspecified file is assumed to be in its last known location. To use a file in a different location, you must specify the new location.  
  
 A database created by a more recent version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot be attached in earlier versions.  
  
> [!NOTE]  
>  A database snapshot cannot be detached or attached.  
  
 When you attach a replicated database that was copied instead of being detached, consider the following:  
  
-   If you attach the database to the same server instance and version as the original database, no additional steps are required.  
  
-   If you attach the database to the same server instance but with an upgraded version, you must execute [sp_vupgrade_replication](../../relational-databases/system-stored-procedures/sp-vupgrade-replication-transact-sql.md) to upgrade replication after the attach operation is complete.  
  
-   If you attach the database to a different server instance, regardless of version, you must execute [sp_removedbreplication](../../relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql.md) to remove replication after the attach operation is complete.  
  
 When a database is first attached or restored to a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a copy of the database master key (encrypted by the service master key) is not yet stored in the server. You must use the **OPEN MASTER KEY** statement to decrypt the database master key (DMK). Once the DMK has been decrypted, you have the option of enabling automatic decryption in the future by using the **ALTER MASTER KEY REGENERATE** statement to provision the server with a copy of the DMK, encrypted with the service master key (SMK). When a database has been upgraded from an earlier version, the DMK should be regenerated to use the newer AES algorithm. For more information about regenerating the DMK, see [ALTER MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-master-key-transact-sql.md). The time required to regenerate the DMK key to upgrade to AES depends upon the number of objects protected by the DMK. Regenerating the DMK key to upgrade to AES is only necessary once, and has no impact on future regenerations as part of a key rotation strategy.  
  
## Permissions  
 For information about how permissions are handled when a database is attached, see [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md).  
  
## Examples  
 The following example attaches files from [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] to the current server.  
  
```  
EXEC sp_attach_db @dbname = N'AdventureWorks2012',   
    @filename1 =   
N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\AdventureWorks2012_Data.mdf',   
    @filename2 =   
N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\AdventureWorks2012_log.ldf';  
```  
  
## See Also  
 [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md)   
 [sp_detach_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-detach-db-transact-sql.md)   
 [sp_helpfile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpfile-transact-sql.md)   
 [sp_removedbreplication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
