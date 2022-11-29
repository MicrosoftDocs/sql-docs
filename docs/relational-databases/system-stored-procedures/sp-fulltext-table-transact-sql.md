---
description: "sp_fulltext_table (Transact-SQL)"
title: "sp_fulltext_table (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_fulltext_table_TSQL"
  - "sp_fulltext_table"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_fulltext_table"
ms.assetid: a765f311-07fc-4af3-b74c-e9a027fbecce
author: markingmyname
ms.author: maghan
monikerRange: "=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_fulltext_table (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-xxx-md.md)]

  Marks or unmarks a table for full-text indexing.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE FULLTEXT INDEX](../../t-sql/statements/create-fulltext-index-transact-sql.md), [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md), and [DROP FULLTEXT INDEX](../../t-sql/statements/drop-fulltext-index-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_fulltext_table   
   [ @tabname= ] 'qualified_table_name'           
   , [ @action= ] 'action'   
   [   
   , [ @ftcat= ] 'fulltext_catalog_name'           
   , [ @keyname= ] 'unique_index_name'   
   ]  
```  
  
## Arguments  
`[ @tabname = ] 'qualified_table_name'`
 Is a one- or two-part table name. The table must exist in the current database. *qualified_table_name* is **nvarchar(517)**, with no default.  
  
`[ @action = ] 'action'`
 Is the action to be performed. *action* is **nvarchar(50)**, with no default, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**Create**|Creates the metadata for a full-text index for the table referenced by *qualified_table_name* and specifies that the full-text index data for this table should reside in *fulltext_catalog_name*. This action also designates the use of *unique_index_name* as the full-text key column. This unique index must already be present and must be defined on one column of the table.<br /><br /> A full-text search cannot be performed against this table until the full-text catalog is populated.|  
|**Drop**|Drops the metadata on the full-text index for *qualified_table_name*. If the full-text index is active, it is automatically deactivated before being dropped. It is not necessary to remove columns before dropping the full-text index.|  
|**Activate**|Activates the ability for full-text index data to be gathered for *qualified_table_name*, after it has been deactivated. There must be at least one column participating in the full-text index before it can be activated.<br /><br /> A full-text index is automatically made active (for population) as soon as the first column is added for indexing. If the last column is dropped from the index, the index becomes inactive. If change tracking is on, activating an inactive index starts a new population.<br /><br /> Note that this does not actually populate the full-text index, but simply registers the table in the full-text catalog in the file system so that rows from *qualified_table_name* can be retrieved during the next full-text index population.|  
|**Deactivate**|Deactivates the full-text index for *qualified_table_name* so that full-text index data can no longer be gathered for the *qualified_table_name*. The full-text index metadata remains and the table can be reactivated.<br /><br /> If change tracking is on, deactivating an active index freezes the state of the index: any ongoing population is stopped, and no more changes are propagated to the index.|  
|**start_change_tracking**|Start an incremental population of the full-text index. If the table does not have a timestamp, start a full population of the full-text index. Start tracking changes to the table.<br /><br /> Full-text change tracking does not track any WRITETEXT or UPDATETEXT operations performed on full-text indexed columns that are of type **image**, **text**, or **ntext**.|  
|**stop_change_tracking**|Stop tracking changes to the table.|  
|**update_index**|Propagate the current set of tracked changes to the full-text index.|  
|**start_background_updateindex**|Start propagating tracked changes to the full-text index as they occur.|  
|**stop_background_updateindex**|Stop propagating tracked changes to the full-text index as they occur.|  
|**start_full**|Start a full population of the full-text index for the table.|  
|**start_incremental**|Start an incremental population of the full-text index for the table.|  
|**Stop**|Stop a full or incremental population.|  
  
`[ @ftcat = ] 'fulltext_catalog_name'`
 Is a valid, existing full-text catalog name for a **create** action. For all other actions, this parameter must be NULL. *fulltext_catalog_name* is **sysname**, with a default of NULL.  
  
`[ @keyname = ] 'unique_index_name'`
 Is a valid single-key-column, unique nonnullable index on *qualified_table_name* for a **create** action. For all other actions, this parameter must be NULL. *unique_index_name* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 After a full-text index is deactivated for a particular table, the existing full-text index remains in place until the next full population; however, this index is not used because [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] blocks queries on deactivated tables.  
  
 If the table is reactivated and the index is not repopulated, the old index is still available for queries against any remaining, but not new, full-text enabled columns. Data from deleted columns are matched in queries that specify an all-full-text column search.  
  
 After a table has been defined for full-text indexing, switching the full-text unique key column from one data type to another, either by changing the data type of that column or changing the full-text unique key from one column to another, without a full repopulation may cause a failure to occur during a subsequent query and returning the error message: "Conversion to type *data_type* failed for full-text search key value *key_value*." To prevent this, drop the full-text definition for this table using the **drop** action of **sp_fulltext_table** and redefine it using **sp_fulltext_table** and **sp_fulltext_column**.  
  
 The full-text key column must be defined to be 900 bytes or less. It is recommended that the size of the key column be as small as possible for performance reasons.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role, **db_owner** and **db_ddladmin** fixed database roles, or a user with reference permissions on the full-text catalog can execute **sp_fulltext_table**.  
  
## Examples  
  
### A. Enabling a table for full-text indexing  
 The following example creates full-text index metadata for the `Document` table of the `AdventureWorks` database. `Cat_Desc` is a full-text catalog. `PK_Document_DocumentID` is a unique, single-column index on `Document`.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_fulltext_table 'Production.Document', 'create', 'Cat_Desc', 'PK_Document_DocumentID';  
--Add some columns  
EXEC sp_fulltext_column 'Production.Document','DocumentSummary','add';  
-- Activate the full-text index  
EXEC sp_fulltext_table 'Production.Document','activate';  
GO  
```  
  
### B. Activating and propagating track changes  
 The following example activates and starts propagating tracked changes to the full-text index as they occur.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_fulltext_table 'Production.Document', 'Start_change_tracking';  
EXEC sp_fulltext_table 'Production.Document', 'Start_background_updateindex';  
GO  
```  
  
### C. Removing a full-text index  
 This example removes the full-text index metadata for the `Document` table of the `AdventureWorks` database.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_fulltext_table 'Production.Document', 'drop';  
GO  
```  
  
## See Also  
 [INDEXPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/indexproperty-transact-sql.md)   
 [OBJECTPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/objectproperty-transact-sql.md)   
 [sp_help_fulltext_tables &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-fulltext-tables-transact-sql.md)   
 [sp_help_fulltext_tables_cursor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-fulltext-tables-cursor-transact-sql.md)   
 [sp_helpindex &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpindex-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Full-Text Search and Semantic Search Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/full-text-search-and-semantic-search-stored-procedures-transact-sql.md)  
  
  
