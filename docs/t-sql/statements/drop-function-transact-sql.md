---
title: "DROP FUNCTION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/28/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, database-engine, pdw, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_FUNCTION_TSQL"
  - "DROP FUNCTION"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "user-defined functions [SQL Server], removing"
  - "removing user-defined functions"
  - "DROP FUNCTION statement"
  - "dropping user-defined functions"
  - "deleting user-defined functions"
ms.assetid: ee5ad283-9e44-4109-902f-0ce12669ee11
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP FUNCTION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Removes one or more user-defined functions from the current database. User-defined functions are created by using [CREATE FUNCTION](../../t-sql/statements/create-function-transact-sql.md) and modified by using [ALTER FUNCTION](../../t-sql/statements/alter-function-transact-sql.md).  
  
 The DROP function supports natively compiled, scalar user-defined functions. For more information, see [Scalar User-Defined Functions for In-Memory OLTP](../../relational-databases/in-memory-oltp/scalar-user-defined-functions-for-in-memory-oltp.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
 -- SQL Server, Azure SQL Database 

DROP FUNCTION [ IF EXISTS ] { [ schema_name. ] function_name } [ ,...n ]   
[;]
```

```  
 -- Azure SQL Data Warehouse, Parallel Data Warehouse 

DROP FUNCTION [ schema_name. ] function_name
[;] 
```  
   
  
## Arguments  
 *IF EXISTS*    
 Conditionally drops the function only if it already exists. Available beginning with [!INCLUDE[ssnoversion_md](../../includes/ssnoversion-md.md)] 2016 and in [!INCLUDE[sssds_md](../../includes/sssds-md.md)].
  
 *schema_name*  
 Is the name of the schema to which the user-defined function belongs.  
  
 *function_name*  
 Is the name of the user-defined function or functions to be removed. Specifying the schema name is optional. The server name and database name cannot be specified.  
  
## Remarks  
 DROP FUNCTION will fail if there are [!INCLUDE[tsql](../../includes/tsql-md.md)] functions or views in the database that reference this function and were created by using SCHEMABINDING, or if there are computed columns, CHECK constraints, or DEFAULT constraints that reference the function.  
  
 DROP FUNCTION will fail if there are computed columns that reference this function and have been indexed.  
  
## Permissions  
 To execute DROP FUNCTION, at a minimum, a user must have ALTER permission on the schema to which the function belongs, or CONTROL permission on the function.  
  
## Examples  
  
### A. Dropping a function  
 The following example drops the `fn_SalesByStore` user-defined function from the `Sales` schema in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database. To create this function, see Example B in [CREATE FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-function-transact-sql.md).  
  
```  
DROP FUNCTION Sales.fn_SalesByStore;  
```  
  
## See Also  
 [ALTER FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-function-transact-sql.md)   
 [CREATE FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-function-transact-sql.md)   
 [OBJECT_ID &#40;Transact-SQL&#41;](../../t-sql/functions/object-id-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)   
 [sys.parameters &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-parameters-transact-sql.md)  
  
  
