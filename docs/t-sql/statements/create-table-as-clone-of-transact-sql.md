---
title: CREATE TABLE AS CLONE OF
description: "CREATE TABLE AS CLONE OF creates a new table as a clone of another table in Synapse Data Warehouse in Microsoft Fabric."
author: ajagadish-24
ms.author: ajagadish
ms.reviewer: wiassaf
ms.date: 10/04/2023
ms.service: sql
ms.topic: reference
dev_langs:
  - "TSQL"
monikerRange: "=fabric"
---
# CREATE TABLE AS CLONE OF

[!INCLUDE [applies-to-version/fabric-dw](../../includes/applies-to-version/fabric-dw.md)]

Creates a new table as a zero-copy clone of another table in [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] in [!INCLUDE [fabric](../../includes/fabric.md)]. Only the metadata of the table is copied. The underlying data of the table, stored as parquet files, is not copied.

For more information on cloning a table in [!INCLUDE [fabric](../../includes/fabric.md)], see [Clone table](/fabric/data-warehouse/tutorial-clone-table).

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax
  
```syntaxsql
CREATE TABLE 
    { database_name.schema_name.table_name | schema_name.table_name | table_name } 
AS CLONE OF 
    { database_name.schema_name.table_name | schema_name.table_name | table_name } AT {point_in_time}
```  

## Arguments

#### *database_name*

 The name of the database that will contain the new cloned table. The default is the current database.  
  
#### *schema_name*

The schema of the table where the table clone is located. Specifying *schema* is optional when table is cloned within the same schema. If blank, the default schema is used.

Providing the schema name is required when a table is cloned across schemas.
  
#### *table_name*

The name of the cloned table. Table names must follow the [rules for identifiers](../../relational-databases/databases/database-identifiers.md?view=fabric&preserve-view=true). *table_name* can be a maximum of 128 characters.

### *point_in_time

At the 'datetime' provided, the table clone accurately mirrors the data as it was at that particular date and time. It is a Datetime datatype.
'YYYY-MM-DDThh:mm:ss' is the format where 'T' indicates the start of time part of the datetime value.

Specifying the *datetime is not required for creating table clones as of current point in time.

Note: This specification is necessary only when there is a need to create a table clone at a specific historical time. In cases where a table clone is intended to be generated based on the current moment, there is no need to explicitly specify this timestamp.

## Permissions

Requires: 
 - SELECT permission on the entire source table
 - CREATE TABLE permissions on the schema in which the clone of the table will be created
  
## Constraints

Primary and unique key constraints defined in the source table will be replicated when creating a clone.
 
## Limitations

- Table clones across various warehouses within a workspace is currently not supported.
- The tables present in the Lakehouse generated SQL Endpoint is currently not supported to clone.
- Cloned tables do not currently inherit row-level security or dynamic data masking.

## Examples

## A. Create a table clone within the same schema

```sql
--Clone creation within the same schema
CREATE TABLE dbo.Employee AS CLONE OF dbo.EmployeeUSA;
```  

## B. Create a table clone across schemas within the same data warehouse

```sql
--Clone creation across schemas
CREATE TABLE dbo.Employee AS CLONE OF dbo1.EmployeeUSA;
```  

## C. Create a table clone within the same schema AT a historical point-in-time

```sql
--Clone creation within the same schema
CREATE TABLE dbo.Employee AS CLONE OF dbo.EmployeeUSA AT '2023-05-23T14:24:10.325';
``` 

## D. Create a table clone across schema AT a historical point-in-time

```sql
--Clone creation within the same schema
CREATE TABLE dbo.Employee AS CLONE OF dbo1.EmployeeUSA AT '2023-05-23T14:24:10';
``` 


## Next steps
 
- [Create a table on [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] in [!INCLUDE [fabric](../../includes/fabric.md)]](/fabric/data-warehouse/create-table)
- [[!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] in [!INCLUDE [fabric](../../includes/fabric.md)]](/fabric/data-warehouse/data-warehousing)
- [Clone table in [!INCLUDE [fabric](../../includes/fabric.md)]](/fabric/data-warehouse/tutorial-clone-table)
