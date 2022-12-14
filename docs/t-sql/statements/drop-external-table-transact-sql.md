---
title: "DROP EXTERNAL TABLE (Transact-SQL)"
description: DROP EXTERNAL TABLE (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "11/02/2021"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP EXTERNAL TABLE (Transact-SQL)
[!INCLUDE [sqlserver2016-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdbmi-asa-pdw.md)]

  Removes a PolyBase external table from a database, but doesn't delete the external data.  
  
 ![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP EXTERNAL TABLE { database_name.schema_name.table_name | schema_name.table_name | table_name }
[;]  
```  
 
## Arguments  

 `[ database_name . [schema_name] . | schema_name . ] table_name`  
 The one- to three-part name of the external table to remove. The table name can optionally include the schema, or the database and schema.  
  
## Permissions  
  
 Requires: 
 - **ALTER** permission on the schema to which the table belongs.
 - **ALTER ANY EXTERNAL DATA SOURCE**
 - **ALTER ANY EXTERNAL FILE FORMAT**
  
## Remarks  
 Dropping an external table removes all table-related metadata. It doesn't delete the external data.  
  
## Examples  
  
### A. Using basic syntax  
  
```sql  
DROP EXTERNAL TABLE SalesPerson;  
DROP EXTERNAL TABLE dbo.SalesPerson;  
DROP EXTERNAL TABLE EasternDivision.dbo.SalesPerson;  
```  
  
### B. Dropping an external table from the current database  
 The following example removes the `ProductVendor1` table, its data, indexes, and any dependent views from the current database.  
  
```sql  
DROP EXTERNAL TABLE ProductVendor1;  
```  
  
### C. Dropping a table from another database  
 The following example drops the `SalesPerson` table in the `EasternDivision` database.  
  
```sql  
DROP EXTERNAL TABLE EasternDivision.dbo.SalesPerson;  
```  
  
## See Also  
 [CREATE EXTERNAL TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-transact-sql.md)  
  
