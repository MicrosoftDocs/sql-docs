---
title: "DROP PARTITION FUNCTION (Transact-SQL)"
description: DROP PARTITION FUNCTION (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP PARTITION FUNCTION"
  - "DROP_PARTITION_FUNCTION_TSQL"
helpviewer_keywords:
  - "deleting partition functions"
  - "DROP PARTITION FUNCTION statement"
  - "partition functions [SQL Server], removing"
  - "dropping partition functions"
  - "removing partition functions"
dev_langs:
  - "TSQL"
---
# DROP PARTITION FUNCTION (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Removes a partition function from the current database. Partition functions are created by using CREATE PARTITION FUNCTION and modified by using ALTER PARTITION FUNCTION.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql 
DROP PARTITION FUNCTION partition_function_name [ ; ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *partition_function_name*  
 Is the name of the partition function that is to be dropped.  
  
## Remarks  
 A partition function can be dropped only if there are no partition schemes currently using the partition function. If there are partition schemes using the partition function, DROP PARTITION FUNCTION returns an error.  
  
## Permissions  
 Any one of the following permissions can be used to execute DROP PARTITION FUNCTION:  
  
-   ALTER ANY DATASPACE permission. This permission defaults to members of the **sysadmin** fixed server role and the **db_owner** and **db_ddladmin** fixed database roles.  
  
-   CONTROL or ALTER permission on the database in which the partition function was created.  
  
-   CONTROL SERVER or ALTER ANY DATABASE permission on the server of the database in which the partition function was created.  
  
## Examples  
 The following example assumes the partition function `myRangePF` has been created in the current database.  
  
```sql 
DROP PARTITION FUNCTION myRangePF;  
```  
  
## See Also  
 [CREATE PARTITION FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-function-transact-sql.md)   
 [ALTER PARTITION FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-partition-function-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sys.partition_functions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-functions-transact-sql.md)   
 [sys.partition_parameters &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-parameters-transact-sql.md)   
 [sys.partition_range_values &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-range-values-transact-sql.md)   
 [sys.partitions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partitions-transact-sql.md)   
 [sys.tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-tables-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)   
 [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)  
  
  
