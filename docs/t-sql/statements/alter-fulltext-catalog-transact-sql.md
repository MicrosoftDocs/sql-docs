---
title: "ALTER FULLTEXT CATALOG (Transact-SQL)"
description: ALTER FULLTEXT CATALOG (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER_FULLEXT_CATALOG_TSQL"
  - "ALTER FULLEXT CATALOG"
helpviewer_keywords:
  - "modifying full-text catalogs"
  - "full-text catalogs [SQL Server], rebuilding"
  - "accent sensitivity"
  - "ALTER FULLTEXT CATALOG statement"
  - "full-text catalogs [SQL Server], modifying"
  - "full-text catalogs [SQL Server], reorganizing"
dev_langs:
  - "TSQL"
---
# ALTER FULLTEXT CATALOG (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Changes the properties of a full-text catalog.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql 
ALTER FULLTEXT CATALOG catalog_name   
{ REBUILD [ WITH ACCENT_SENSITIVITY = { ON | OFF } ]  
| REORGANIZE  
| AS DEFAULT   
}  
```  
  
## Arguments
 *catalog_name*  
 Specifies the name of the catalog to be modified. If a catalog with the specified name does not exist, [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error and does not perform the ALTER operation.  
  
 REBUILD  
 Tells [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to rebuild the entire catalog. When a catalog is rebuilt, the existing catalog is deleted and a new catalog is created in its place. All the tables that have full-text indexing references are associated with the new catalog. Rebuilding resets the full-text metadata in the database system tables.  
  
 WITH ACCENT_SENSITIVITY = {ON|OFF}  
 Specifies if the catalog to be altered is accent-sensitive or accent-insensitive for full-text indexing and querying.  
  
 To determine the current accent-sensitivity property setting of a full-text catalog, use the FULLTEXTCATALOGPROPERTY function with the **accentsensitivity** property value against *catalog_name*. If the function returns '1', the full-text catalog is accent sensitive; if the function returns '0', the catalog is not accent sensitive.  
  
 The catalog and database default accent sensitivity are the same.  
  
 REORGANIZE  
 Tells [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to perform a *master merge*, which involves merging the smaller indexes created in the process of indexing into one large index. Merging the full-text index fragments can improve performance and free up disk and memory resources. If there are frequent changes to the full-text catalog, use this command periodically to reorganize the full-text catalog.  
  
 REORGANIZE also optimizes internal index and catalog structures.  
  
 Keep in mind that, depending on the amount of indexed data, a master merge may take some time to complete. Master merging a large amount of data can create a long running transaction, delaying truncation of the transaction log during checkpoint. In this case, the transaction log might grow significantly under the full recovery model. As a best practice, ensure that your transaction log contains sufficient space for a long-running transaction before reorganizing a large full-text index in a database that uses the full recovery model. For more information, see [Manage the Size of the Transaction Log File](../../relational-databases/logs/manage-the-size-of-the-transaction-log-file.md).  
  
 AS DEFAULT  
 Specifies that this catalog is the default catalog. When full-text indexes are created with no specified catalogs, the default catalog is used. If there is an existing default full-text catalog, setting this catalog AS DEFAULT will override the existing default.  
  
## Permissions  
 User must have ALTER permission on the full-text catalog, or be a member of the **db_owner**, **db_ddladmin** fixed database roles, or sysadmin fixed server role.  
  
> [!NOTE]  
>  To use ALTER FULLTEXT CATALOG AS DEFAULT, the user must have ALTER permission on the full-text catalog and CREATE FULLTEXT CATALOG permission on the database.  
  
## Examples  
 The following example changes the `accentsensitivity` property of the default full-text catalog `ftCatalog`, which is accent sensitive.  
  
```sql  
--Change to accent insensitive  
USE AdventureWorks2022;  
GO  
ALTER FULLTEXT CATALOG ftCatalog   
REBUILD WITH ACCENT_SENSITIVITY=OFF;  
GO  
-- Check Accentsensitivity  
SELECT FULLTEXTCATALOGPROPERTY('ftCatalog', 'accentsensitivity');  
GO  
--Returned 0, which means the catalog is not accent sensitive.  
```  

## Remarks
 The full-text catalog REBUILD operation is paused when a session starts executing one or more open DML commands in a transaction involving the index tables present in the catalog to be reconstructed. Until the concurrent transaction completes (commits or rolls back), the full catalog population will not proceed. It is recommended that the administrator monitor this behavior using the DMVs sys.dm_exec_requests and sys.dm_exec_sessions. Locks may be observed between the concurrent session and the background sessions performing the catalog reconstruction, with the wait type LCK_M_IS.
 
 A similar scenario occurs with the REORGANIZE operation; however, you will see the wait type "FT_MASTER_MERGE" in the session where the command is being executed. It is recommended to monitor whether there are one or more sessions running DML commands with long-running open transactions involving the index tables present in the catalog to be reorganized. If this is happening, when using DMVs, you may see one or more background sessions with an LCK_M_IX wait type and the "FT MASTER MERGE" command. The REORGANIZE will not finish until the lock is released.

```sql
--View blocked background sessions
SELECT
    r1.session_id, 
    r1.blocking_session_id,
    r1.wait_type,
    r1.wait_resource,
    r1.last_wait_type,
    r1.command AS BlockedSessionCommand,
    r2.command AS BlockingSessionCommand,
    s1.login_name AS BlockedSessionLogin,
    s2.login_name AS BlockingSessionLogin,
    s1.host_name AS BlockedSessionHost,
    s2.host_name AS BlockingSessionHost,
    r1.status AS BlockedSessionStatus,
    r2.status AS BlockingSessionStatus
FROM sys.dm_exec_requests r1
INNER JOIN sys.dm_exec_sessions s1 ON r1.session_id = s1.session_id
INNER JOIN sys.dm_exec_sessions s2 ON  r1.blocking_session_id = s2.session_id
LEFT JOIN sys.dm_exec_requests r2 ON s2.session_id = r2.session_id
WHERE r1.blocking_session_id <> 0
AND r1.status = 'background'
ORDER BY r1.wait_time DESC;
```
  
## See Also  
 [sys.fulltext_catalogs &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-catalogs-transact-sql.md)   
 [CREATE FULLTEXT CATALOG &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-catalog-transact-sql.md)   
 [DROP FULLTEXT CATALOG &#40;Transact-SQL&#41;](../../t-sql/statements/drop-fulltext-catalog-transact-sql.md)   
 [Full-Text Search](../../relational-databases/search/full-text-search.md)  
  
  
