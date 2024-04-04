---
title: CREATE TABLE AS CLONE OF
description: "CREATE TABLE AS CLONE OF creates a new table as a clone of another table in Synapse Data Warehouse in Microsoft Fabric."
author: ajagadish-24
ms.author: ajagadish
ms.reviewer: wiassaf
ms.date: 11/03/2023
ms.service: sql
ms.topic: reference
dev_langs:
  - "TSQL"
ms.custom: ignite-2023
monikerRange: "=fabric"
---
# CREATE TABLE AS CLONE OF

[!INCLUDE [applies-to-version/fabric-dw](../../includes/applies-to-version/fabric-dw.md)]

Creates a new table as a [zero-copy clone of another table](/fabric/data-warehouse/clone-table) in [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] in [!INCLUDE [fabric](../../includes/fabric.md)]. Only the metadata of the table is copied. The underlying data of the table, stored as parquet files, is not copied.

For more information on cloning a table in [!INCLUDE [fabric](../../includes/fabric.md)], see [Clone table with TSQL in Microsoft Fabric](/fabric/data-warehouse/tutorial-clone-table). You can also clone tables in [!INCLUDE [fabric](../../includes/fabric.md)] via the Fabric portal, for examples see [Clone tables in the Fabric portal](/fabric/data-warehouse/tutorial-clone-table-portal).

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax
  
```syntaxsql
CREATE TABLE 
    { database_name.schema_name.table_name | schema_name.table_name | table_name } 
AS CLONE OF 
    { database_name.schema_name.table_name | schema_name.table_name | table_name } [AT {point_in_time}]
```  

## Arguments

#### *database_name*

 The name of the database that will contain the new cloned table. The default is the current database.  
  
#### *schema_name*

The schema of the table where the table clone is located. Specifying *schema* is optional when table is cloned within the same schema. If blank, the default schema is used.

Providing the schema name is required when a table is cloned across schemas.
  
#### *table_name*

The name of the cloned table. Table names must follow the [rules for identifiers](../../relational-databases/databases/database-identifiers.md?view=fabric&preserve-view=true). *table_name* can be a maximum of 128 characters.

#### *point_in_time*

At the date and time provided, the table clone accurately mirrors the data as it was at that particular date and time in UTC. The format of the datetime data type value must be 'YYYY-MM-DDThh:mm:ss', where 'T' indicates the start of time.

Specifying the *point_in_time* is not required for creating table clones as of current point in time.

[!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] automatically preserves and maintains the data history for seven calendar days. For more information, visit [Clone table](/fabric/data-warehouse/clone-table).

## Permissions

Requires:

 - SELECT permission on the entire source table
 - CREATE TABLE permissions on the schema in which the clone of the table will be created
  
## Constraints

Primary and unique key constraints defined in the source table are replicated when creating a clone.
 
## Limitations

For limitations, see [Clone table limitations](/fabric/data-warehouse/clone-table#limitations).

## Examples

### A. Create a table clone within the same schema

```sql
--Clone creation within the same schema
CREATE TABLE dbo.Employee AS CLONE OF dbo.EmployeeUSA;
```  

### B. Create a table clone across schemas within the same data warehouse

```sql
--Clone creation across schemas
CREATE TABLE dbo.Employee AS CLONE OF dbo1.EmployeeUSA;
```  

### C. Create a table clone within the same schema at a point in time

```sql
--Clone creation within the same schema
CREATE TABLE dbo.Employee AS CLONE OF dbo.EmployeeUSA AT '2023-05-23T14:24:10.325';
```

### D. Create a table clone across schema at a point in time

```sql
--Clone creation within the same schema
CREATE TABLE dbo.Employee AS CLONE OF dbo1.EmployeeUSA AT '2023-05-23T14:24:10';
```

## Related content

- [Clone table in Microsoft Fabric](/fabric/data-warehouse/clone-table)
- [Create a table on [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] in [!INCLUDE [fabric](../../includes/fabric.md)]](/fabric/data-warehouse/create-table)
- [[!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] in [!INCLUDE [fabric](../../includes/fabric.md)]](/fabric/data-warehouse/data-warehousing)
- [Clone table in [!INCLUDE [fabric](../../includes/fabric.md)]](/fabric/data-warehouse/tutorial-clone-table)
- [Clone tables in the Fabric portal](/fabric/data-warehouse/tutorial-clone-table-portal)
