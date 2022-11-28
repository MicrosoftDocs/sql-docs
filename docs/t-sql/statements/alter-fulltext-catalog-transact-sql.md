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
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql 
ALTER FULLTEXT CATALOG catalog_name   
{ REBUILD [ WITH ACCENT_SENSITIVITY = { ON | OFF } ]  
| REORGANIZE  
| AS DEFAULT   
}  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
USE AdventureWorks2012;  
GO  
ALTER FULLTEXT CATALOG ftCatalog   
REBUILD WITH ACCENT_SENSITIVITY=OFF;  
GO  
-- Check Accentsensitivity  
SELECT FULLTEXTCATALOGPROPERTY('ftCatalog', 'accentsensitivity');  
GO  
--Returned 0, which means the catalog is not accent sensitive.  
```  
  
## See Also  
 [sys.fulltext_catalogs &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-catalogs-transact-sql.md)   
 [CREATE FULLTEXT CATALOG &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-catalog-transact-sql.md)   
 [DROP FULLTEXT CATALOG &#40;Transact-SQL&#41;](../../t-sql/statements/drop-fulltext-catalog-transact-sql.md)   
 [Full-Text Search](../../relational-databases/search/full-text-search.md)  
  
  
