---
title: "DROP PARTITION FUNCTION (Transact-SQL)"
description: DROP PARTITION FUNCTION (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
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
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Removes a partition function from the current database. Partition functions are created by using CREATE PARTITION FUNCTION and modified by using ALTER PARTITION FUNCTION.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql 
DROP PARTITION FUNCTION partition_function_name [ ; ]  
```  
  
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
  
## Related content

- [CREATE PARTITION FUNCTION (Transact-SQL)](create-partition-function-transact-sql.md)
- [ALTER PARTITION FUNCTION (Transact-SQL)](alter-partition-function-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
- [sys.partition_functions (Transact-SQL)](../../relational-databases/system-catalog-views/sys-partition-functions-transact-sql.md)
- [sys.partition_parameters (Transact-SQL)](../../relational-databases/system-catalog-views/sys-partition-parameters-transact-sql.md)
- [sys.partition_range_values (Transact-SQL)](../../relational-databases/system-catalog-views/sys-partition-range-values-transact-sql.md)
- [sys.partitions (Transact-SQL)](../../relational-databases/system-catalog-views/sys-partitions-transact-sql.md)
- [sys.tables (Transact-SQL)](../../relational-databases/system-catalog-views/sys-tables-transact-sql.md)
- [sys.indexes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)
- [sys.index_columns (Transact-SQL)](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)
