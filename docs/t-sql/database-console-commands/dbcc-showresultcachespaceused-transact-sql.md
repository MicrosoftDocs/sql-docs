---
title: "DBCC SHOWRESULTCACHESPACEUSED (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/03/2019"
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 73f598cf-b02a-4dba-8d89-9fc0b55a12b8
author: XiaoyuMSFT 
ms.author: xiaoyul
monikerRange: "= azure-sqldw-latest || = sqlallproducts-allversions"
---
# DBCC SHOWRESULTCACHESPACEUSED (Transact-SQL)

[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

Shows the storage space used result set caching for an Azure [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] database.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DBCC SHOWRESULTCACHESPACEUSED  
[;]  
```  
## Remarks

The `DBCC SHOWRESULTCACHESPACEUSED` command doesn't take any parameters and returns the space used by the database where the command is run.

The maximum size of result set cache is 1 TB per database.  Azure SQL Data Warehouse automatically evicts entries in the result set cache:

- every 48 hours if the result set hasn't been used.
- when the result set cache approaches the maximum size.

To manually empty the result set cache for a database, users can use one of these options:

- Turn OFF the result set cache feature for the database
- Run `DBCC DROPRESULTSETCACHE` while connected to the database 

Pausing a database won't empty result set cache.  

## Permissions

Requires VIEW SERVER STATE permission.
  
## Result Sets  
  
|Column|Data Type|Description|  
|------------|---------------|-----------------|  
|reserved_space|bigint|Total space used for the database, in KB. This number will change as the cached result set increases.|  
|data_space|bigint|Space used for data, in KB.|  
|index_space|bigint|Space used for indexes, in KB.|  
|unused_space|bigint|Space that is part of the reserved space and not used, in KB.|  


## See also

[ALTER DATABASE SET Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-set-options?view=azure-sqldw-latest)</br>
[ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql?view=azure-sqldw-latest)</br>
[SET RESULT SET CACHING &#40;Transact-SQL&#41;](/sql/t-sql/statements/set-result-set-caching-transact-sql)</br>
[DBCC DROPRESULTSETCACHE  &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-dropresultsetcache-transact-sql)