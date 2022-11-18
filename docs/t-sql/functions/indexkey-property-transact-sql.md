---
title: "INDEXKEY_PROPERTY (Transact-SQL)"
description: "INDEXKEY_PROPERTY (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "INDEXKEY_PROPERTY_TSQL"
  - "INDEXKEY_PROPERTY"
helpviewer_keywords:
  - "index keys [SQL Server]"
  - "INDEXKEY_PROPERTY function"
  - "viewing index keys"
  - "displaying index keys"
  - "keys [SQL Server], index"
dev_langs:
  - "TSQL"
---
# INDEXKEY_PROPERTY (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns information about the index key. Returns NULL for XML indexes.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Instead, use [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
INDEXKEY_PROPERTY ( object_ID ,index_ID ,key_ID ,property )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *object_ID*  
 Is the object identification number of the table or indexed view. *object_ID* is **int**.  
  
 *index_ID*  
 Is the index identification number. *index_ID* is **int**.  
  
 *key_ID*  
 Is the index key column position. *key_ID* is **int**.  
  
 *property*  
 Is the name of the property for which information will be returned. *property* is a character string and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**ColumnId**|Column ID at the *key_ID* position of the index.|  
|**IsDescending**|Order in which the index column is stored.<br /><br /> 1 = Descending 0 = Ascending|  
  
## Return Types  
 **int**  
  
## Exceptions  
 Returns NULL on error or if a caller does not have permission to view the object.  
  
 A user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as INDEXKEY_PROPERTY may return NULL if the user does not have any permission on the object. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
 In the following example, both properties are returned for index ID `1`, key column `1` in the `Production.Location` table.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT   
    INDEXKEY_PROPERTY(OBJECT_ID('Production.Location', 'U'),  
        1,1,'ColumnId') AS [Column ID],  
    INDEXKEY_PROPERTY(OBJECT_ID('Production.Location', 'U'),  
        1,1,'IsDescending') AS [Asc or Desc order];  
```  
  
 Here is the result set:  
  
```  
Column ID   Asc or Desc order   
----------- -----------------   
1           0  
  
(1 row(s) affected)  
```  
  
## See Also  
 [INDEX_COL &#40;Transact-SQL&#41;](../../t-sql/functions/index-col-transact-sql.md)   
 [INDEXPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/indexproperty-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)   
 [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)  
  
  
