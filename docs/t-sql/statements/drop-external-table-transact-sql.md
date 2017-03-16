---
title: "DROP EXTERNAL TABLE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 02a6a236-0756-4570-abfa-6f677a7df042
caps.latest.revision: 12
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# DROP EXTERNAL TABLE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-pdw_md](../../includes/tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md)]

  Removes a PolyBase external table from. This does not delete the external data.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
DROP EXTERNAL TABLE [ database_name . [schema_name ] . | schema_name . ] table_name   
[;]  
```  
  

## Arguments  
 [ *database_name* . [*schema_name*] . | *schema_name* . ] *table_name*  
 The one- to three-part name of the external table to remove. The table name can optionally include the schema, or the database and schema.  
  
## Permissions  
  
-   Requires **ALTER** permission on the schema to which the table belongs.  
  
## General Remarks  
 Dropping an external table removes all table-related metadata. It does not delete the external data.  
  
## Examples  
  
### A. Using basic syntax  
  
```  
DROP EXTERNAL TABLE SalesPerson;  
DROP EXTERNAL TABLE dbo.SalesPerson;  
DROP EXTERNAL TABLE EasternDivision.dbo.SalesPerson;  
```  
  
### B. Dropping an external table from the current database  
 The following example removes the `ProductVendor1` table, its data, indexes, and any dependent views from the current database.  
  
```  
DROP EXTERNAL TABLE ProductVendor1;  
```  
  
### C. Dropping a table from another database  
 The following example drops the `SalesPerson` table in the `EasternDivision` database.  
  
```  
DROP EXTERNAL TABLE EasternDivision.dbo.SalesPerson;  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### D. Using basic syntax  
  
```  
DROP EXTERNAL TABLE SalesPerson;  
DROP EXTERNAL TABLE dbo.SalesPerson;  
DROP EXTERNAL TABLE EasternDivision.dbo.SalesPerson;  
```  
  
### E. Dropping an external table from the current database  
 The following example removes the `ProductVendor1` table, its data, indexes, and any dependent views from the current database.  
  
```  
DROP EXTERNAL TABLE ProductVendor1;  
```  
  
### F. Dropping a table from another database  
 The following example drops the `SalesPerson` table in the `EasternDivision` database.  
  
```  
DROP EXTERNAL TABLE EasternDivision.dbo.SalesPerson;  
```  
  
## See Also  
 [CREATE EXTERNAL TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-transact-sql.md)  
  
  

