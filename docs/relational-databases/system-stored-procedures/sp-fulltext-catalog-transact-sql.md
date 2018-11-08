---
title: "sp_fulltext_catalog (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_fulltext_catalog_TSQL"
  - "sp_fulltext_catalog"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_fulltext_catalog"
ms.assetid: e49b98e4-d1f1-42b2-b16f-eb2fc7aa1cf5
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_fulltext_catalog (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Creates and drops a full-text catalog, and starts and stops the indexing action for a catalog. Multiple full-text catalogs can be created for each database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE FULLTEXT CATALOG](../../t-sql/statements/create-fulltext-catalog-transact-sql.md), [ALTER FULLTEXT CATALOG](../../t-sql/statements/alter-fulltext-catalog-transact-sql.md), and [DROP FULLTEXT CATALOG](../../t-sql/statements/drop-fulltext-catalog-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_fulltext_catalog [ @ftcat= ] 'fulltext_catalog_name' ,   
     [ @action= ] 'action'   
     [ , [ @path= ] 'root_directory' ]   
```  
  
## Arguments  
 [ **@ftcat=**] **'***fulltext_catalog_name***'**  
 Is the name of the full-text catalog. Catalog names must be unique for each database. *fulltext_catalog_name* is **sysname**.  
  
 [ **@action=**] **'***action***'**  
 Is the action to be performed. *action* is **varchar(20)**, and can be one of these values.  
  
> [!NOTE]  
>  Full-text catalogs can be created, dropped, and modified as needed. However, avoid making schema changes on multiple catalogs at the same time. These actions can be performed using the **sp_fulltext_table** stored procedure, which is the recommended way.  
  
|Value|Description|  
|-----------|-----------------|  
|**Create**|Creates an empty, new full-text catalog in the file system and adds an associated row in **sysfulltextcatalogs** with the *fulltext_catalog_name* and *root_directory*, if present, values. *fulltext_catalog_name* must be unique within the database.|  
|**Drop**|Drops *fulltext_catalog_name* by removing it from the file system and deleting the associated row in **sysfulltextcatalogs**. This action fails if this catalog contains indexes for one or more tables. **sp_fulltext_table** '*table_name*', 'drop' should be executed to drop the tables from the catalog.<br /><br /> An error is displayed if the catalog does not exist.|  
|**start_incremental**|Starts an incremental population for *fulltext_catalog_name*. An error is displayed if the catalog does not exist. If a full-text index population is already active, a warning is displayed but no population action occurs. With incremental population only changed rows are retrieved for full-text indexing, provided there is a **timestamp** column present in the table being full-text indexed.|  
|**start_full**|Starts a full population for *fulltext_catalog_name*. Every row of every table associated with this full-text catalog is retrieved for full-text indexing even if they have already been indexed.|  
|**Stop**|Stops an index population for *fulltext_catalog_name*. An error is displayed if the catalog does not exist. No warning is displayed if population is already stopped.|  
|**Rebuild**|Rebuilds *fulltext_catalog_name*. When a catalog is rebuilt, the existing catalog is deleted and a new catalog is created in its place. All the tables that have full-text indexing references are associated with the new catalog. Rebuilding resets the full-text metadata in the database system tables.<br /><br /> If change tracking is OFF, rebuilding does not cause a repopulation of the newly created full-text catalog. In this case, to repopulate, execute **sp_fulltext_catalog** with the **start_full** or **start_incremental** action.|  
  
 [ **@path=**] **'***root_directory***'**  
 Is the root directory (not the complete physical path) for a **create** action. *root_directory* is **nvarchar(100)** and has a default value of NULL, which indicates the use of the default location specified at setup. This is the Ftdata subdirectory in the Mssql directory; for example, C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\FTData. The specified root directory must reside on a drive on the same computer, consist of more than just the drive letter, and cannot be a relative path. Network drives, removable drives, floppy disks, and UNC paths are not supported. Full-text catalogs must be created on a local hard drive associated with an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 **@path** is valid only when *action* is **create**. For actions other than **create** (**stop**, **rebuild**, and so on), **@path** must be NULL or omitted.  
  
 If the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is a virtual server in a cluster, the catalog directory specified needs to be on a shared disk drive on which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource depends. If @path is not specified, the location of default catalog directory is on the shared disk drive, in the directory that was specified when the virtual server was installed.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 The **start_full** action is used to create a complete snapshot of the full-text data in *fulltext_catalog_name*. The **start_incremental** action is used to re-index only the changed rows in the database. Incremental population can be applied only if the table has a column of the type **timestamp**. If a table in the full-text catalog does not contain a column of the type **timestamp**, the table undergoes a full population.  
  
 Full-text catalog and index data is stored in files created in a full-text catalog directory. The full-text catalog directory is created as a sub-directory of the directory specified in **@path** or in the server default full-text catalog directory if **@path** is not specified. The name of the full-text catalog directory is built in a way that guarantees it will be unique on the server. Therefore, all full-text catalog directories on a server can share the same path.  
  
## Permissions  
 The caller is required to be member of the **db_owner** role. Depending on the action requested, the caller should not be denied ALTER or CONTROL permissions (which **db_owner** has) on the target full-text catalog.  
  
## Examples  
  
### A. Create a full-text catalog  
 This example creates an empty full-text catalog, **Cat_Desc**, in the **AdventureWorks2012** database.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_fulltext_catalog 'Cat_Desc', 'create';  
GO  
```  
  
### B. To rebuild a full-text catalog  
 This example rebuilds an existing full-text catalog, **Cat_Desc**, in the **AdventureWorks2012** database.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_fulltext_catalog 'Cat_Desc', 'rebuild';  
GO  
```  
  
### C. Start the population of a full-text catalog  
 This example begins a full population of the **Cat_Desc** catalog.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_fulltext_catalog 'Cat_Desc', 'start_full';  
GO  
```  
  
### D. Stop the population of a full-text catalog  
 This example stops the population of the **Cat_Desc** catalog.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_fulltext_catalog 'Cat_Desc', 'stop';  
GO  
```  
  
### E. To remove a full-text catalog  
 This example removes the **Cat_Desc** catalog.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_fulltext_catalog 'Cat_Desc', 'drop';  
GO  
```  
  
## See Also  
 [FULLTEXTCATALOGPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/fulltextcatalogproperty-transact-sql.md)   
 [sp_fulltext_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-fulltext-database-transact-sql.md)   
 [sp_help_fulltext_catalogs &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-fulltext-catalogs-transact-sql.md)   
 [sp_help_fulltext_catalogs_cursor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-fulltext-catalogs-cursor-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Full-Text Search](../../relational-databases/search/full-text-search.md)  
  
  
