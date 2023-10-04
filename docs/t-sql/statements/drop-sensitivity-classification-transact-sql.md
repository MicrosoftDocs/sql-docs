---
title: "DROP SENSITIVITY CLASSIFICATION (Transact-SQL)"
description: DROP SENSITIVITY CLASSIFICATION (Transact-SQL)
author: Madhumitatripathy
ms.author: matripathy
ms.date: 04/19/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP SENSITIVITY CLASSIFICATION"
  - "DROP_SENSITIVITY_CLASSIFICATION"
helpviewer_keywords:
  - "DROP SENSITIVITY CLASSIFICATION statement"
  - "dropping labels"
  - "drop labels"
  - "removing labels"
  - "remove labels"
  - "classification [SQL]"
  - "labels [SQL]"
  - "information types"
  - "data classification"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver15||=azuresqldb-current||=azure-sqldw-latest"
---
# DROP SENSITIVITY CLASSIFICATION (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Drops sensitivity classification metadata from one or more database columns.

## Syntax

```syntaxsql
DROP SENSITIVITY CLASSIFICATION FROM
    <object_name> [, ...n ]

<object_name> ::=
{
    [schema_name.]table_name.column_name
}
```  

## Arguments  

*object_name* ([schema_name.]table_name.column_name)

Is the name of the database column from which to remove the classification. Currently only column classification is supported.
    - *schema_name* (optional) - Is the name of the schema to which the classified column belongs to.
    - *table_name* - Is the name of the table to which the classified column belongs to.
    - *column_name* - Is the name of the column from which to drop the classification.

## Remarks  

- Multiple object classifications can be dropped using a single 'DROP SENSITIVITY CLASSIFICATION' statement.

## Permissions  

Requires ALTER ANY SENSITIVITY CLASSIFICATION permission. The ALTER ANY SENSITIVITY CLASSIFICATION is implied by the database permission CONTROL, or by the server permission CONTROL SERVER.


## Examples  


### A. Dropping classification from a single column

The following example removes the classification from the column `dbo.sales.price`.  

```sql
DROP SENSITIVITY CLASSIFICATION FROM
    dbo.sales.price
```

### B. Dropping classification from multiple columns

The following example removes the classification from the columns `dbo.sales.price`, `dbo.sales.discount`, and `SalesLT.Customer.Phone`.  

```sql
DROP SENSITIVITY CLASSIFICATION FROM
    dbo.sales.price, dbo.sales.discount, SalesLT.Customer.Phone  
```

## See Also  

[ADD SENSITIVITY CLASSIFICATION (Transact-SQL)](../../t-sql/statements/add-sensitivity-classification-transact-sql.md)

[sys.sensitivity_classifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-sensitivity-classifications-transact-sql.md)

[Getting started with SQL Information Protection](/azure/azure-sql/database/data-discovery-and-classification-overview)
