---
title: "sp_db_vardecimal_storage_format (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_db_vardecimal_storage_format"
  - "sp_db_vardecimal_storage_format_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_db_vardecimal_storage_format"
  - "decimal data type, compressing"
  - "compressing decimal data"
  - "numeric data type, compressing"
  - "database compression [SQL Server]"
  - "table compression [SQL Server]"
ms.assetid: 9920b2f7-b802-4003-913c-978c17ae4542
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_db_vardecimal_storage_format (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the current vardecimal storage format state of a database or enables a database for vardecimal storage format.  Starting with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], user databases are always enabled. Enabling databases for the vardecimal storage format is only necessary in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
  
> [!IMPORTANT]  
>  Changing the vardecimal storage format state of a database can affect backup and recovery, database mirroring, sp_attach_db, log shipping, and replication.  
  
## Syntax  
  
```  
  
sp_db_vardecimal_storage_format [ [ @dbname = ] 'database_name']   
    [ , [ @vardecimal_storage_format = ] { 'ON' | 'OFF' } ]   
[;]  
```  
  
## Arguments  
 [ @dbname= ] '*database_name*'  
 Is the name of the database for which the storage format is to be changed. *database_name* is **sysname**, with no default. If the database name is omitted, the vardecimal storage format status of all the databases in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are returned.  
  
 [ @vardecimal_storage_format= ] {'ON'|'OFF'}  
 Specifies whether the vardecimal storage format is enabled. @vardecimal_storage_format can be ON or OFF. The parameter is **varchar(3)**, with no default. If a database name is provided but @vardecimal_storage_format is omitted, the current setting of the specified database is returned. This argument has no effect on [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or later versions.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 If the database storage format cannot be changed, sp_db_vardecimal_storage_format returns an error. If the database is already in the specified state, the stored procedure has no effect.  
  
 If the @vardecimal_storage_format argument is not provided, returns the columns Database Name and the Vardecimal State.  
  
## Remarks  
 sp_db_vardecimal_storage_format returns the vardecimal state but cannot change the vardecimal state.  
  
 sp_db_vardecimal_storage_format will fail in the following circumstances:  
  
-   There are active users in the database.  
  
-   The database is enabled for mirroring.  
  
-   The edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not support vardecimal storage format.  
  
 To change the vardecimal storage format state to OFF, a database must be set to simple recovery mode. When a database is set to simple recovery mode, the log chain is broken. Perform a full database backup after you set the vardecimal storage format state to OFF.  
  
 Changing the state to OFF will fail if there are tables using vardecimal database compression. To change the storage format of a table, use [sp_tableoption](../../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md). To determine which tables in a database are using vardecimal storage format, use the `OBJECTPROPERTY` function and search for the `TableHasVarDecimalStorageFormat` property, as shown in the following example.  
  
```  
USE AdventureWorks2012 ;  
GO  
SELECT name, object_id, type_desc  
FROM sys.objects   
 WHERE OBJECTPROPERTY(object_id,   
   N'TableHasVarDecimalStorageFormat') = 1 ;  
GO  
```  
  
## Examples  
 The following code enables compression in the `AdventureWorks2012` database, confirms the state, and then compresses decimal and numeric columns in the `Sales.SalesOrderDetail` table.  
  
```  
USE master ;  
GO  
  
EXEC sp_db_vardecimal_storage_format 'AdventureWorks2012', 'ON' ;  
GO  
  
-- Check the vardecimal storage format state for  
-- all databases in the instance.  
EXEC sp_db_vardecimal_storage_format ;  
GO  
  
USE AdventureWorks2012 ;  
GO  
  
EXEC sp_tableoption 'Sales.SalesOrderDetail', 'vardecimal storage format', 1 ;  
GO  
```  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)  
  
  
