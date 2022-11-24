---
title: "FULLTEXTCATALOGPROPERTY (Transact-SQL)"
description: "FULLTEXTCATALOGPROPERTY (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "FULLTEXTCATALOGPROPERTY_TSQL"
  - "FULLTEXTCATALOGPROPERTY"
helpviewer_keywords:
  - "full-text catalogs [SQL Server], properties"
  - "FULLTEXTCATALOGPROPERTY function"
  - "status information [SQL Server], full-text catalogs"
dev_langs:
  - "TSQL"
---
# FULLTEXTCATALOGPROPERTY (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns information about full-text catalog properties in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)].  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
FULLTEXTCATALOGPROPERTY ('catalog_name' ,'property')  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
  
> [!NOTE]  
>  The following properties will be removed in a future release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]: **LogSize** and **PopulateStatus**. Avoid using these properties in new development work, and plan to modify applications that currently use any of them.  
  
_catalog\_name_  
Is an expression containing the name of the full-text catalog.  
  
_property_  
Is an expression containing the name of the full-text catalog property. The table lists the properties and provides descriptions of the information returned.  
  
|Property|Description|  
|--------------|-----------------|  
|**AccentSensitivity**|Accent-sensitivity setting.<br /><br /> 0 = Accent insensitive<br /><br /> 1 = Accent sensitive|  
|**IndexSize**|Logical size of the full-text catalog in megabytes (MB). Includes the size of semantic key phrase and document similarity indexes.<br /><br /> For more information, see "Remarks," later in this topic.|  
|**ItemCount**|Number of indexed items including all full-text, keyphrase, and document similarity indexes in a catalog|  
|**LogSize**|Supported for backward compatibility only. Always returns 0.<br /><br /> Size, in bytes, of the combined set of error logs associated with a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Search Service full-text catalog.|  
|**MergeStatus**|Indicates whether a master merge is in progress.<br /><br /> 0 = master merge isn't in progress<br /><br /> 1 = master merge is in progress|  
|**PopulateCompletionAge**|The difference in seconds between the completion of the last full-text index population and 01/01/1990 00:00:00.<br /><br /> Only updated for full and incremental crawls. Returns 0 if no population has occurred.|  
|**PopulateStatus**|0 = Idle<br /><br /> 1 = Full population in progress<br /><br /> 2 = Paused<br /><br /> 3 = Throttled<br /><br /> 4 = Recovering<br /><br /> 5 = Shutdown<br /><br /> 6 = Incremental population in progress<br /><br /> 7 = Building index<br /><br /> 8 = Disk is full. Paused.<br /><br /> 9 = Change tracking|  
|**UniqueKeyCount**|Number of unique keys in the full-text catalog.|  
|**ImportStatus**|Indicates whether the full-text catalog is being imported.<br /><br /> 0 = The full-text catalog isn't being imported.<br /><br /> 1 = The full-text catalog is being imported.|  
  
## Return Types  
**int**  
  
## Exceptions  
Returns NULL on error or if a caller doesn't have permission to view the object.  
  
In [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], a user can only view the metadata of securables. These securables are ones that the user owns or on which the user has been granted permission. As such, built-in functions that emit metadata, such as FULLTEXTCATALOGPROPERTY, may return NULL if the user doesn't have any permission on the object. For more information, see [sp_help_fulltext_catalogs &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-fulltext-catalogs-transact-sql.md).  
  
## Remarks  
FULLTEXTCATALOGPROPERTY ('_catalog\_name_','**IndexSize**') looks at only fragments with status 4 or 6 as shown in [sys.fulltext_index_fragments](../../relational-databases/system-catalog-views/sys-fulltext-index-fragments-transact-sql.md). These fragments are part of the logical index. As such, the **IndexSize** property returns only the logical index size. 

During an index merge, however, the actual index size might be double its logical size. To find the actual size that is being consumed by a full-text index during a merge, use the [sp_spaceused](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md) system stored procedure. That procedure looks at all fragments associated with a full-text index. 

The full-text population may fail. It may fail if you restrict the growth of the full-text catalog file and don't allow enough space for the merge process. In this case, FULLTEXTCATALOGPROPERTY ('_catalog\_name_','**IndexSize**') returns 0 and the following error is written to the full-text log:  
  
`Error: 30059, Severity: 16, State: 1. A fatal error occurred during a full-text population and caused the population to be cancelled. Population type is: FULL; database name is FTS_Test (id: 13); catalog name is t1_cat (id: 5); table name t1 (id: 2105058535). Fix the errors that are logged in the full-text crawl log. Then, resume the population. The basic Transact-SQL syntax for this is: ALTER FULLTEXT INDEX ON table_name RESUME POPULATION.`  
  
It's important that applications don't wait in a tight loop, checking for the **PopulateStatus** property to become idle. Becoming idle indicates that population has completed. This check takes CPU cycles away from the database and full-text search processes, and causes timeouts. It's usually a better option to check the corresponding **PopulateStatus** property at the table level, **TableFullTextPopulateStatus** in the OBJECTPROPERTYEX system function. This and other new full-text properties in OBJECTPROPERTYEX provide more granular information about full-text indexing tables. For more information, see [OBJECTPROPERTYEX &#40;Transact-SQL&#41;](../../t-sql/functions/objectpropertyex-transact-sql.md).  
  
## Examples  
The following example returns the number of full-text indexed items in a full-text catalog named `Cat_Desc`.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT fulltextcatalogproperty('Cat_Desc', 'ItemCount');  
GO  
```  
  
## See Also  
[FULLTEXTSERVICEPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/fulltextserviceproperty-transact-sql.md)   
[Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
[sp_help_fulltext_catalogs &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-fulltext-catalogs-transact-sql.md)  
  
  
