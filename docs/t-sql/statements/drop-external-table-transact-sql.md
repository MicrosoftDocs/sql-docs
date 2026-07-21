---
title: "DROP EXTERNAL TABLE (Transact-SQL)"
description: DROP EXTERNAL TABLE removes an external table from a database, but doesn't delete the external data.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: hudequei, wiassaf
ms.date: 05/13/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP_EXTERNAL_TABLE"
  - "DROP EXTERNAL TABLE"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# DROP EXTERNAL TABLE (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw-fabricdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw-fabricdw.md)]

 Removes an [external table](create-external-table-transact-sql.md) from a database, but doesn't delete the external data.  

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

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

<a id="a-using-basic-syntax"></a>

### A. Use basic syntax

```sql
DROP EXTERNAL TABLE SalesPerson;  
DROP EXTERNAL TABLE dbo.SalesPerson;  
DROP EXTERNAL TABLE EasternDivision.dbo.SalesPerson;  
```  

<a id="b-dropping-an-external-table-from-the-current-database"></a>

### B. Drop an external table from the current database
 The following example removes the `ProductVendor1` table, its data, indexes, and any dependent views from the current database.  

```sql
DROP EXTERNAL TABLE ProductVendor1;  
```  

<a id="c-dropping-a-table-from-another-database"></a>

### C. Drop a table from another database
 The following example drops the `SalesPerson` table in the `EasternDivision` database.  

```sql
DROP EXTERNAL TABLE EasternDivision.dbo.SalesPerson;  
```  

## Related content

- [CREATE EXTERNAL TABLE (Transact-SQL)](create-external-table-transact-sql.md)
