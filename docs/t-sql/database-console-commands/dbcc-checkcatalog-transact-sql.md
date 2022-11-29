---
title: "DBCC CHECKCATALOG (Transact-SQL)"
description: "DBCC CHECKCATALOG (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "11/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "DBCC_CHECKCATALOG_TSQL"
  - "DBCC CHECKCATALOG"
  - "CHECKCATALOG_TSQL"
  - "CHECKCATALOG"
helpviewer_keywords:
  - "catalogs [SQL Server], consistency checks"
  - "checking catalog consistency"
  - "DBCC CHECKCATALOG statement"
  - "integrity [SQL Server], catalogs"
  - "consistency [SQL Server], catalogs"
dev_langs:
  - "TSQL"
---
# DBCC CHECKCATALOG (Transact-SQL)
[!INCLUDE [SQL Server SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Checks for catalog consistency within the specified database. The database must be online.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
DBCC CHECKCATALOG   
[   
    (   
    database_name | database_id | 0  
    )  
]  
    [ WITH NO_INFOMSGS ]   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *database_name* | *database_id* | 0  
 Is the name or ID of the database for which to check catalog consistency. If not specified, or if 0 is specified, the current database is used. Database names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 WITH NO_INFOMSGS  
 Suppresses all informational messages.  
  
## Remarks  
After the DBCC CHECKCATALOG command finishes, a message is written to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log. If the DBCC command successfully executes, the message indicates a successful completion and the amount of time that the command ran. If the DBCC command stops before completing the check because of an error, the message indicates the command was terminated, a state value, and the amount of time the command ran. The following table lists and describes the state values that can be included in the message.
  
|State|Description|  
|-----------|-----------------|  
|0|Error number 8930 was raised. This indicates a metadata corruption that caused the DBCC command to terminate.|  
|1|Error number 8967 was raised. There was an internal DBCC error.|  
|2|A failure occurred during emergency mode database repair.|  
|3|This indicates a metadata corruption that caused the DBCC command to terminate.|  
|4|An assert or access violation was detected.|  
|5|An unknown error occurred that terminated the DBCC command.|  
  
DBCC CHECKCATALOG performs various consistency checks between system metadata tables. DBCC CHECKCATALOG uses an internal database snapshot to provide the transactional consistency that it needs to perform these checks. For more information, see [View the Size of the Sparse File of a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/view-the-size-of-the-sparse-file-of-a-database-snapshot-transact-sql.md) and the "DBCC Internal Database Snapshot Usage" section in [DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md).
If a snapshot cannot be created DBCC CHECKCATALOG acquires an exclusive database lock to obtain the required consistency. If any inconsistencies are detected, they cannot be repaired and the database must be restored from a backup.
  
> [!NOTE]  
> Running DBCC CHECKCATALOG against **tempdb** does not perform any checks. This is because, for performance reasons, database snapshots are not available on **tempdb**. This means that the required transactional consistency cannot be obtained. Recycle the server to resolve any **tempdb** metadata issues.  
  
> [!NOTE]  
> DBCC CHECKCATALOG does not check FILESTREAM data. FILESTREAM stores binary large objects (BLOBS) on the file system.  
  
DBCC CHECKCATALOG is also run as part of [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md).
  
## Result Sets  
If no database is specified, DBCC CHECKCATALOG returns:
  
```
DBCC execution completed. If DBCC printed error messages, contact your system administrator.  
```  
  
If [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] is specified as the database name, DBCC CHECKCATALOG returns:
  
```
DBCC execution completed. If DBCC printed error messages, contact your system administrator.  
```  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role, or the **db_owner** fixed database role.  
  
## Examples  
The following example checks the catalog integrity in both the current database and in the `AdventureWorks` database.
  
```sql
-- Check the current database.  
DBCC CHECKCATALOG;  
GO  
-- Check the AdventureWorks2012 database.  
DBCC CHECKCATALOG (AdventureWorks2012);  
GO  
```  
  
## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)  
[System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)
  
