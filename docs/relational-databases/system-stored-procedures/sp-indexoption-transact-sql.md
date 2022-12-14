---
description: "sp_indexoption (Transact-SQL)"
title: "sp_indexoption (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_indexoption"
  - "sp_indexoption_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_indexoption"
ms.assetid: 75f836be-d322-4a53-a45d-25bee6b42a52
author: markingmyname
ms.author: maghan
---
# sp_indexoption (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Sets locking option values for user-defined clustered and nonclustered indexes or tables with no clustered index.  
  
 The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] automatically makes choices of page-, row-, or table-level locking. You do not have to set these options manually. **sp_indexoption** is provided for expert users who know with certainty that a particular type of lock is always appropriate.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Instead, use [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_indexoption [ @IndexNamePattern = ] 'table_or_index_name'   
    , [ @OptionName = ] 'option_name'   
    , [ @OptionValue = ] 'value'  
```  
  
## Arguments  
`[ @IndexNamePattern = ] 'table_or_index_name'`
 Is the qualified or nonqualified name of a user-defined table or index. *table_or_index_name* is **nvarchar(1035)**, with no default. Quotation marks are required only if a qualified index or table name is specified. If a fully qualified table name, including a database name, is provided, the database name must be the name of the current database. If a table name is specified with no index, the specified option value is set for all indexes on that table and the table itself if no clustered index exists.  
  
`[ @OptionName = ] 'option_name'`
 Is an index option name. *option_name* is **varchar(35)**, with no default. *option_name* can have one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**AllowRowLocks**|When TRUE, row locks are allowed when accessing the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when row locks are used. When FALSE, row locks are not used. The default is TRUE.|  
|**AllowPageLocks**|When TRUE, page locks are allowed when accessing the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when page locks are used. When FALSE, page locks are not used. The default is TRUE.|  
|**DisAllowRowLocks**|When TRUE, row locks are not used. When FALSE, row locks are allowed when accessing the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when row locks are used.|  
|**DisAllowPageLocks**|When TRUE, page locks are not used. When FALSE, page locks are allowed when accessing the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when page locks are used.|  
  
`[ @OptionValue = ] 'value'`
 Specifies whether the *option_name* setting is enabled (TRUE, ON, yes, or 1) or disabled (FALSE, OFF, no, or 0). *value* is **varchar(12)**, with no default.  
  
## Return Code Values  
 0 (success) or greater than 0 (failure)  
  
## Remarks  
 XML indexes are not supported. If an XML index is specified, or a table name is specified with no index name and the table contains an XML index, the statement fails. To set these options, use [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md) instead.  
  
 To display the current row and page locking properties, use [INDEXPROPERTY](../../t-sql/functions/indexproperty-transact-sql.md) or the [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) catalog view.  
  
-   Row-, page-, and table-level locks are allowed when accessing the index when **AllowRowLocks** = TRUE or **DisAllowRowLocks** = FALSE, and **AllowPageLocks** = TRUE or **DisAllowPageLocks** = FALSE. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] chooses the appropriate lock and can escalate the lock from a row or page lock to a table lock.  
  
 Only a table-level lock is allowed when accessing the index when **AllowRowLocks** = FALSE or **DisAllowRowLocks** = TRUE and **AllowPageLocks** = FALSE or **DisAllowPageLocks** = TRUE.  
  
 If a table name is specified with no index, the settings are applied to all indexes on that table. When the underlying table has no clustered index (that is, it is a heap) the settings are applied as follows:  
  
-   When **AllowRowLocks** or **DisAllowRowLocks** are set to TRUE or FALSE, the setting is applied to the heap and any associated nonclustered indexes.  
  
-   When **AllowPageLocks** option is set to TRUE or **DisAllowPageLocks** is set to FALSE, the setting is applied to the heap and any associated nonclustered indexes.  
  
-   When **AllowPageLocks** option is set FALSE or **DisAllowPageLocks** is set to TRUE, the setting is fully applied to the nonclustered indexes. That is, all page locks are disallowed on the nonclustered indexes. On the heap, only the shared (S), update (U), and exclusive (X) locks for the page are disallowed. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] can still acquire an intent page lock (IS, IU or IX) for internal purposes.  
  
## Permissions  
 Requires ALTER permission on the table.  
  
## Examples  
  
### A. Setting an option on a specific index  
 The following example disallows page locks on the `IX_Customer_TerritoryID` index on the `Customer` table.  
  
```sql  
USE AdventureWorks2012;  
GO  
EXEC sp_indexoption N'Sales.Customer.IX_Customer_TerritoryID',  
    N'disallowpagelocks', TRUE;  
```  
  
### B. Setting an option on all indexes on a table  
 The following example disallows row locks on all indexes associated with the `Product` table. The `sys.indexes` catalog view is queried before and after executing the `sp_indexoption` procedure to show the results of the statement.  
  
```sql  
USE AdventureWorks2012;  
GO  
--Display the current row and page lock options for all indexes on the table.  
SELECT name, type_desc, allow_row_locks, allow_page_locks   
FROM sys.indexes  
WHERE object_id = OBJECT_ID(N'Production.Product');  
GO  
-- Set the disallowrowlocks option on the Product table.   
EXEC sp_indexoption N'Production.Product',  
    N'disallowrowlocks', TRUE;  
GO  
--Verify the row and page lock options for all indexes on the table.  
SELECT name, type_desc, allow_row_locks, allow_page_locks   
FROM sys.indexes  
WHERE object_id = OBJECT_ID(N'Production.Product');  
GO  
```  
  
### C. Setting an option on a table with no clustered index  
 The following example disallows page locks on a table with no clustered index (a heap). The `sys.indexes` catalog view is queried before and after the `sp_indexoption` procedure is executed to show the results of the statement.  
  
```sql  
USE AdventureWorks2012;  
GO  
--Display the current row and page lock options of the table.   
SELECT OBJECT_NAME (object_id) AS [Table], type_desc, allow_row_locks, allow_page_locks   
FROM sys.indexes  
WHERE OBJECT_NAME (object_id) = N'DatabaseLog';  
GO  
-- Set the disallowpagelocks option on the table.   
EXEC sp_indexoption DatabaseLog,  
    N'disallowpagelocks', TRUE;  
GO  
--Verify the row and page lock settings of the table.  
SELECT OBJECT_NAME (object_id) AS [Table], allow_row_locks, allow_page_locks   
FROM sys.indexes  
WHERE OBJECT_NAME (object_id) = N'DatabaseLog';  
GO  
```  
  
## See Also  
 [INDEXPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/indexproperty-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)  
  
  
